using POC.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShows.Data;

namespace POC.BusinessLogic.Interfaces
{
    public interface IUserManagement
    {
        public List<UserModel> GetUsers(int otherthanUser);

        public UserModel GetUser(Login login);
    }
}
