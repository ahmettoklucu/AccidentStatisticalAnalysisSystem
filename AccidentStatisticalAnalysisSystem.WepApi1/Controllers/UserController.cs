using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccidentStatisticalAnalysisSystem.WepApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController()
        {
            _userService = new UserManager(new EfUserDal());
        }

    }
}
