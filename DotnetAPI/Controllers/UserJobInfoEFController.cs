namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IUserRepository _userRepository;

    public UserJobInfoEFController(IConfiguration config, IUserRepository userRepository)
    {
        _entityFramework = new DataContextEF(config);
        _userRepository = userRepository;
    }

   [HttpGet("GetAllUserJorInfo")]
    public IEnumerable<UserJobInfo> GetAllUserJorInfo()
    {
        IEnumerable<UserJobInfo> userJobInfos = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return userJobInfos;
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo? GetUserJobInfo(int userId)
    {
        UserJobInfo? UserJobInfo = _entityFramework.UserJobInfo.Find(userId);
        return UserJobInfo;
    }
   
    [HttpPost("AddUserJobInfo/{userId}")]
    public IActionResult AddUserJobInfo(int userId, UserJobInfoToAddDto UserJobInfo)
    {
        // First check if the user exists
        User? existingUser = _entityFramework.Users.Find(userId);
        if (existingUser == null)
        {
            return NotFound($"User with ID {userId} not found");
        }
        
        // Create the UserJobInfo model (not DTO)
        UserJobInfo UserJobInfoToAdd = new UserJobInfo()
        {
            UserId = userId,  // Set the UserId from route parameter
            JobTitle = UserJobInfo.JobTitle,
            Department = UserJobInfo.Department
        };

        _userRepository.AddEntity<UserJobInfo>(UserJobInfoToAdd);
        _entityFramework.SaveChanges();
        return Ok();
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfoUpdateDto UserJobInfoUpdate)
    {
      UserJobInfo? existingUserJobInfo = _entityFramework.UserJobInfo.Find(UserJobInfoUpdate.UserId);
        if (existingUserJobInfo == null)
        {
            return NotFound($"User with ID {UserJobInfoUpdate.UserId} not found");
        }
        
        // Only update fields that were provided (not null)
        if (UserJobInfoUpdate.JobTitle != null) 
            existingUserJobInfo.JobTitle = UserJobInfoUpdate.JobTitle;
        if (UserJobInfoUpdate.Department != null) 
            existingUserJobInfo.Department = UserJobInfoUpdate.Department;
       
        
        _entityFramework.SaveChanges();
        return Ok(existingUserJobInfo);
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo UserJobInfoToDelete = _entityFramework.UserJobInfo.Find(userId);
        if (UserJobInfoToDelete != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(UserJobInfoToDelete);
            _entityFramework.SaveChanges();
            return Ok();
        }
        return NotFound($"User Info with UserId: {userId} not found");
    }
}