using POC.DataAccess;
namespace POC.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<UserDataAccess> GetUsers(int otherthanUser);

        public UserDataAccess GetUser(string userName, string password);
    }
}
