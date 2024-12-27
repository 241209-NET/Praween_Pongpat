using ContactManager.API.Model;

namespace ContactManager.API.Service;

public interface IUserService{
    //CRUD op. for Basic goals
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int userId);
    User CreateUser(User newUser);
    User? DeleteUser(int userId);
}

public interface IContactService{
    //CRUD op. for Basic goals
    IEnumerable<Contact> GetAllContacts(int userId);
    Contact? GetContactById(int userId, int contactId);
    Contact CreateContact(int userId, Contact newContact);
    Contact? DeleteContact(int userId, int contactId);
}