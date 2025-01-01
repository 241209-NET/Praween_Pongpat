using ContactManager.API.Model;
using ContactManager.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repository;

public class ContactRepository : IContactRepository
{
    private readonly ContactManagerContext _contactManagerContext;
    public ContactRepository(ContactManagerContext contactManagerContext){
        _contactManagerContext = contactManagerContext;
    }

    public Contact CreateContact(int userId, Contact newContact)
    {
        newContact.UserId = userId;
        _contactManagerContext.Contacts.Add(newContact);
        _contactManagerContext.SaveChanges();
        return newContact;
    }

    public Contact? DeleteContact(int userId, int contactId)
    {
        var contact = GetContactById(userId, contactId);
        //contact is ensured not null, screened in service layer
        _contactManagerContext.Contacts.Remove(contact!);
        _contactManagerContext.SaveChanges();
        return contact;
    }

    public IEnumerable<Contact> GetAllContacts(int userId)
    {
        return _contactManagerContext.Contacts
            .Where(c => c.UserId == userId)
            .ToList();
    }

    public Contact? GetContactById(int userId, int contactId)
    {
        return _contactManagerContext.Contacts
            .FirstOrDefault(c => c.UserId == userId && c.ContactId == contactId);
    }

    public Contact UpdateContact(Contact contact)
    {
        _contactManagerContext.Contacts.Update(contact);
        _contactManagerContext.SaveChanges();
        return contact;
    }
}