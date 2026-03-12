using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
}
