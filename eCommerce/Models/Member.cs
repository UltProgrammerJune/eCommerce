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
    public required string Name { get; set; }
    /// <summary>
    /// Email address of the member.
    /// </summary>
    public required string Email { get; set; }
    /// <summary>
    /// Password for the member's account.
    /// </summary>
    public required string Password { get; set; }
    /// <summary>
    /// Date of birth of the member.
    /// </summary>
    public DateOnly DOB { get; set; }
}
