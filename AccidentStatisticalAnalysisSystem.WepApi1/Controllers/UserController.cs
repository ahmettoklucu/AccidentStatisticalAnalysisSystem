using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> Login(string UserName,string Password)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.UserName = UserName;
            loginRequest.Password = Password;
            

            var result= _userService.Login(loginRequest);
            if (result.Result.Success==true)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Result.Token.Id.ToString()),
                    new Claim(ClaimTypes.Role, result.Result.Token.RoleId.ToString()),
                    new Claim(ClaimTypes.Email, result.Result.Token.EMail),
                    new Claim(ClaimTypes.MobilePhone, result.Result.Token.PhoneNumber),
                    new Claim(ClaimTypes.NameIdentifier, result.Result.Token.UserName.ToString()),
                    // Diğer gerekli bilgileri ekleyebilirsiniz.
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok(result.Result.Token.JWT);
            }
            else
            {
                return BadRequest(result.Result.Message);
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
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Result.Token.Id.ToString()),
                    new Claim(ClaimTypes.Role, result.Result.Token.RoleId.ToString()),
                    new Claim(ClaimTypes.Email, result.Result.Token.EMail.ToString()),
                    new Claim(ClaimTypes.MobilePhone, result.Result.Token.PhoneNumber.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, result.Result.Token.UserName.ToString()),
                    // Diğer gerekli bilgileri ekleyebilirsiniz.
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok(result.Result.Token.JWT);
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
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Result.Token.Id.ToString()),
                    new Claim(ClaimTypes.Role, result.Result.Token.RoleId.ToString()),
                    new Claim(ClaimTypes.Email, result.Result.Token.EMail.ToString()),
                    new Claim(ClaimTypes.MobilePhone, result.Result.Token.PhoneNumber.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, result.Result.Token.UserName.ToString()),
                    // Diğer gerekli bilgileri ekleyebilirsiniz.
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok(result.Result.Token.JWT);
            }
            else
            {
                return BadRequest(result.Result.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsyc();  // Asenkron operasyonu bekleyin

            // Direkt olarak JSON serileştirmesi yapabilirsiniz
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
