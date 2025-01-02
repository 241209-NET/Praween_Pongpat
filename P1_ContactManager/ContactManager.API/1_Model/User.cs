using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Model;

public class User{
    public int UserId { get; set; }
    public required string Username { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    public required string Password { get; set; }

    public List<Contact> Contacts { get; set; } = []; //show relation "1" user to "N" contacts
}