using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.Bussiness.Utilities;
using AccidentStatisticalAnalysisSystem.Bussiness.ValidationRules.FluentValidation;
using AccidentStatisticalAnalysisSystem.DataAccess.Abstract;
using AccidentStatisticalAnalysisSystem.DataAccess.Abstract.IRepository;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Repository;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserQueryable _userEntity;
        private readonly IRoleQueryable _roleEntity;

        public UserManager(IUserDal userDal, IUserQueryable userEntity,
       IRoleQueryable roleEntity)
        {

            _userDal = userDal;
            _userEntity = userEntity;
            _roleEntity = roleEntity;
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
        public async Task<ResultModele> AddAsyc(UserResponseModele userResponseModele)
        {
            ResultModele resultModele = new ResultModele();
            resultModele.Success = false;
            resultModele.Message = "";
            try
            {
                var UserName = _userDal.GetAsyc(p => p.UserName == userResponseModele.UserName);
                if (UserName.Result != null)
                {
                    resultModele.Message = "Kullanıcı adi sistemde kayıtlıdır.";
                }
                else if (_userDal.GetAsyc(p => p.PhoneNumber == userResponseModele.PhoneNumber).Result != null)
                {
                    resultModele.Message = "Telefon numarası sistemde kayıtlıdır.";
                }
                else if (_userDal.GetAsyc(p => p.EMail == userResponseModele.EMail).Result != null)
                {
                    resultModele.Message = "EMail sistemde kayıtlıdır.";
                }
                else
                {
                    User user = new User
                    {
                        Name = userResponseModele.Name,
                        SureName = userResponseModele.SureName,
                        Password = userResponseModele.Password,
                        PhoneNumber = userResponseModele.PhoneNumber,
                        EMail = userResponseModele.EMail,
                        UserName = userResponseModele.UserName
                    };
                    ValidationTool.Validate(new UserValidator(), user);
                    userResponseModele.Password = ComputeSHA256Hash(user.Password);
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
        public async Task<List<GetAllUserResponse>> GetAllAsyc()
        {
            try
            {
               var  query =  (from User in _userEntity.Table
                          join Role in _roleEntity.Table on User.RoleId equals Role.Id 
                          select  new GetAllUserResponse
                          {
                              Id=User.Id,
                              Role=Role.Name,
                              Name=User.Name,
                              SureName=User.SureName,
                              UserName=User.UserName,
                              PhoneNumber=User.PhoneNumber,
                              EMail=User.EMail,
                              IsDelete=User.IsDelete
                          }).ToList();
                return  query;


            }
            catch (Exception ex)
            {
                return null;
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
        public async Task<ResultModele> UpdateAsyc(UserResponseModele userResponseModele)
        {
            ResultModele resultModele = new ResultModele();
            try
            {
                resultModele.Success = false;
                resultModele.Message = "";
                if (_userDal.GetAsyc(p => p.UserName == userResponseModele.UserName && p.Id != userResponseModele.Id) != null)
                {
                    resultModele.Message = "Kullanıcı adi sistemde kayıtlıdır.";
                    resultModele.Success = false;
                }
                else if (_userDal.GetAsyc(p => p.PhoneNumber == userResponseModele.EMail && p.Id != userResponseModele.Id) != null)
                {
                    resultModele.Message = "Telefon numarası sistemde kayıtlıdır.";
                    resultModele.Success = false;
                }
                else if (_userDal.GetAsyc(p => p.EMail == userResponseModele.EMail && p.Id != userResponseModele.Id) != null)
                {
                    resultModele.Message = "EMail sistemde kayıtlıdır.";
                    resultModele.Success = false;
                }
                else
                {
                    User user = new User
                    {
                        Name = userResponseModele.Name,
                        SureName = userResponseModele.SureName,
                        Password = userResponseModele.Password,
                        PhoneNumber = userResponseModele.PhoneNumber,
                        EMail = userResponseModele.EMail,
                        UserName = userResponseModele.UserName
                    };
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
        public  LoginResult EmailLogin(LoginRequest loginRequest )
        {
            var loginResult = new LoginResult();
            loginResult.Success = false;
            loginResult.Message = "";
            loginResult.Token = null;
            try
            {
                var user = _userDal.Get(p => p.EMail.Equals(loginRequest.UserName, StringComparison.Ordinal));
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
                        TokenProcess.GenerateToken(ref loginResult, userResponseModele, 25);

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
        public  LoginResult PhoneLogin(LoginRequest loginRequest )
        {
            var loginResult = new LoginResult();
            loginResult.Success = false;
            loginResult.Message = "";
            loginResult.Token = null;
            try
            {
                var user = _userDal.Get(p => p.PhoneNumber.Equals(loginRequest.UserName, StringComparison.Ordinal));
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
                        TokenProcess.GenerateToken(ref loginResult, userResponseModele, 25);

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
        public  LoginResult UserNameLogin(LoginRequest loginRequest )
        {
            var loginResult = new LoginResult();
            loginResult.Success = false;
            loginResult.Message = "";
            loginResult.Token = null;
            try
            {
                var user = _userDal.Get(p => p.UserName.Equals(loginRequest.UserName, StringComparison.Ordinal));
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
                        TokenProcess.GenerateToken(ref loginResult, userResponseModele, 25);

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
        public  ResultModele ChangePassword(string OldPassword, string NewPassword, Guid UserId)
        {
            ResultModele resultModele = new ResultModele();
            resultModele.Message = "";
            resultModele.Success = false;
            var user =  _userDal.Get(p => p.Id == UserId);
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
        public LoginResult Login(LoginRequest loginRequest)
        {
            var loginResult = new LoginResult();
            loginResult.Success = false;
            loginResult.Message = "";
            loginResult.Token = null;

            try
            {
                var user =  _userDal.Get(p => p.UserName.Equals(loginRequest.UserName, StringComparison.Ordinal) || p.EMail.Equals(loginRequest.UserName, StringComparison.Ordinal) || p.PhoneNumber.Equals(loginRequest.UserName, StringComparison.Ordinal));
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
                        TokenProcess.GenerateToken( ref loginResult, userResponseModele, 20);

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
            return  loginResult;
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
        public bool TokenRenewal(string Token ,ref LoginResult loginResult)
        {
            loginResult.Token = new Token();
            loginResult.Success = false;
            loginResult.Message = "";
           
            
            bool result=false;
            var success = TokenProcess.ValidateToken(Token);
            if (success == true)
            {
                
                var claim = TokenProcess.DecodeToken(Token);
                var item = _userDal.Get(p => p.EMail == claim.EMail && p.UserName == claim.UserName&&p.PhoneNumber==p.PhoneNumber);
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
               
                TokenProcess.GenerateToken(ref loginResult, userResponseModele, 20);
                result = true;

            }
            else
            {
                result = false;
            }
            return result;

        }
    }
}
