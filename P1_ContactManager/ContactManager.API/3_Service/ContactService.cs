using ContactManager.API.Model;
using ContactManager.API.Repository;
using ContactManager.API.Util;

namespace ContactManager.API.Service;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly Utilities _utilities;
    public ContactService(IContactRepository contactRepository, Utilities utilities){
        _contactRepository = contactRepository;
        _utilities = utilities;
    }
    public ContactOutputDTO CreateContact(int userId, ContactInputDTO contactInputDTO)
    {
        var contact = _utilities.ContactInputDTOtoContactObject(contactInputDTO);
        var newContact = _contactRepository.CreateContact(userId, contact);
        return _utilities.ContactObjectToDTOOutput(newContact);
    }

    public ContactOutputDTO? DeleteContact(int userId, int contactId)
    {
        var contact = GetContactById(userId, contactId);
        if(contact is null) return null;
        var deletedContact = _contactRepository.DeleteContact(userId, contactId);
        if(deletedContact is null) return null;
        return _utilities.ContactObjectToDTOOutput(deletedContact);
    }

    public IEnumerable<ContactOutputDTO> GetAllContacts(int userId)
    {
        var contactList = _contactRepository.GetAllContacts(userId);
        return contactList.Select( contact => _utilities.ContactObjectToDTOOutput(contact));
    }

    public ContactOutputDTO? GetContactById(int userId, int contactId)
    {
        var contact = _contactRepository.GetContactById(userId, contactId);
        if(contact is null) return null;
        return _utilities.ContactObjectToDTOOutput(contact);
    }

    public ContactOutputDTO? UpdateContact(int userId, int contactId, ContactUpdateDTO contactUpdateDTO)
    {
        var existingContact = _contactRepository.GetContactById(userId, contactId);
        if (existingContact == null) return null;

        existingContact.Name = contactUpdateDTO.Name;
        existingContact.PhoneNumber = contactUpdateDTO.PhoneNumber ?? existingContact.PhoneNumber;
        existingContact.Email = contactUpdateDTO.Email ?? existingContact.Email;
        existingContact.Memo = contactUpdateDTO.Memo ?? existingContact.Memo;

        _contactRepository.UpdateContact(existingContact);
        return _utilities.ContactObjectToDTOOutput(existingContact);
    }

}