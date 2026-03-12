using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;


/// <summary>
/// Represents an individual product in the e-commerce application, 
/// with properties for identification, title, and price.
/// </summary>
public class Product
{
    [Key]
    public int ProductId { get; set; }
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public required string Title { get; set; }
    [Range(0, 10_000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
}
