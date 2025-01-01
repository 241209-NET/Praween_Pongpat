using ContactManager.API.Model;

namespace ContactManager.API.Repository;

public interface IUserRepository{
    //CRUD op. for Basic goals
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int userId);
    User CreateUser(User newUser);
    User? DeleteUser(int userId);
    User UpdateUser(User user);
}

public interface IContactRepository{
    //CRUD op. for Basic goals
    IEnumerable<Contact> GetAllContacts(int userId);
    Contact? GetContactById(int userId, int contactId);
    Contact CreateContact(int userId, Contact newContact);
    Contact? DeleteContact(int userId, int contactId);
    Contact UpdateContact(Contact contact);
}