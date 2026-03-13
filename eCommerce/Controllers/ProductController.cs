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
    public ProductController(ProductDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves products from the database (with optional search / price filters)
    /// and returns them to the view for display with pagination.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, decimal? minPrice, decimal? maxPrice, int page = 1)
    {
        const int productsPerPage = 3; // Number of products to display per page

        IQueryable<Product> query = _context.Products;

        // Apply filters
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.Title.Contains(searchTerm));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        int totalProducts = await query.CountAsync();
        int totalPagesNeeded = (int)Math.Ceiling(totalProducts / (double)productsPerPage);

        if (page < 1) page = 1;
        if (totalPagesNeeded > 0 && page > totalPagesNeeded) page = totalPagesNeeded;

        List<Product> products = await query
            .OrderBy(p => p.ProductId)
            .Skip((page - 1) * productsPerPage)
            .Take(productsPerPage)
            .ToListAsync();

        ProductListViewModel productListViewModel = new()
        {
            Products = products,
            CurrentPage = page,
            TotalPages = totalPagesNeeded,
            PageSize = productsPerPage,
            TotalItems = totalProducts,
            SearchTerm = searchTerm,
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };

        return View(productListViewModel);
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
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"{product.Title} was created successfully!";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        Product? product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
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
