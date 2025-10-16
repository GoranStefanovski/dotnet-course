namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    IUserRepository _userRepository;

    public UserJobInfoEFController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

   [HttpGet("GetAllUserJorInfo")]
    public IEnumerable<UserJobInfo> GetAllUserJorInfo()
    {
        IEnumerable<UserJobInfo> userJobInfos = _userRepository.GetUsersJobInfo();
        return userJobInfos;
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo? GetUserJobInfo(int userId)
    {
        return _userRepository.GetSingleUserJobInfo(userId);
    }
   
    [HttpPost("AddUserJobInfo/{userId}")]
    public IActionResult AddUserJobInfo(int userId, UserJobInfoToAddDto UserJobInfo)
    {
        // First check if the user exists
        User? existingUser = _userRepository.GetSingleUser(userId);
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

        if (!_userRepository.AddEntity(UserJobInfoToAdd))
        {
            return BadRequest("Failed to add user");
        }
        
        return Ok();
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfoUpdateDto UserJobInfoUpdate)
    {
        UserJobInfo? existingUserJobInfo = _userRepository.GetSingleUserJobInfo(UserJobInfoUpdate.UserId);
        if (existingUserJobInfo == null)
        {
            return NotFound($"User with ID {UserJobInfoUpdate.UserId} not found");
        }
        
        // Only update fields that were provided (not null)
        if (UserJobInfoUpdate.JobTitle != null) 
            existingUserJobInfo.JobTitle = UserJobInfoUpdate.JobTitle;
        if (UserJobInfoUpdate.Department != null) 
            existingUserJobInfo.Department = UserJobInfoUpdate.Department;
       
        if (!_userRepository.EditEntity(existingUserJobInfo))
        {
            return BadRequest("Failed to update user job info");
        }
        
        return Ok(existingUserJobInfo);
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo UserJobInfoToDelete = _userRepository.GetSingleUserJobInfo(userId);
        if (UserJobInfoToDelete != null)
        {
            if (!_userRepository.RemoveEntity(UserJobInfoToDelete))
            {
                return BadRequest("Failed to delete user");
            }
            return Ok();
        }
        return NotFound($"User Info with UserId: {userId} not found");
    }
}