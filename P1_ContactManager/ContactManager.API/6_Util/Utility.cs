using ContactManager.API.Model;

namespace ContactManager.API.Util;

public class Utilities{
    public UserOutputDTO UserObjectToDTOOutput(User user){
        return new UserOutputDTO{
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            Contacts = user.Contacts
        };
    }

    public User UserInputDTOToUserObject(UserInputDTO userInputDTO){
        return new User{
            Username = userInputDTO.UserName,
            Email = userInputDTO.Email,
            Password = userInputDTO.Password
        };
    }

    public ContactOutputDTO ContactObjectToDTOOutput(Contact contact){
        return new ContactOutputDTO{
            ContactId = contact.ContactId,
            UserId = contact.UserId,
            Name = contact.Name,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
            Memo = contact.Memo
        };
    }

    public Contact ContactInputDTOtoContactObject(ContactInputDTO contactInputDTO){
        return new Contact{
            Name = contactInputDTO.Name,
            PhoneNumber = contactInputDTO.PhoneNumber,
            Email = contactInputDTO.Email,
            Memo = contactInputDTO.Memo
        };
    }


}