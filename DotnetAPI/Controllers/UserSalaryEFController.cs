namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserSalaryEFController : ControllerBase
{
    DataContextEF _entityFramework;

    IUserRepository _userRepository;

    public UserSalaryEFController(IConfiguration config, IUserRepository userRepository)
    {
        _entityFramework = new DataContextEF(config);

        _userRepository = userRepository;
    }

   [HttpGet("GetAllUserSalary")]
    public IEnumerable<UserSalary> GetAllUserSalary()
    {
        IEnumerable<UserSalary> userSalaries = _userRepository.GetUsersSalaries();
        return userSalaries;
    }

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary? GetUserSalary(int userId)
    {
        return _userRepository.GetSingleUserSalary(userId);
    }
   
    [HttpPost("AddUserSalary/{userId}")]
    public IActionResult AddUserSalary(int userId, UserSalaryToAddDto userSalary)
    {
        // First check if the user exists
        User? existingUser = _userRepository.GetSingleUser(userId);
        if (existingUser == null)
        {
            return NotFound($"User with ID {userId} not found");
        }

        // Create the UserSalary model (not DTO)
        UserSalary userSalaryToAdd = new UserSalary()
        {
            UserId = userId,  // Set the UserId from route parameter
            Salary = userSalary.Salary,      // Convert int to decimal
            AvgSalary = userSalary.AvgSalary // Convert int to decimal
        };
        
        if (!_userRepository.AddEntity(userSalaryToAdd))
        {
            return BadRequest("Failed to add user salary");
        }
        
        return Ok();
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalaryUpdateDto userSalaryUpdate)
    {
      UserSalary? existingUserSalary = _userRepository.GetSingleUserSalary(userSalaryUpdate.UserId);
        if (existingUserSalary == null)
        {
            return NotFound($"User with ID {userSalaryUpdate.UserId} not found");
        }
        
        // Only update fields that were provided (not null)
        if (userSalaryUpdate.Salary.HasValue) 
            existingUserSalary.Salary = userSalaryUpdate.Salary.Value;
        if (userSalaryUpdate.AvgSalary.HasValue) 
            existingUserSalary.AvgSalary = userSalaryUpdate.AvgSalary.Value;
       
        // NEW WAY:
        if (!_userRepository.EditEntity(existingUserSalary))
        {
            return BadRequest("Failed to update user salary");
        }
        
        return Ok(existingUserSalary);
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary userSalaryToDelete = _userRepository.GetSingleUserSalary(userId);
        if (userSalaryToDelete != null)
        {
            if (!_userRepository.RemoveEntity(userSalaryToDelete))
            {
                return BadRequest("Failed to delete user");
            }
            return Ok();
        }
        return NotFound($"User Salary with UserId: {userId} not found");
    }
}