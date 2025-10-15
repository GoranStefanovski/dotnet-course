using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace DotnetAPI.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }

        public IEnumerable<T> LoadData<T>(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql, parameters);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }

        public T LoadDataSingle<T>(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql, parameters);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql) > 0;
        }

        public bool ExecuteSql(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql, parameters) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql);
        }

        public int ExecuteSqlWithRowCount(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql, parameters);
        }
    }
}