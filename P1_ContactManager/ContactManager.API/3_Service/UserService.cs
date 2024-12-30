using ContactManager.API.Model;
using ContactManager.API.Repository;
using ContactManager.API.Util;

namespace ContactManager.API.Service;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;
    private readonly Utilities _utilities;
    public UserService(IUserRepository userRepository, Utilities utilities){
        _userRepository = userRepository;
        _utilities = utilities;
    }
    public UserOutputDTO CreateUser(UserInputDTO userInputDTO)
    {
        var user = _utilities.UserInputDTOToUserObject(userInputDTO);
        var newUser = _userRepository.CreateUser(user);
        return _utilities.UserObjectToDTOOutput(newUser);
    }
    
    public UserOutputDTO? DeleteUser(int userId)
    {
        var user = GetUserById(userId);
        if(user is null) return null;
        var deletedUser = _userRepository.DeleteUser(userId);
        if(deletedUser is null) return null;
        return _utilities.UserObjectToDTOOutput(deletedUser);
    }

    public IEnumerable<UserOutputDTO> GetAllUsers()
    {
        var userList = _userRepository.GetAllUsers();
        return userList.Select( user => _utilities.UserObjectToDTOOutput(user));
    }

    public UserOutputDTO? GetUserById(int userId)
    {
        var user = _userRepository.GetUserById(userId);
        if(user is null) return null;
        return _utilities.UserObjectToDTOOutput(user);
    }

    public UserOutputDTO? UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        var existingUser = _userRepository.GetUserById(userId);
        if (existingUser == null) return null;

        existingUser.Username = userUpdateDTO.Username;
        existingUser.Email = userUpdateDTO.Email ?? existingUser.Email;
        existingUser.Password = userUpdateDTO.Password ?? existingUser.Password;

        _userRepository.UpdateUser(existingUser);
        return _utilities.UserObjectToDTOOutput(existingUser);
}
}