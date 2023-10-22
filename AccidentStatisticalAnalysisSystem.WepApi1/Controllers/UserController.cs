    using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccidentStatisticalAnalysisSystem.WepApi.Controllers
{

    [Route("api/[controller]/[action]")]
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
        [HttpGet]
        public async Task<IActionResult> EMailLogin(string Email,string password)
        {
            string message;
            Token token;
            bool resul= _userService.EmailLogin(Email, password, out message, out token);
            if (resul==true)
            {
                return Ok(token.JWT);
            }
            else
            {
                return BadRequest(message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> PhoneLogin(string Phone, string password)
        {
            string message;
            Token token;
            bool resul = _userService.PhoneLogin(Phone, password, out message, out token);
            if (resul == true)
            {
                return Ok(token.JWT);
            }
            else
            {
                return BadRequest(message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserNameLogin(string UserName, string password)
        {
            string message;
            Token token;
            bool resul = _userService.UserNameLogin(UserName, password, out message, out token);
            if (resul == true)
            {
                return Ok(token.JWT);
            }
            else
            {
                return BadRequest(message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result=_userService.GetAllAsyc();

                return Ok(result.Result);
  

            
        }
    }
}
