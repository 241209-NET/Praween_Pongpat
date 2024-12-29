using ContactManager.API.Model;

namespace ContactManager.API.Service;

public interface IUserService{
    //CRUD op. for Basic goals
    IEnumerable<UserOutputDTO> GetAllUsers();
    UserOutputDTO? GetUserById(int userId);
    UserOutputDTO CreateUser(UserInputDTO userInputDTO);
    UserOutputDTO? DeleteUser(int userId);
}

public interface IContactService{
    //CRUD op. for Basic goals
    IEnumerable<ContactOutputDTO> GetAllContacts(int userId);
    ContactOutputDTO? GetContactById(int userId, int contactId);
    ContactOutputDTO CreateContact(int userId, ContactInputDTO contactInputDTO);
    ContactOutputDTO? DeleteContact(int userId, int contactId);
}