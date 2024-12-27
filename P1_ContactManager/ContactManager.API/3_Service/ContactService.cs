using ContactManager.API.Model;
using ContactManager.API.Repository;

namespace ContactManager.API.Service;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    public ContactService(IContactRepository contactRepository){
        _contactRepository = contactRepository;
    }
    public Contact CreateContact(int userId, Contact newContact)
    {
        return _contactRepository.CreateContact(userId, newContact);
    }

    public Contact? DeleteContact(int userId, int contactId)
    {
        var contact = GetContactById(userId, contactId);
        if(contact is null) return null;
        return _contactRepository.DeleteContact(userId, contactId);
    }

    public IEnumerable<Contact> GetAllContacts(int userId)
    {
        return _contactRepository.GetAllContacts(userId);
    }

    public Contact? GetContactById(int userId, int contactId)
    {
        return _contactRepository.GetContactById(userId, contactId);
    }
}