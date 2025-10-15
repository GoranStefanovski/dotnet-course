namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;

    public UserEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
       IEnumerable<User> users = _entityFramework.Users.ToList<User>();
       return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User? GetSingleUser(int userId)
    {
        User? user = _entityFramework.Users.Find(userId);
        return user;
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
      User userToAdd = new User()
      {
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Gender = user.Gender,
        Active = user.Active
      };
      _entityFramework.Users.Add(userToAdd);
      _entityFramework.SaveChanges();
      return Ok();
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(UserUpdateDto userUpdate)
    {
        User? existingUser = _entityFramework.Users.Find(userUpdate.UserId);
        if (existingUser == null)
        {
            return NotFound($"User with ID {userUpdate.UserId} not found");
        }
        
        // Only update fields that were provided (not null)
        if (userUpdate.FirstName != null) 
            existingUser.FirstName = userUpdate.FirstName;
        if (userUpdate.LastName != null) 
            existingUser.LastName = userUpdate.LastName;
        if (userUpdate.Email != null) 
            existingUser.Email = userUpdate.Email;
        if (userUpdate.Gender != null) 
            existingUser.Gender = userUpdate.Gender;
        if (userUpdate.Active.HasValue) 
            existingUser.Active = userUpdate.Active.Value;
        
        _entityFramework.SaveChanges();
        return Ok(existingUser);
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User userToDelete = _entityFramework.Users.Find(userId);
        if (userToDelete != null)
        {
            _entityFramework.Users.Remove(userToDelete);
            _entityFramework.SaveChanges();
            return Ok();
        }
        return NotFound($"User with ID {userId} not found");
    }
}