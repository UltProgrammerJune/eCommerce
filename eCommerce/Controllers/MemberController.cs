using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class MemberController : Controller
{
    private readonly ProductDbContext _context;

    public MemberController(ProductDbContext context)
    {
        _context = context;
    }
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel reg)
    {
        if (ModelState.IsValid)
        {
            // Check if the name/email is already registered
            bool usernameTaken = await _context.Members
                                 .AnyAsync(m => m.Name == reg.Name);

            if (usernameTaken)
            {
                ModelState.AddModelError(nameof(Member.Name), "Username already taken");
                return View(reg);
            }

            bool emailTaken = await _context.Members
                     .AnyAsync(m => m.Email == reg.Email);

            if (emailTaken)
            {
                ModelState.AddModelError(nameof(Member.Email), "Email already taken");
                return View(reg);
            }

            if (usernameTaken || emailTaken)
            {
                return View(reg);
            }

            // Map ViewModel to Member model tracked by Database
            Member newMember = new()
            {
                Name = reg.Name,
                Email = reg.Email,
                Password = reg.Password,
                DOB = reg.DOB
            };

            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        return View(reg);
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            // Check if the user exists in the database
            Member? loggedInMember = await _context.Members
                                    .Where(m => (m.Email == login.UsernameOrEmail || m.Name == login.UsernameOrEmail)
                                        && m.Password == login.Password)
                                    .SingleOrDefaultAsync();

            if (loggedInMember == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username/email or password.");
                return View(login);
            }
            // Log the user in
            HttpContext.Session.SetString("Username", loggedInMember.Name);
            HttpContext.Session.SetInt32("ID", loggedInMember.MemberID);

            return RedirectToAction("Index", "Home");
        }
        return View(login);
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}

