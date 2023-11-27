using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.Bussiness.Utilities;
using AccidentStatisticalAnalysisSystem.Bussiness.ValidationRules.FluentValidation;
using AccidentStatisticalAnalysisSystem.DataAccess.Abstract;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly HttpContext _httpContext;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        private string ComputeSHA256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool VerifySHA256Hash(string rawData, string hash)
        {
            string hashOfInput = ComputeSHA256Hash(rawData);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (comparer.Compare(hashOfInput, hash) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ResultModele> AddAsyc(User user)
        {
            ResultModele resultModele = new ResultModele();
            resultModele.Success = false;
            resultModele.Message = "";
            try
            {
                var UserName = _userDal.GetAsyc(p => p.UserName == user.UserName);
                if (UserName.Result != null)
                {
                    resultModele.Message = "Kullanıcı adi sistemde kayıtlıdır.";
                }
                else if (_userDal.GetAsyc(p => p.PhoneNumber == user.PhoneNumber).Result != null)
                {
                    resultModele.Message = "Telefon numarası sistemde kayıtlıdır.";
                }
                else if (_userDal.GetAsyc(p => p.EMail == user.EMail).Result != null)
                {
                    resultModele.Message = "EMail sistemde kayıtlıdır.";
                }
                else
                {
                    ValidationTool.Validate(new UserValidator(), user);
                    user.Password = ComputeSHA256Hash(user.Password);
                    _userDal.AddAsyc(user);
                    resultModele.Message = "Kayıt işlemi başarili";
                    resultModele.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultModele.Message = ex.Message;
                resultModele.Success = false;

            }
            return resultModele;
        }
        public async Task<ResultModele> DeleteAsyc(User user)
        {
            ResultModele resultModele = new ResultModele();
            try
            {
                if (user == null)
                {
                    resultModele.Message = "Kullanıcı bulunamadı.";
                    resultModele.Success = false;
                }
                else
                {
                    _userDal.DeleteAsyc(user);
                    resultModele.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultModele.Message = "Kullanıcı silinemedi" + ex.Message;
                resultModele.Success = false;
            }
            return resultModele;
        }
        public async Task<UserResponseModele> GetAsyc(Guid UserId)
        {
            UserResponseModele userResponseModele = new UserResponseModele();
            try
            {
                var item = _userDal.GetAsyc(p => p.Id == UserId);
                userResponseModele.Name = item.Result.Name;
                userResponseModele.PhoneNumber = item.Result.PhoneNumber;
                userResponseModele.SureName = item.Result.SureName;
                userResponseModele.RoleId = item.Result.RoleId;
                userResponseModele.EMail = item.Result.EMail;
                userResponseModele.Id = item.Result.Id;
                userResponseModele.IsDelete = item.Result.IsDelete;
                userResponseModele.StarDate = item.Result.StarDate;
                userResponseModele.Password = item.Result.Password;
                userResponseModele.SecretKey = item.Result.SecretKey;
                userResponseModele.UserName = item.Result.UserName;
            }
            catch (Exception)
            {
                userResponseModele = null;
            }
            return userResponseModele;

        }
        public async Task<string> GetAllAsyc()
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            try
            {
                var Users = await _userDal.GetAllAsyc();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(Users));
                foreach (var item in users)
                {
                    UserResponseModele userResponseModele = new UserResponseModele
                    {
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        SureName = item.SureName,
                        RoleId = item.RoleId,
                        EMail = item.EMail,
                        Id = item.Id,
                        IsDelete = item.IsDelete,
                        StarDate = item.StarDate,
                        Password = item.Password,
                        SecretKey = item.SecretKey,
                        UserName = item.UserName
                    };
                    userResponseModeles.Add(userResponseModele);
                }
                return JsonConvert.SerializeObject(userResponseModeles);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> GetUserByUserNameAsyc(string userName)
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            var Users = await _userDal.GetAllAsyc(p => p.UserName.ToLower().Contains(userName.ToLower()));
            List<User> users = JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(Users));
            foreach (var item in users)
            {
                UserResponseModele userResponseModele = new UserResponseModele
                {
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    SureName = item.SureName,
                    RoleId = item.RoleId,
                    EMail = item.EMail,
                    Id = item.Id,
                    IsDelete = item.IsDelete,
                    StarDate = item.StarDate,
                    Password = item.Password,
                    SecretKey = item.SecretKey,
                    UserName = item.UserName
                };
                userResponseModeles.Add(userResponseModele);
            }
            return JsonConvert.SerializeObject(userResponseModeles);
        }
        public async Task<ResultModele> UpdateAsyc(User user)
        {
            ResultModele resultModele = new ResultModele();
            try
            {
                resultModele.Success = false;
                resultModele.Message = "";
                if (_userDal.GetAsyc(p => p.UserName == user.UserName && p.Id != user.Id) != null)
                {
                    resultModele.Message = "Kullanıcı adi sistemde kayıtlıdır.";
                    resultModele.Success = false;
                }
                else if (_userDal.GetAsyc(p => p.PhoneNumber == user.EMail && p.Id != user.Id) != null)
                {
                    resultModele.Message = "Telefon numarası sistemde kayıtlıdır.";
                    resultModele.Success = false;
                }
                else if (_userDal.GetAsyc(p => p.EMail == user.EMail && p.Id != user.Id) != null)
                {
                    resultModele.Message = "EMail sistemde kayıtlıdır.";
                    resultModele.Success = false;
                }
                else
                {

                    ValidationTool.Validate(new UserValidator(), user);
                    user.Password = ComputeSHA256Hash(user.Password);
                    _userDal.UpdateAsyc(user);
                    resultModele.Message = "Kullanıcı ekleme işlemi başarili";
                    resultModele.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultModele.Message = "HATA" + ex.Message;
                resultModele.Success = false;
            }
            return resultModele;
        }
        public async Task<LoginResult> EmailLogin(LoginRequest loginRequest, HttpContext httpContext)
        {
            var loginResult = new LoginResult();
            try
            {
                loginResult.Success = false;
                loginResult.Message = "";
                loginResult.Token = null;
                var User = _userDal.GetAsyc(p => p.PhoneNumber == loginRequest.UserName);
                if (User != null)
                {
                    if (VerifySHA256Hash(loginRequest.Password, User.Result.Password) == false)
                    {
                        loginResult.Message = "Şifre hatalı tekrar deneyiniz.";
                        User = null;
                    }
                    else
                    {
                        loginResult.Message = "Giriş başarili.";
                        loginResult.Success = true;
                        UserResponseModele userResponseModele = new UserResponseModele();
                        userResponseModele.Name = User.Result.Name;
                        userResponseModele.SureName = User.Result.SureName;
                        userResponseModele.PhoneNumber = User.Result.PhoneNumber;
                        userResponseModele.EMail = User.Result.EMail;
                        userResponseModele.Id = User.Result.Id;
                        userResponseModele.IsDelete = User.Result.IsDelete;
                        userResponseModele.StarDate = User.Result.StarDate;
                        userResponseModele.SecretKey = User.Result.SecretKey;
                        userResponseModele.UserName = User.Result.UserName;
                        userResponseModele.Password = User.Result.Password;
                        userResponseModele.RoleId = User.Result.RoleId; // Yeni bir LoginResult nesnesi oluşturun.
                        TokenProcess.GenerateToken(httpContext, ref loginResult, userResponseModele, 25);
                        loginResult.Token.JWT = loginResult.Token.JWT;
                        loginResult.Token.ValidMinute = loginResult.Token.ValidMinute;
                        loginResult.Token.ValidityDatetime = loginResult.Token.ValidityDatetime;
                    }

                }
                else
                {
                    loginResult.Message = "Bu telefon sistemde bulunmamaktadir.";

                }
            }
            catch (Exception e)
            {
                loginResult.Token = null;
                loginResult.Message = "HATA" + e.Message;
                loginResult.Success = false;
            };
            return loginResult;
        }
        public async Task<LoginResult> PhoneLogin(LoginRequest loginRequest, HttpContext httpContext)
        {
            var loginResult = new LoginResult();
            try
            {
                loginResult.Success = false;
                loginResult.Message = "";
                loginResult.Token = null;
                var User = await _userDal.GetAsyc(p => p.PhoneNumber == loginRequest.UserName);
                if (User != null)
                {
                    if (VerifySHA256Hash(loginRequest.Password, User.Password) == false)
                    {
                        loginResult.Message = "Şifre hatalı tekrar deneyiniz.";
                        User = null;
                    }
                    else
                    {
                        loginResult.Message = "Giriş başarili.";
                        loginResult.Success = true;

                        UserResponseModele userResponseModele = new UserResponseModele();
                        userResponseModele.Name = User.Name;
                        userResponseModele.SureName = User.SureName;
                        userResponseModele.PhoneNumber = User.PhoneNumber;
                        userResponseModele.EMail = User.EMail;
                        userResponseModele.Id = User.Id;
                        userResponseModele.IsDelete = User.IsDelete;
                        userResponseModele.StarDate = User.StarDate;
                        userResponseModele.SecretKey = User.SecretKey;
                        userResponseModele.UserName = User.UserName;
                        userResponseModele.Password = User.Password;
                        userResponseModele.RoleId = User.RoleId;
                        TokenProcess.GenerateToken(httpContext, ref loginResult, userResponseModele, 25);
                        loginResult.Token.JWT = loginResult.Token.JWT;
                        loginResult.Token.ValidMinute = loginResult.Token.ValidMinute;
                        loginResult.Token.ValidityDatetime = loginResult.Token.ValidityDatetime;
                    }

                }
                else
                {
                    loginResult.Message = "Bu telefon sistemde bulunmamaktadir.";
                }
            }
            catch (Exception e)
            {
                loginResult.Token = null;
                loginResult.Message = "HATA" + e.Message;
                loginResult.Success = false;
            };
            return loginResult;
        }
        public async Task<LoginResult> UserNameLogin(LoginRequest loginRequest, HttpContext httpContext)
        {
            var loginResult = new LoginResult();
            try
            {
                loginResult.Success = false;
                loginResult.Message = "";
                loginResult.Token = null;
                var User = await _userDal.GetAsyc(p => p.UserName == loginRequest.UserName);
                if (User != null)
                {
                    if (VerifySHA256Hash(loginRequest.Password, User.Password) == false)
                    {
                        loginResult.Message = "Şifre hatalı tekrar deneyiniz.";
                        User = null;
                    }
                    else
                    {
                        loginResult.Message = "Giriş başarili.";
                        loginResult.Success = true;
                        UserResponseModele userResponseModele = new UserResponseModele();
                        userResponseModele.Name = User.Name;
                        userResponseModele.SureName = User.SureName;
                        userResponseModele.PhoneNumber = User.PhoneNumber;
                        userResponseModele.EMail = User.EMail;
                        userResponseModele.Id = User.Id;
                        userResponseModele.IsDelete = User.IsDelete;
                        userResponseModele.StarDate = User.StarDate;
                        userResponseModele.SecretKey = User.SecretKey;
                        userResponseModele.UserName = User.UserName;
                        userResponseModele.Password = User.Password;
                        userResponseModele.RoleId = User.RoleId;
                        userResponseModele.RoleId = User.RoleId; // Yeni bir LoginResult nesnesi oluşturun.
                        TokenProcess.GenerateToken(httpContext, ref loginResult, userResponseModele, 25);
                        loginResult.Token.JWT = loginResult.Token.JWT;
                        loginResult.Token.ValidMinute = loginResult.Token.ValidMinute;
                        loginResult.Token.ValidityDatetime = loginResult.Token.ValidityDatetime;
                    }
                }
                else
                {
                    loginResult.Message = "Bu Kullanıcı ismi sistemde bulunmamaktadir.";
                }
            }
            catch (Exception e)
            {
                loginResult.Token = null;
                loginResult.Message = "HATA" + e.Message;
                loginResult.Success = false;
            };
            return loginResult;
        }
        public async Task<string> GetUserByEMailAsyc(string EMail)
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            var Users = await _userDal.GetAllAsyc(p => p.EMail.ToLower().Contains(EMail.ToLower()));
            List<User> users = JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(Users));
            foreach (var item in users)
            {
                UserResponseModele userResponseModele = new UserResponseModele
                {
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    SureName = item.SureName,
                    RoleId = item.RoleId,
                    EMail = item.EMail,
                    Id = item.Id,
                    IsDelete = item.IsDelete,
                    StarDate = item.StarDate,
                    Password = item.Password,
                    SecretKey = item.SecretKey,
                    UserName = item.UserName
                };
                userResponseModeles.Add(userResponseModele);
            }
            return JsonConvert.SerializeObject(userResponseModeles);
        }
        public async Task<ResultModele> ChangePassword(string OldPassword, string NewPassword, Guid UserId)
        {
            ResultModele resultModele = new ResultModele();
            resultModele.Message = "";
            resultModele.Success = false;
            var user = await _userDal.GetAsyc(p => p.Id == UserId);
            if (user != null)
            {
                if (VerifySHA256Hash(OldPassword, user.Password) == false)
                {
                    resultModele.Message = "Şifre hatalı tekrar deneyiniz.";
                    resultModele.Success = false;
                }
                else
                {
                    User User = JsonConvert.DeserializeObject<User>(user.ToString());
                    ValidationTool.Validate(new UserValidator(), user);
                    User.Password = ComputeSHA256Hash(User.Password);
                    _userDal.UpdateAsyc(User);
                    resultModele.Message = "Şifre değiştirme işlemi başarili.";
                    resultModele.Success = true;
                }
            }
            else
            {
                resultModele.Message = "Bir hata Oluştu.";
                resultModele.Success = false;
            }
            return resultModele;
        }
        public LoginResult Login(LoginRequest loginRequest, HttpContext httpContext)
        {
            var loginResult = new LoginResult();
            loginResult.Success = false;
            loginResult.Message = "";
            loginResult.Token = null;

            try
            {
                var user = _userDal.Get(p => p.UserName.Equals(loginRequest.UserName, StringComparison.Ordinal) || p.EMail.Equals(loginRequest.UserName, StringComparison.Ordinal) || p.PhoneNumber.Equals(loginRequest.UserName, StringComparison.Ordinal));
                if (user != null)
                {
                    if (VerifySHA256Hash(loginRequest.Password, user.Password) == false)
                    {
                        loginResult.Message = "Şifre hatalı tekrar deneyiniz.";
                        user = null;
                    }
                    else
                    {
                        loginResult.Message = "Giriş başarılı.";
                        loginResult.Success = true;
                        UserResponseModele userResponseModele = new UserResponseModele();
                        userResponseModele.Name = user.Name;
                        userResponseModele.SureName = user.SureName;
                        userResponseModele.PhoneNumber = user.PhoneNumber;
                        userResponseModele.EMail = user.EMail;
                        userResponseModele.Id = user.Id;
                        userResponseModele.IsDelete = user.IsDelete;
                        userResponseModele.StarDate = user.StarDate;
                        userResponseModele.SecretKey = user.SecretKey;
                        userResponseModele.UserName = user.UserName;
                        userResponseModele.Password = user.Password;
                        userResponseModele.RoleId = user.RoleId;
                        var generateTokenResult = new LoginResult();
                        TokenProcess.GenerateToken(httpContext, ref loginResult, userResponseModele, 25);

                    }
                }
                else
                {
                    loginResult.Message = "Bu Kullanıcı sistemde bulunmamaktadır.";
                }
            }
            catch (Exception e)
            {
                loginResult.Success = false;
                loginResult.Message = "Hata sistem yöneticisi ile iletişime geçin.";
            }
            return loginResult;
        }
        public async Task<string> GetUserByPhoneAsyc(string Phone)
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            var Users = await _userDal.GetAllAsyc(p => p.PhoneNumber.ToLower().Contains(Phone.ToLower()));
            List<User> users = JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(Users));
            foreach (var item in users)
            {
                UserResponseModele userResponseModele = new UserResponseModele
                {
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    SureName = item.SureName,
                    RoleId = item.RoleId,
                    EMail = item.EMail,
                    Id = item.Id,
                    IsDelete = item.IsDelete,
                    StarDate = item.StarDate,
                    Password = item.Password,
                    SecretKey = item.SecretKey,
                    UserName = item.UserName
                };
                userResponseModeles.Add(userResponseModele);
            }
            return JsonConvert.SerializeObject(userResponseModeles);
        }
    }
}
