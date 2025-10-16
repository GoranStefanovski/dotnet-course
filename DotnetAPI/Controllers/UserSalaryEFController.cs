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
        IEnumerable<UserSalary> userSalaries = _entityFramework.UserSalary.ToList<UserSalary>();
        return userSalaries;
    }

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary? GetUserSalary(int userId)
    {
        UserSalary? userSalary = _entityFramework.UserSalary.Find(userId);
        return userSalary;
    }
   
    [HttpPost("AddUserSalary/{userId}")]
    public IActionResult AddUserSalary(int userId, UserSalaryToAddDto userSalary)
    {
        // First check if the user exists
        User? existingUser = _entityFramework.Users.Find(userId);
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

        _userRepository.AddEntity<UserSalary>(userSalaryToAdd);
        _entityFramework.SaveChanges();
        return Ok();
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalaryUpdateDto userSalaryUpdate)
    {
      UserSalary? existingUserSalary = _entityFramework.UserSalary.Find(userSalaryUpdate.UserId);
        if (existingUserSalary == null)
        {
            return NotFound($"User with ID {userSalaryUpdate.UserId} not found");
        }
        
        // Only update fields that were provided (not null)
        if (userSalaryUpdate.Salary.HasValue) 
            existingUserSalary.Salary = userSalaryUpdate.Salary.Value;
        if (userSalaryUpdate.AvgSalary.HasValue) 
            existingUserSalary.AvgSalary = userSalaryUpdate.AvgSalary.Value;
       
        
        _entityFramework.SaveChanges();
        return Ok(existingUserSalary);
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary userSalaryToDelete = _entityFramework.UserSalary.Find(userId);
        if (userSalaryToDelete != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userSalaryToDelete);
            _entityFramework.SaveChanges();
            return Ok();
        }
        return NotFound($"User Salary with UserId: {userId} not found");
    }
}