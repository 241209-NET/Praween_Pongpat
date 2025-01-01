using ContactManager.API.Model;
using ContactManager.API.Repository;
using ContactManager.API.Service;
using ContactManager.API.Util;
using Moq;

namespace ContactManager.TEST.ServiceTests;

public class ContactServiceTest{
    private readonly Mock<IContactRepository> _mockConactRepository;
    private readonly Utilities _utilities;
    private readonly ContactService _contactService;
    public ContactServiceTest(){
        _mockConactRepository = new Mock<IContactRepository>();
        _utilities = new Utilities();
        _contactService = new ContactService(_mockConactRepository.Object, _utilities);
    }

    [Fact]
    public void GetAllContactTest(){
        var contacts = new List<Contact>{
            new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"},
            new Contact {ContactId = 2, UserId = 101, Name = "test2", PhoneNumber = "2222222222",Email = "test2@test.test", Memo = "test2"},
            new Contact {ContactId = 3, UserId = 101, Name = "test3", PhoneNumber = "3333333333",Email = "test3@test.test", Memo = "test3"}
        };
        int userId = 101;
        _mockConactRepository.Setup(repo => repo.GetAllContacts(userId)).Returns(contacts);

        var outputContact = _contactService.GetAllContacts(userId);

        Assert.NotNull(outputContact);
        Assert.Equal(3, outputContact.Count());
        //check contains in the list
        Assert.Contains(outputContact, c => c.ContactId == 1 && c.Name == "test1" && c.Email == "test1@test.test");
        Assert.Contains(outputContact, c => c.ContactId == 2 && c.Name == "test2" && c.Email == "test2@test.test");
        Assert.Contains(outputContact, c => c.ContactId == 3 && c.Name == "test3" && c.Email == "test3@test.test");
        Assert.DoesNotContain(outputContact, c => c.UserId == 102);
    }

    [Fact]
    public void CreateContactTest_Passed(){
        var contactInput = new ContactInputDTO {Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        var expectedContact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        int userId = 101;
        
        _mockConactRepository.Setup(repo => repo.CreateContact(userId, It.IsAny<Contact>())).Returns(expectedContact);

        var outputContact = _contactService.CreateContact(userId, contactInput);

        Assert.NotNull(outputContact);
        Assert.Equal(101, outputContact.UserId);
        Assert.Equal("test1", outputContact.Name);
        Assert.Equal("test1@test.test", outputContact.Email);
    }

    [Fact]
    public void GetContactByIdTest_ValidUserId_ValidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        _mockConactRepository.Setup(repo => repo.GetContactById(contact.UserId, contact.ContactId)).Returns(contact);

        var outputContact = _contactService.GetContactById(contact.UserId, contact.ContactId);

        Assert.NotNull(outputContact);
        Assert.Equal(1, outputContact.ContactId);
        Assert.Equal(101, outputContact.UserId);
        Assert.Equal("test1", outputContact.Name);
        Assert.Equal("test1@test.test", outputContact.Email);
    }

    [Fact]
    public void GetContactByIdTest_InvalidUserId_ValidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        int otherUserId = 102;
        _mockConactRepository.Setup(repo => repo.GetContactById(otherUserId, contact.ContactId)).Returns((Contact)null!);

        var outputContact = _contactService.GetContactById(otherUserId, contact.ContactId);

