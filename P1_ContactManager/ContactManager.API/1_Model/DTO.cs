using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Model;

public class UserInputDTO{
    public required string Username { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public required string Password { get; set; }
}

public class UserOutputDTO{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }
    public List<int> Contacts { get; set; } = [];
}

public class ContactInputDTO{
    public required string Name { get; set; }
    [Phone]
    public required string PhoneNumber { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Memo { get; set; }
}

public class ContactOutputDTO{
    public int ContactId { get; set; }
    public int UserId { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Memo { get; set; }
}

public class UserUpdateDTO
{
    public required string Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class ContactUpdateDTO
{
    public required string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Memo { get; set; }
}
