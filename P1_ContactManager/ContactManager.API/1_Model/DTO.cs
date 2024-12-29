namespace ContactManager.API.Model;

public class UserInputDTO{
    public required string UserName { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }
}

public class UserOutputDTO{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }
    public List<Contact> Contacts { get; set; } = [];
}

public class ContactInputDTO{
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
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