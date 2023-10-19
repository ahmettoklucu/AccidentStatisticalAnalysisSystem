using Microsoft.AspNetCore.Mvc;

namespace AccidentStatisticalAnalysisSystem.WepApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController() 
        { 

        }

    }
}
