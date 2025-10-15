namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserSalaryController : ControllerBase
{
    DataContextDapper _dapper;

    public UserSalaryController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

   [HttpGet("GetAllUserSalary")]
    public IEnumerable<UserSalary> GetAllUserSalary()
    {
        string sql = @"
            SELECT `UserId`,
                   `Salary`,
                   `AvgSalary`
            FROM dotnetapp.UserSalary";
        IEnumerable<UserSalary> userSalarys = _dapper.LoadData<UserSalary>(sql);
        return userSalarys;
    }

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        string sql = @"
            SELECT `UserId`,
                   `Salary`,
                   `AvgSalary`
            FROM dotnetapp.UserSalary
            WHERE UserId = @UserId";
        UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql, new { UserId = userId });
        return userSalary;
    }
   
    [HttpPost("AddUserSalary/{userId}")]
    public IActionResult AddUserSalary(int userId, UserSalaryToAddDto UserSalary)
    {
        // First check if the user exists
        string checkUserSql = "SELECT COUNT(*) FROM dotnetapp.Users WHERE UserId = @UserId";
        var userExists = _dapper.LoadDataSingle<int>(checkUserSql, new { UserId = userId });
        
        if (userExists == 0)
        {
            return NotFound($"User with ID {userId} not found");
        }
        
        string sql = @"
            INSERT INTO dotnetapp.UserSalary (UserId, Salary, AvgSalary)
            VALUES (@UserId, @Salary, @AvgSalary)";
        
        var parameters = new 
        {
            UserId = userId,  // From route parameter
            Salary = UserSalary.Salary,
            AvgSalary = UserSalary.AvgSalary
        };
        
        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }
        return BadRequest("Failed to add user salary info");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalaryUpdateDto userSalaryUpdate)
    {
        // First get the existing user job info to avoid overwriting with empty values
        string getSql = @"
            SELECT `UserId`, `Salary`, `AvgSalary`
            FROM dotnetapp.UserSalary
            WHERE UserId = @UserId";
            
        UserSalary existingUserSalary;
        try 
        {
            existingUserSalary = _dapper.LoadDataSingle<UserSalary>(getSql, new { UserId = userSalaryUpdate.UserId });
        }
        catch
        {
            return NotFound($"User job info with ID {userSalaryUpdate.UserId} not found");
        }
        
        // Only update fields that were provided (not null)
        if (userSalaryUpdate.Salary.HasValue) 
            existingUserSalary.Salary = userSalaryUpdate.Salary.Value;
        if (userSalaryUpdate.AvgSalary.HasValue) 
            existingUserSalary.AvgSalary = userSalaryUpdate.AvgSalary.Value;
        
        string updateSql = @"
            UPDATE dotnetapp.UserSalary
            SET Salary = @Salary,
                AvgSalary = @AvgSalary
            WHERE UserId = @UserId";
        
        if (_dapper.ExecuteSql(updateSql, existingUserSalary))
        {
            return Ok(existingUserSalary);
        }
        return BadRequest("Failed to update user job info");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"
            DELETE FROM dotnetapp.UserSalary
            WHERE UserId = @UserId";
        
        if (_dapper.ExecuteSql(sql, new { UserId = userId }))
        {
            return Ok();
        }
        return BadRequest("Failed to delete user job info");
    }
}