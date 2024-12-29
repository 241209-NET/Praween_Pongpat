using ContactManager.API.Model;
using ContactManager.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
    
    private readonly IUserService _userService;
    public UserController(IUserService userService){
        _userService = userService;
    }

    [HttpPost]
    public IActionResult CreateUser(UserInputDTO userInputDTO){
        var user = _userService.CreateUser(userInputDTO);
        if(user is null) return BadRequest("something is wrong on createUser");
        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetAllUsers(){
        var userList = _userService.GetAllUsers();
        if(userList is null) return BadRequest("something is wrong on getAllUsers");
        return Ok(userList);
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserById(int userId){
        var user = _userService.GetUserById(userId);
        if(user is null) return NotFound("No userId found");
        return Ok(user);
    }

    [HttpDelete]
    public IActionResult DeleteUser(int userId){
        var user = _userService.DeleteUser(userId);
        if(user is null) return NotFound("No userId found to be deleted");
        return Ok(user);
    }
}