    using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
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
        public async Task<IActionResult> Login(string UserName, string password)
        {
            string message;
            Token token;
            bool resul= _userService.Login(UserName, password, out message, out token);
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
