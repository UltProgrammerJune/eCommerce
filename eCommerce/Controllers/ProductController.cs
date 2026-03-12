using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class ProductController : Controller
{
    private readonly ProductDbContext _context;
    /// <summary>
    /// Initializes a new instance of the ProductController class using the specified database context.
    /// </summary>
    /// <param name="context">The database context used to access and manage product data within the application.</param>
    public ProductController(ProductDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Retrieves all products from the database and returns them to the view for display.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        List<Product> allProducts = await _context.Products.ToListAsync();
        return View(allProducts);
    }

    [HttpGet]
    /// <summary>
    /// Returns the view for creating a new product, allowing the user to input product details.
    /// </summary>
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    /// <summary>
    /// Validates the model state, adds the new product to the database, saves changes, and redirects to the index page with a success message.
    /// </summary>
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product); /// Add to database
            await _context.SaveChangesAsync(); /// Saves changes to database

            TempData["Message"] = $"{product.Title} was created successfully!"; // Set a success message to display after redirecting

            return RedirectToAction(nameof(Index));
        }
        return View(product); // If model state is not valid, return the view with the product to show validation errors
    }

    [HttpGet]
    /// <summary>
    /// Retrieves the product with the specified ID from the database and returns it to the view for editing.
    /// </summary>
    public IActionResult Edit(int id)
    {
        Product? product = _context.Products
            .Where(p => p.ProductId == id)
            .FirstOrDefault();

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]

    /// <summary>
    /// Validates the model state, updates the product in the database, and redirects to the index page with a success message.
    /// </summary>
    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"{product.Title} was updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    /// <summary>
    /// Retrieves the product within the database and returns it to the view for confirmation before deletion.
    /// </summary>

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Product? product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    /// <summary>
    /// Deletes the specified product from the database, saves changes, and redirects to the index page with a success message.
    /// </summary>

    [ActionName(nameof(Delete))]
    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        Product? product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return RedirectToAction(nameof(Index));
        }

        _context.Remove(product);
        await _context.SaveChangesAsync();

        TempData["Message"] = $"{product.Title} was deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
