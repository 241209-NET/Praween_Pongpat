namespace ContactManager.API.Model;

public class Contact{
    public int ContactId { get; set; }
    public int UserId { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Memo { get; set; }

    public User User { get; set; } //show relation "1" user to "N" contacts
    
}