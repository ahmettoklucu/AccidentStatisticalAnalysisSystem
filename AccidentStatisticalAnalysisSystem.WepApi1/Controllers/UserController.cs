using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Repository;
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
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserResponseModele userResponseModele)
        {
            var addedProduct = await  _userService.AddAsyc(userResponseModele);
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

            var result= _userService.Login(loginRequest);
            if (result.Success==true)
            {
                LoginResult loginResult = new LoginResult();
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
        public async Task<IActionResult> PhoneLogin(LoginRequest loginRequest)
        {
           

            var result = _userService.PhoneLogin(loginRequest);
            if (result.Success == true)
            {
                return Ok(new  {Token= result.Token.JWT });
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UserNameLogin(LoginRequest loginRequest)
        {

            var result = _userService.UserNameLogin(loginRequest);
            if (result.Success == true)
            {
                
                return Ok(result.Token.JWT);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async  Task<IActionResult> RenavalToken([FromBody] TokenRequestModele tokenRequest)
        {
            var loginResult = new LoginResult
            {
                Success = false,
                Message = "",
                Token = new Token()
            };
            try
            {
                var result = _userService.TokenRenewal(tokenRequest.Token, ref loginResult);
                if (result == true)
                {
                    var responseData = new { Token = loginResult.Token.JWT};
                    HttpContext.Response.Headers.TryAdd("Authorization", loginResult.Token.JWT);
                    return Ok(loginResult.Token.JWT);
                }
            }
            catch(Exception ex)
            {
                loginResult.Message = ex.Message;
            }
            return BadRequest(loginResult.Message);
        }
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="1")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await _userService.GetAllAsyc();
            return Ok(result);

        }
    }
}
