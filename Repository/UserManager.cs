using Microsoft.Data.Sqlite;
using Dapper;

public class UserManger: IUserManger
{
    private string _connectionString;
    public UserManger(string connectionString)
    {
        _connectionString = connectionString;

        using (var connection = new SqliteConnection(_connectionString))
        {
            string CREATE_USER_TABLE = @"create table if not exists user (
	            Id integer primary key AUTOINCREMENT,
	            username text,
	            password text
            );";
            connection.Execute(CREATE_USER_TABLE);
        }
    }
    public IEnumerable<user> GetUsers()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var users = connection.Query<user>(@"select * from user");
            return users;
        }
    }
    public user GetUser(string Name)
    {
        var template = new user { username = Name };
        var parameters = new DynamicParameters(template);
        var sql = @"select * from user where username = @username";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var user = connection.QueryFirstOrDefault<user>(sql, parameters);
            return user;
        }
    }

     public bool AddUser( user user)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
           var insertsql = @" insert into user (username, password)
	                    values (@username, @password);";

            var insertparameters = new user()
                    {
                        username = user.username,
                        password = user.password
                    };
            connection.Execute(insertsql, insertparameters);
            return true;
        }
       return false;
    }

}
