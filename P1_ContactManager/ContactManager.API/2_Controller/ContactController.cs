using ContactManager.API.Model;
using ContactManager.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase {
    
    private readonly IContactService _contactService;
    public ContactController (IContactService contactService){
        _contactService = contactService;
    }

    [HttpGet]
    public IActionResult GetAllContacts(int userId){
        var contactList = _contactService.GetAllContacts(userId);
        if(contactList is null) return BadRequest("something is wrong on getAllContacts");
        return Ok(contactList);
    }

    [HttpGet("{contactId}")]
    public IActionResult GetContactById(int userId, int contactId){
        var contact = _contactService.GetContactById(userId, contactId);
        if(contact is null) return NotFound("No contact found from given user id: " + userId);
        return Ok(contact);
    }

    [HttpPost]
    public IActionResult CreateContact(int userId, Contact newContact){
        var contact = _contactService.CreateContact(userId, newContact);
        if(contact is null) return BadRequest("something is wrong on createContact");
        return Ok(contact);
    }

    [HttpDelete]
    public IActionResult DeleteContact(int userId, int contactId){
        var contact = _contactService.DeleteContact(userId, contactId);
        if(contact is null) return NotFound("No contactId found to be deleted");
        return Ok(contact);
    }
}