public interface IUserManger
{
    IEnumerable<user> GetUsers();
    user GetUser(string sName);
    bool AddUser( user user);
}