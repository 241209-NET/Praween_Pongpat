using ContactManager.API.Model;
using ContactManager.API.Repository;
using ContactManager.API.Service;
using ContactManager.API.Util;
using Moq;

namespace ContactManager.TEST.ServiceTests;

public class UserServiceTest{

    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Utilities _utilities;
    private readonly UserService _userService;
    public UserServiceTest(){
        _mockUserRepository = new Mock<IUserRepository>();
        _utilities = new Utilities();
        _userService = new UserService(_mockUserRepository.Object, _utilities);
    }

    [Fact]
    public void GetAllUserTest(){
        var users = new List<User>{
            new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"},
            new User {UserId = 102, Username = "test102", Email = "test102@test.test", Password = "password2"},
            new User {UserId = 103, Username = "test103", Email = "test103@test.test", Password = "password3"}
        };
        _mockUserRepository.Setup( repo => repo.GetAllUsers()).Returns(users);

        var outputUsers = _userService.GetAllUsers();

        Assert.NotNull(outputUsers);
        Assert.Equal(3, outputUsers.Count());
        //check contains in the list
        Assert.Contains(outputUsers, u => u.UserId == 101 && u.Username == "test101" && u.Email == "test101@test.test");
        Assert.Contains(outputUsers, u => u.UserId == 102 && u.Username == "test102" && u.Email == "test102@test.test");
        Assert.Contains(outputUsers, u => u.UserId == 103 && u.Username == "test103" && u.Email == "test103@test.test");
    }

    [Fact]
    public void CreateUserTest(){
        var userInput = new UserInputDTO{Username = "test101", Email = "test101@test.test", Password = "password1"};
        var expectedUser = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        _mockUserRepository.Setup(repo => repo.CreateUser(It.Is<User>( u =>
            u.Username == userInput.Username &&
            u.Email == userInput.Email &&
            u.Password == userInput.Password
            ))).Returns(expectedUser);

        var outputUser = _userService.CreateUser(userInput);

        Assert.NotNull(outputUser);
        Assert.Equal(101, outputUser.UserId);
        Assert.Equal("test101", outputUser.Username);
        Assert.Equal("password1", outputUser.Password);
    }

    [Fact]
    public void GetUserById__ValidUserId_Passed()
    {
        var user = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        _mockUserRepository.Setup(repo => repo.GetUserById(user.UserId)).Returns(user);

        var outputUser = _userService.GetUserById(user.UserId);

        Assert.NotNull(outputUser);
        Assert.Equal(101, outputUser.UserId);
        Assert.Equal("test101", outputUser.Username);
        Assert.Equal("password1", outputUser.Password);
    }

    [Fact]
    public void GetUserById__InvalidUserId()
    {
        var user = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        int otherId = 1;
        _mockUserRepository.Setup(repo => repo.GetUserById(otherId)).Returns((User)null!);

        var outputUser = _userService.GetUserById(otherId);

        Assert.Null(outputUser);
    }

    [Fact]
    public void DeleteUserTest_ValidUserId_Passed(){
        var user = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        _mockUserRepository.Setup(repo => repo.GetUserById(user.UserId)).Returns(user);
        _mockUserRepository.Setup(repo => repo.DeleteUser(user.UserId)).Returns(user);

        var outputUser = _userService.DeleteUser(user.UserId);

        Assert.NotNull(outputUser);
        Assert.Equal(101, outputUser.UserId);
        Assert.Equal("test101", outputUser.Username);
        Assert.Equal("password1", outputUser.Password);
    }

    [Fact]
    public void DeleteUserTest_InvalidUserId(){
        var user = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        int otherId = 102;
        _mockUserRepository.Setup(repo => repo.DeleteUser(otherId)).Returns((User)null!);

        var outputUser = _userService.DeleteUser(otherId);

        Assert.Null(outputUser);
    }

    [Fact]
    public void DeleteUserTest_ValidUserId_NullOnDeleteUser(){
        var user = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        _mockUserRepository.Setup(repo => repo.GetUserById(user.UserId)).Returns(user);
        _mockUserRepository.Setup(repo => repo.DeleteUser(user.UserId)).Returns((User)null!);

        var outputUser = _userService.DeleteUser(user.UserId);

        Assert.Null(outputUser);
    }

    [Fact]
    public void UpdateUserTest_ValidUserId(){
        var beforeUpdate = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        var afterUpdate = new User {UserId = 101, Username = "test102", Email = "test102@test.test", Password = "password2"};
        var updateDTO = new UserUpdateDTO {Username = "test102", Email = "test102@test.test", Password = "password2"};
        _mockUserRepository.Setup(repo => repo.GetUserById(beforeUpdate.UserId)).Returns(beforeUpdate);
        _mockUserRepository.Setup(repo => repo.UpdateUser(beforeUpdate)).Returns(afterUpdate);

        var outputUser = _userService.UpdateUser(beforeUpdate.UserId,updateDTO);

        Assert.NotNull(outputUser);
        Assert.Equal(101, outputUser.UserId);
        Assert.Equal("test102", outputUser.Username);
        Assert.Equal("password2", outputUser.Password);
    }

    [Fact]
    public void UpdateUserTest_InvalidUserId(){
        var beforeUpdate = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        var afterUpdate = new User {UserId = 101, Username = "test102", Email = "test102@test.test", Password = "password2"};
        var updateDTO = new UserUpdateDTO {Username = "test102", Email = "test102@test.test", Password = "password2"};
        int otherId = 102;
        _mockUserRepository.Setup(repo => repo.GetUserById(otherId)).Returns((User)null!);
        _mockUserRepository.Setup(repo => repo.UpdateUser(beforeUpdate)).Returns(afterUpdate);

        var outputUser = _userService.UpdateUser(beforeUpdate.UserId,updateDTO);

        Assert.Null(outputUser);
    }

    [Fact]
    public void UpdateUserTest_ValidUserId_NullEmailNullPassword(){
        var beforeUpdate = new User {UserId = 101, Username = "test101", Email = "test101@test.test", Password = "password1"};
        var afterUpdate = new User {UserId = 101, Username = "test102", Email = "test101@test.test", Password = "password1"};
        var updateDTO = new UserUpdateDTO {Username = "test102", Email = null, Password = null};
        _mockUserRepository.Setup(repo => repo.GetUserById(beforeUpdate.UserId)).Returns(beforeUpdate);
        _mockUserRepository.Setup(repo => repo.UpdateUser(beforeUpdate)).Returns(afterUpdate);

        var outputUser = _userService.UpdateUser(beforeUpdate.UserId,updateDTO);

        Assert.NotNull(outputUser);
        Assert.Equal(101, outputUser.UserId);
        Assert.Equal("test102", outputUser.Username);
        Assert.Equal("test101@test.test", outputUser.Email);
        Assert.Equal("password1", outputUser.Password);
    }
}