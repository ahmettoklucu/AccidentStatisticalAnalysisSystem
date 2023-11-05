using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AccidentStatisticalAnalysisSystem.WepApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController()
        {
            _userService = new UserManager(new EfUserDal());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserResponseModele userResponseModele)
        {
            User user=new User();
            user.Name = userResponseModele.Name;
            user.SureName = userResponseModele.SureName;
            user.Password = userResponseModele.Password;
            user.PhoneNumber = userResponseModele.PhoneNumber;
            user.EMail = userResponseModele.EMail; 
            user.UserName = userResponseModele.UserName;
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
        [HttpPost]
        public async Task<IActionResult> Login(string UserName,string Password)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.UserName = UserName;
            loginRequest.Password = Password;
            

            var result= _userService.Login(loginRequest);
            if (result.Result.Success==true)
            {
                return Ok(result.Result.Token.JWT);
            }
            else
            {
                return BadRequest(result.Result.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> PhoneLogin(string UserName, string Password)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.UserName = UserName;
            loginRequest.Password = Password;

            var result = _userService.PhoneLogin(loginRequest);
            if (result.Result.Success == true)
            {
                return Ok(result.Result.Token.JWT);
            }
            else
            {
                return BadRequest(result.Result.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserNameLogin(string UserName, string password)
        {
            var loginRequest = new LoginRequest();
            loginRequest.UserName = UserName;
            loginRequest.Password = password;
            var result = _userService.UserNameLogin(loginRequest);
            if (result.Result.Success == true)
            {
                return Ok(result.Result.Token.JWT);
            }
            else
            {
                return BadRequest(result.Result.Message);
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
