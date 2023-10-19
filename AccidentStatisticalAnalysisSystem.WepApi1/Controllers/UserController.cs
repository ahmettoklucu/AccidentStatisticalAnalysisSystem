using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
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
        [HttpPost]
        public async Task<IActionResult> Register(string Name,string SureName,string UserName,string Password,string PhoneNumber,string EMail)
        {
            User user=new User();
            user.Name = Name;
            user.SureName = SureName;
            user.Password = Password;
            user.PhoneNumber = PhoneNumber;
            user.EMail = EMail; 
            user.UserName = UserName;
            bool addedProduct =  _userService.AddAsyc(user,out string message);
            if (addedProduct==true)
            {
                return Ok(message);

            }
            else
            {
                return BadRequest(message);
            }


        }
    }
}
