using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();
        public bool AddEntity<T>(T entityToAdd);
        public bool EditEntity<T>(T entityToUpdate);
        public bool RemoveEntity<T>(T entityToDelete);
        public IEnumerable<User> GetUsers();
        public IEnumerable<UserSalary> GetUsersSalaries();
        public IEnumerable<UserJobInfo> GetUsersJobInfo();
        public User GetSingleUser(int userId);
        public UserSalary GetSingleUserSalary(int userId);
        public UserJobInfo GetSingleUserJobInfo(int userId);
    }
} 