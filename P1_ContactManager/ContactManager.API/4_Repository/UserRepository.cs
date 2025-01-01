using ContactManager.API.Data;
using ContactManager.API.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repository;

public class UserRepository : IUserRepository
{

    private readonly ContactManagerContext _contactManagerContext;
    public UserRepository(ContactManagerContext contactManagerContext){
        _contactManagerContext = contactManagerContext;
    }

    public User CreateUser(User newUser)
    {
        _contactManagerContext.Users.Add(newUser);
        _contactManagerContext.SaveChanges();
        return newUser;
    }

    public User? DeleteUser(int userId)
    {
        var user = GetUserById(userId);
        //user is ensured not null, screened in service layer
        _contactManagerContext.Users.Remove(user!);
        _contactManagerContext.SaveChanges();
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _contactManagerContext.Users
            .Include(c => c.Contacts)
            .ToList();
    }

    public User? GetUserById(int userId)
    {
        return _contactManagerContext.Users
            .Include(c => c.Contacts)
            .FirstOrDefault(u => u.UserId == userId);
    }

    public User UpdateUser(User user)
    {
        _contactManagerContext.Users.Update(user);
        _contactManagerContext.SaveChanges();
        return user;
    }
}