namespace DotnetAPI.Controllers;

using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT `UserId`,
                   `FirstName`,
                   `LastName`,
                   `Email`,
                   `Gender`,
                   `Active`
            FROM dotnetapp.Users";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
            SELECT `UserId`,
                   `FirstName`,
                   `LastName`,
                   `Email`,
                   `Gender`,
                   `Active`
            FROM dotnetapp.Users
            WHERE UserId = @UserId";
        User user = _dapper.LoadDataSingle<User>(sql, new { UserId = userId });
        return user;
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
            INSERT INTO dotnetapp.Users (FirstName, LastName, Email, Gender, Active)
            VALUES (@FirstName, @LastName, @Email, @Gender, @Active)";
        
        if (_dapper.ExecuteSql(sql, user))
        {
            return Ok();
        }
        return BadRequest("Failed to add user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        // First get the existing user to avoid overwriting with empty values
        string getSql = @"
            SELECT `UserId`, `FirstName`, `LastName`, `Email`, `Gender`, `Active`
            FROM dotnetapp.Users
            WHERE UserId = @UserId";
            
        User existingUser;
        try 
        {
            existingUser = _dapper.LoadDataSingle<User>(getSql, new { UserId = user.UserId });
        }
        catch
        {
            return NotFound($"User with ID {user.UserId} not found");
        }
        
        // Only update fields that were provided (not null or empty)
        if (!string.IsNullOrEmpty(user.FirstName)) existingUser.FirstName = user.FirstName;
        if (!string.IsNullOrEmpty(user.LastName)) existingUser.LastName = user.LastName;
        if (!string.IsNullOrEmpty(user.Email)) existingUser.Email = user.Email;
        if (!string.IsNullOrEmpty(user.Gender)) existingUser.Gender = user.Gender;
        
        // For boolean, we always update since false is a valid value
        existingUser.Active = user.Active;
        
        string updateSql = @"
            UPDATE dotnetapp.Users
            SET FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Gender = @Gender,
                Active = @Active
            WHERE UserId = @UserId";
        
        if (_dapper.ExecuteSql(updateSql, existingUser))
        {
            return Ok(existingUser);
        }
        return BadRequest("Failed to update user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM dotnetapp.Users
            WHERE UserId = @UserId";
        
        if (_dapper.ExecuteSql(sql, new { UserId = userId }))
        {
            return Ok();
        }
        return BadRequest("Failed to delete user");
    }
}