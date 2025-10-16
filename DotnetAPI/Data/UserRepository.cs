using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFramework;

        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public bool AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd == null) return false;
            
            try
            {
                _entityFramework.Add(entityToAdd);
                return SaveChanges();
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }

        public bool EditEntity<T>(T entityToUpdate)
        {
            if (entityToUpdate == null) return false;
            
            try
            {
                _entityFramework.Update(entityToUpdate);
                return SaveChanges();  // This is the improvement!
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }

        public bool RemoveEntity<T>(T entityToDelete)
        {
            if (entityToDelete == null) return false;
            
            try
            {
                _entityFramework.Remove(entityToDelete);
                return SaveChanges();
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();
            return users;
        }

        public IEnumerable<UserSalary> GetUsersSalaries()
        {
            IEnumerable<UserSalary> userSalaries = _entityFramework.UserSalary.ToList<UserSalary>();
            return userSalaries;
        }

        public IEnumerable<UserJobInfo> GetUsersJobInfo()
        {
            IEnumerable<UserJobInfo> userJobInfos = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
            return userJobInfos;
        }

        public User GetSingleUser(int userId)
        {
            User? user = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault<User>();

            if (user != null)
            {
                return user;
            }

            throw new Exception("Failed to Get User");
        }

        public UserSalary GetSingleUserSalary(int userId)
        {
            UserSalary? userSalary = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserSalary>();

            if (userSalary != null)
            {
                return userSalary;
            }

            throw new Exception("Failed to Get User Salary");
        }

        public UserJobInfo GetSingleUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserJobInfo>();

            if (userJobInfo != null)
            {
                return userJobInfo;
            }

            throw new Exception("Failed to Get User Job Info");
        }
    }
}