        Assert.Null(outputContact);
    }

    [Fact]
    public void GetContactByIdTest_ValidUserId_InvalidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        int otherConactId = 102;
        _mockConactRepository.Setup(repo => repo.GetContactById(contact.UserId, otherConactId)).Returns((Contact)null!);

        var outputContact = _contactService.GetContactById(contact.UserId, otherConactId);

        Assert.Null(outputContact);
    }

    [Fact]
    public void DeleteContactTest_ValidUserId_ValidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        _mockConactRepository.Setup(repo => repo.GetContactById(contact.UserId, contact.ContactId)).Returns(contact);
        _mockConactRepository.Setup(repo => repo.DeleteContact(contact.UserId, contact.ContactId)).Returns(contact);

        var outputContact = _contactService.DeleteContact(contact.UserId, contact.ContactId);

        Assert.NotNull(outputContact);
        Assert.Equal(contact.ContactId, outputContact.ContactId);
        Assert.Equal(contact.Name, outputContact.Name);
        Assert.Equal(contact.Email, outputContact.Email);
    }

    [Fact]
    public void DeleteContactTest_InvalidUserId_ValidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        int otherUserId = 102;
        _mockConactRepository.Setup(repo => repo.GetContactById(otherUserId, contact.ContactId)).Returns((Contact)null!);
        _mockConactRepository.Setup(repo => repo.DeleteContact(otherUserId, contact.ContactId)).Returns((Contact)null!);

        var outputContact = _contactService.DeleteContact(otherUserId, contact.ContactId);

        Assert.Null(outputContact);
    }

    [Fact]
    public void DeleteContactTest_ValidUserId_InvalidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111",Email = "test1@test.test", Memo = "test1"};
        int otherConactId = 2;
        _mockConactRepository.Setup(repo => repo.GetContactById(contact.UserId, otherConactId)).Returns((Contact)null!);
        _mockConactRepository.Setup(repo => repo.DeleteContact(contact.UserId, otherConactId)).Returns((Contact)null!);

        var outputContact = _contactService.DeleteContact(contact.UserId, otherConactId);

        Assert.Null(outputContact);
    }

    [Fact]
    public void UpdateContactTest_ValidUserId_ValidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111", Email = "test1@test.test", Memo = "test1"};
        var contactAfterUpdate = new Contact {ContactId = 1, UserId = 101, Name = "test2", PhoneNumber = "2222222222", Email = "test2@test.test", Memo = "test2"};
        var updateDTO =  new ContactUpdateDTO {Name = "test2", PhoneNumber = "2222222222", Email = "test2@test.test", Memo = "test2"};
        _mockConactRepository.Setup(repo => repo.GetContactById(contact.UserId, contact.ContactId)).Returns(contact);
        _mockConactRepository.Setup(repo => repo.UpdateContact(contact)).Returns(contactAfterUpdate);

        var outputContact = _contactService.UpdateContact(contact.UserId, contact.ContactId, updateDTO);

        Assert.NotNull(outputContact);
        Assert.Equal(contactAfterUpdate.Name, outputContact.Name);
        Assert.Equal(contactAfterUpdate.PhoneNumber, outputContact.PhoneNumber);
        Assert.Equal(contactAfterUpdate.Email, outputContact.Email);
        Assert.Equal(contactAfterUpdate.Memo, outputContact.Memo);
    }

    [Fact]
    public void UpdateContactTest_ValidUserId_ValidContactId_NullPhoneNumber_NullEmail_NullMemo(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111", Email = "test1@test.test", Memo = "test1"};
        var contactAfterUpdate = new Contact {ContactId = 1, UserId = 101, Name = "test2", PhoneNumber = "1111111111", Email = "test1@test.test", Memo = "test1"};
        var updateDTO =  new ContactUpdateDTO {Name = "test2", PhoneNumber = null, Email = null, Memo = null};
        _mockConactRepository.Setup(repo => repo.GetContactById(contact.UserId, contact.ContactId)).Returns(contact);
        _mockConactRepository.Setup(repo => repo.UpdateContact(contact)).Returns(contactAfterUpdate);

        var outputContact = _contactService.UpdateContact(contact.UserId, contact.ContactId, updateDTO);

        Assert.NotNull(outputContact);
        Assert.Equal(contactAfterUpdate.Name, outputContact.Name);
        Assert.Equal(contactAfterUpdate.PhoneNumber, outputContact.PhoneNumber);
        Assert.Equal(contactAfterUpdate.Email, outputContact.Email);
        Assert.Equal(contactAfterUpdate.Memo, outputContact.Memo);
    }

    [Fact]
    public void UpdateContactTest_InvalidUserId_ValidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111", Email = "test1@test.test", Memo = "test1"};
        var contactAfterUpdate = new Contact {ContactId = 1, UserId = 101, Name = "test2", PhoneNumber = "2222222222", Email = "test2@test.test", Memo = "test2"};
        var updateDTO =  new ContactUpdateDTO {Name = "test2", PhoneNumber = "2222222222", Email = "test2@test.test", Memo = "test2"};
        int otherUserId = 102;
        _mockConactRepository.Setup(repo => repo.GetContactById(otherUserId, contact.ContactId)).Returns((Contact)null!);
        _mockConactRepository.Setup(repo => repo.UpdateContact(contact)).Returns(contactAfterUpdate);

        var outputContact = _contactService.UpdateContact(contact.UserId, contact.ContactId, updateDTO);

        Assert.Null(outputContact);
    }

    [Fact]
    public void UpdateContactTest_ValidUserId_InvalidContactId(){
        var contact = new Contact {ContactId = 1, UserId = 101, Name = "test1", PhoneNumber = "1111111111", Email = "test1@test.test", Memo = "test1"};
        var contactAfterUpdate = new Contact {ContactId = 1, UserId = 101, Name = "test2", PhoneNumber = "2222222222", Email = "test2@test.test", Memo = "test2"};
        var updateDTO =  new ContactUpdateDTO {Name = "test2", PhoneNumber = "2222222222", Email = "test2@test.test", Memo = "test2"};
        int otherConactId = 2;
        _mockConactRepository.Setup(repo => repo.GetContactById(contact.UserId, otherConactId)).Returns((Contact)null!);
        _mockConactRepository.Setup(repo => repo.UpdateContact(contact)).Returns(contactAfterUpdate);

        var outputContact = _contactService.UpdateContact(contact.UserId, contact.ContactId, updateDTO);

        Assert.Null(outputContact);
    }
}