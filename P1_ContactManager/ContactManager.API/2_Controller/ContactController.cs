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
    public IActionResult GetContactById([FromQuery] int userId, int contactId){
        if(userId == 0) return BadRequest("invalid userId, or required");
        var contact = _contactService.GetContactById(userId, contactId);
        if(contact is null) return NotFound("No contact found from given user id: " + userId);
        return Ok(contact);
    }

    [HttpPost]
    public IActionResult CreateContact(int userId, [FromBody] ContactInputDTO contactInputDTO){
        //check input format from inputDTO
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var contact = _contactService.CreateContact(userId, contactInputDTO);
        if(contact is null) return BadRequest("something is wrong on createContact");
        return Ok(contact);
    }

    [HttpDelete]
    public IActionResult DeleteContact(int userId, int contactId){
        var contact = _contactService.DeleteContact(userId, contactId);
        if(contact is null) return NotFound("No contactId found to be deleted");
        return Ok(contact);
    }

    [HttpPut("{contactId}")]
    public IActionResult UpdateContact(int userId, int contactId, [FromBody] ContactUpdateDTO contactUpdateDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var contact = _contactService.UpdateContact(userId, contactId, contactUpdateDTO);
        if (contact == null)
            return NotFound($"Contact with ID {contactId} not found for user {userId}.");

        return Ok(contact);
    }
}