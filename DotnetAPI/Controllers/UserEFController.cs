namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    IUserRepository _userRepository;

    public UserEFController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User? GetSingleUser(int userId)
    {
        return _userRepository.GetSingleUser(userId);
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

        if (!_userRepository.AddEntity(userToAdd))
        {
            return BadRequest("Failed to add user");
        }
        
        return Ok();
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(UserUpdateDto userUpdate)
    {
        User? existingUser = _userRepository.GetSingleUser(userUpdate.UserId);
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
        
        if (!_userRepository.EditEntity(existingUser))
        {
            return BadRequest("Failed to update user");
        }
        
        return Ok(existingUser);
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User userToDelete = _userRepository.GetSingleUser(userId);
        if (userToDelete != null)
        {
            if (!_userRepository.RemoveEntity(userToDelete))
            {
                return BadRequest("Failed to delete user");
            }
            return Ok();
        }
        return NotFound($"User with ID {userId} not found");
    }
}