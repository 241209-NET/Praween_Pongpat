using ContactManager.API.Model;
using ContactManager.API.Repository;

namespace ContactManager.API.Service;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }
    public User CreateUser(User newUser)
    {
        return _userRepository.CreateUser(newUser);
    }

    public User? DeleteUser(int userId)
    {
        var user = GetUserById(userId);
        if(user is null) return null;
        return _userRepository.DeleteUser(userId);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User? GetUserById(int userId)
    {
        return _userRepository.GetUserById(userId);
    }
}