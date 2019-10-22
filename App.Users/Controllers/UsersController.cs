using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using App.Users.Services;

namespace App.Users.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUsersManager _usersManager;
        public UsersController(
            IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        [HttpGet("active")]
        public ActionResult<IEnumerable<string>> GetListActiveUers()
        {
            var serviceCallResult = _usersManager.GetListActiveUsers().ToList();
            return serviceCallResult;
        }

        [HttpPost]
        public void ResetPassword(int id, string pass)
        {
            _usersManager.ResetPassword(id, pass);
        }
        [HttpPost]
        public void BlockOrUnblockUserById(int id)
        {
            _usersManager.BlockOrUnblockUserById(id);
        }
    }
}
