using Microsoft.AspNetCore.Mvc;
using POC.BusinessLogic.Interfaces;
using POC.DataModel;

namespace POCAPI.Controllers
{
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserManagement _userManagement;
        public UserController(IUserManagement userManagement) 
        {
            _userManagement = userManagement;
        }

        [HttpGet]
        [Route("api/getUsers/{userId}")]
        public ActionResult GetUsers(string userId)
        {
            List<UserModel> users = _userManagement.GetUsers(Convert.ToInt32(userId));
            return Ok(users);
        }

    }
}
