using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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
        [AllowAnonymous]
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
            var addedProduct = await  _userService.AddAsyc(user);
            if (addedProduct.Success==true)
            {
                return Ok(addedProduct.Message);
            }
            else
            {
                return BadRequest(addedProduct.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {

            var result=  _userService.Login(loginRequest);
            if (result.Success==true)
            {
                var responseData = new { Token = result.Token.JWT, result.Token.RoleId };
                HttpContext.Response.Headers.TryAdd("Authorization", result.Token.JWT);
                return Ok(responseData);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> PhoneLogin(string UserName, string Password)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.UserName = UserName;
            loginRequest.Password = Password;

            var result = _userService.PhoneLogin(loginRequest);
            if (result.Result.Success == true)
            {
                return Ok(new  {Token= result.Result.Token.JWT });
            }
            else
            {
                return BadRequest(result.Result.Message);
            }
        }
        [AllowAnonymous]
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
        //[Authorize(AuthenticationSchemes = "Bearer",Roles ="1")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsyc(); 
            var users = JsonConvert.DeserializeObject<List<UserResponseModele>>(result.ToString());
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(users, settings);
            return Ok(json);
        }
    }
}
