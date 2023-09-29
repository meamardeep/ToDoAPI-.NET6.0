using POC.BusinessLogic.Interfaces;
using POC.DataAccess;
using POC.DataModel;
using POC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShows.Data;

namespace POC.BusinessLogic
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;
        public UserManagement(IUserRepository userRepository) 
        {
          _userRepository = userRepository;
        }

        public UserModel GetUser(Login login)
        {
            UserDataAccess user = _userRepository.GetUser(login.UserName, login.Password);
            return new UserModel()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FullName = user.UserName,
            };
        }

        public List<UserModel> GetUsers(int otherthanUser)
        {
           List<UserDataAccess> userDataAccess = _userRepository.GetUsers(otherthanUser);
           List<UserModel> users = new List<UserModel>();
           foreach(var item in userDataAccess)
           {
                users.Add(new UserModel()
                {
                    FullName = item.UserName,
                    UserId = item.UserId,
                    UserName = item.UserName,
                });
           }

           return users;
        }

    }
}
