using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Model;

public class Contact{
    public int ContactId { get; set; }
    public int UserId { get; set; }
    public required string Name { get; set; }
    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    public string? Memo { get; set; }
}