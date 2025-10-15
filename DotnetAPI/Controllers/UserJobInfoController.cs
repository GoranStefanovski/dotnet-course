namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserJobInfoController : ControllerBase
{
    DataContextDapper _dapper;

    public UserJobInfoController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetAllUserJobInfo")]
    public IEnumerable<UserJobInfo> GetAllUserJobInfo()
    {
        string sql = @"
            SELECT `UserId`,
                   `JobTitle`,
                   `Department`
            FROM dotnetapp.UserJobInfo";
        IEnumerable<UserJobInfo> userJobInfos = _dapper.LoadData<UserJobInfo>(sql);
        return userJobInfos;
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        string sql = @"
            SELECT `UserId`,
                   `JobTitle`,
                   `Department`
            FROM dotnetapp.UserJobInfo
            WHERE UserId = @UserId";
        UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql, new { UserId = userId });
        return userJobInfo;
    }

    [HttpPost("AddUserJobInfo/{userId}")]
    public IActionResult AddUserJobInfo(int userId, UserJobInfoToAddDto userJobInfo)
    {
        // First check if the user exists
        string checkUserSql = "SELECT COUNT(*) FROM dotnetapp.Users WHERE UserId = @UserId";
        var userExists = _dapper.LoadDataSingle<int>(checkUserSql, new { UserId = userId });
        
        if (userExists == 0)
        {
            return NotFound($"User with ID {userId} not found");
        }
        
        string sql = @"
            INSERT INTO dotnetapp.UserJobInfo (UserId, JobTitle, Department)
            VALUES (@UserId, @JobTitle, @Department)";
        
        var parameters = new 
        {
            UserId = userId,  // From route parameter
            JobTitle = userJobInfo.JobTitle,
            Department = userJobInfo.Department
        };
        
        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }
        return BadRequest("Failed to add user job info");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfoUpdateDto userJobInfoUpdate)
    {
        // First get the existing user job info to avoid overwriting with empty values
        string getSql = @"
            SELECT `UserId`, `JobTitle`, `Department`
            FROM dotnetapp.UserJobInfo
            WHERE UserId = @UserId";
            
        UserJobInfo existingUserJobInfo;
        try 
        {
            existingUserJobInfo = _dapper.LoadDataSingle<UserJobInfo>(getSql, new { UserId = userJobInfoUpdate.UserId });
        }
        catch
        {
            return NotFound($"User job info with ID {userJobInfoUpdate.UserId} not found");
        }
        
        // Only update fields that were provided (not null or empty)
        if (!string.IsNullOrEmpty(userJobInfoUpdate.JobTitle)) 
            existingUserJobInfo.JobTitle = userJobInfoUpdate.JobTitle;
        if (!string.IsNullOrEmpty(userJobInfoUpdate.Department)) 
            existingUserJobInfo.Department = userJobInfoUpdate.Department;
        
        string updateSql = @"
            UPDATE dotnetapp.UserJobInfo
            SET JobTitle = @JobTitle,
                Department = @Department
            WHERE UserId = @UserId";
        
        if (_dapper.ExecuteSql(updateSql, existingUserJobInfo))
        {
            return Ok(existingUserJobInfo);
        }
        return BadRequest("Failed to update user job info");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = @"
            DELETE FROM dotnetapp.UserJobInfo
            WHERE UserId = @UserId";
        
        if (_dapper.ExecuteSql(sql, new { UserId = userId }))
        {
            return Ok();
        }
        return BadRequest("Failed to delete user job info");
    }
}