using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

/// <summary>
/// Represents a individual website user.
/// </summary>
public class Member
{
    /// <summary>
    /// Unique identifier for the member, serving as the primary key in the database.
    /// </summary>
    [Key]
    public int MemberID { get; set; }
    /// <summary>
    /// Public facing username of the member.
    /// Alphanumeric chars only.
    /// </summary>
    [RegularExpression(@"^[a-zA-Z0-9]+$",
        ErrorMessage = "Username must be alphanumeric.")]
    [StringLength(25)]
    public required string Name { get; set; }
    /// <summary>
    /// Email address of the member.
    /// </summary>
    public required string Email { get; set; }
    /// <summary>
    /// Password for the member's account.
    /// </summary>
    [StringLength(50, MinimumLength = 6,
        ErrorMessage = "Your password must be between 6 and 50 characters.")]
    public required string Password { get; set; }
    /// <summary>
    /// Date of birth of the member.
    /// </summary>
    public DateOnly DOB { get; set; }
}

public class RegisterViewModel
{
    [RegularExpression(@"^[a-zA-Z0-9]+$",
        ErrorMessage = "Username must be alphanumeric.")]
    [StringLength(25)]
    public required string Username { get; set; }
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    [StringLength(50, MinimumLength = 6,
        ErrorMessage = "Your password must be between 6 and 50 characters.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [Compare(nameof(Password),
    ErrorMessage = "The password does not match.")]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }
    [DataType(DataType.Date)]
    public DateOnly DOB { get; set; }
}
