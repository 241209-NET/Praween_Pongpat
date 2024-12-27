using Microsoft.EntityFrameworkCore;
using ContactManager.API.Model;

namespace ContactManager.API.Data;

public class ContactManagerContext : DbContext
{
    public ContactManagerContext(){}
    public ContactManagerContext(DbContextOptions<ContactManagerContext> options) : base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
}