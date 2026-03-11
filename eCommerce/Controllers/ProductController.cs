using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers;

public class ProductController : Controller
{
    private readonly ProductDbContext _context;
    public ProductController(ProductDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product); /// Add to database
            await _context.SaveChangesAsync(); /// Saves changes to database

            return RedirectToAction(nameof(Index));
        }
        return View(product); // If model state is not valid, return the view with the product to show validation errors
    }
}
