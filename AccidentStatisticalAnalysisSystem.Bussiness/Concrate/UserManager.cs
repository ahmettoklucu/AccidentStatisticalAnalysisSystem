using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.Bussiness.Utilities;
using AccidentStatisticalAnalysisSystem.Bussiness.ValidationRules.FluentValidation;
using AccidentStatisticalAnalysisSystem.DataAccess.Abstract;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
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
    public class UserManager:IUserService
    {
        private readonly IUserDal _userDal;
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
        public bool AddAsyc(User user, out string mesaj)
        {
            bool result = false;
            mesaj = "";
            try
            {
                var UserName = _userDal.GetAsyc(p => p.UserName == user.UserName);
                if (UserName.Result != null)
                {
                    mesaj = "Kullanıcı adi sistemde kayıtlıdır.";
                }
                else if (_userDal.GetAsyc(p => p.PhoneNumber == user.PhoneNumber).Result != null)
                {
                    mesaj = "Telefon numarası sistemde kayıtlıdır.";
                }
                else if (_userDal.GetAsyc(p => p.EMail == user.EMail).Result != null)
                {
                    mesaj = "EMail sistemde kayıtlıdır.";
                }
                else
                {

                    ValidationTool.Validate(new UserValidator(), user);
                    user.Password = ComputeSHA256Hash(user.Password);
                    _userDal.AddAsyc(user);
                    mesaj = "Kayıt işlemi başarili";
                    result = true;
                }
            }
            catch (Exception ex)
            {
                mesaj=ex.Message;
                result = false;
                
            }
           
            return result;
        }
        public void DeleteAsyc(User user)
        {
            _userDal.DeleteAsyc(user);
        }
        public async Task<UserResponseModele> GetAsyc(Guid UserId)
        {
            var item= _userDal.GetAsyc(p => p.Id == UserId);

            UserResponseModele userResponseModele = new UserResponseModele
            {
                Name = item.Result.Name,
                PhoneNumber = item.Result.PhoneNumber,
                SureName = item.Result.SureName,
                RoleId = item.Result.RoleId,
                EMail = item.Result.EMail,
                Id = item.Result.Id,
                IsDelete = item.Result.IsDelete,
                StarDate = item.Result.StarDate,
                Password = item.Result.Password,
                SecretKey = item.Result.SecretKey,
                UserName = item.Result.UserName
            };
            return  userResponseModele;
        }
        public async Task<List<UserResponseModele>> GetAllAsyc()
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            var Users=  _userDal.GetAllAsyc();
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
            return userResponseModeles;
        }
        public async Task<List<UserResponseModele>> GetUserByUserNameAsyc(string userName)
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            var Users = _userDal.GetAllAsyc(p => p.UserName.ToLower().Contains(userName.ToLower()));
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
            return userResponseModeles;
        }
        public bool UpdateAsyc(User user, out string mesaj)
        {
            bool result= false;
            mesaj = "";
            if (_userDal.GetAsyc(p => p.UserName == user.UserName && p.Id != user.Id) != null)
            {
                mesaj = "Kullanıcı adi sistemde kayıtlıdır.";
            }
            else if (_userDal.GetAsyc(p => p.PhoneNumber == user.EMail && p.Id != user.Id) != null)
            {
                mesaj = "Telefon numarası sistemde kayıtlıdır.";
            }
            else if (_userDal.GetAsyc(p => p.EMail == user.EMail && p.Id != user.Id) != null)
            {
                mesaj = "EMail sistemde kayıtlıdır.";
            }
            else
            {

                ValidationTool.Validate(new UserValidator(), user);
                user.Password = ComputeSHA256Hash(user.Password);
                _userDal.UpdateAsyc(user);
                mesaj = "Kullanıcı ekleme işlemi başarili";
                result = true;
            }
            return result;
        }
        public async Task<LoginResult> EmailLogin(LoginRequest loginRequest)
        {
            var loginResult = new LoginResult();
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
                    userResponseModele.RoleId = User.Result.RoleId;
                    var generateTokenResult = TokenProcess.GenerateToken(userResponseModele, 25);
                    loginResult.Token = generateTokenResult.Token;
                    loginResult.Message = generateTokenResult.Message;
                }

            }
            else
            {
                loginResult.Message = "Bu telefon sistemde bulunmamaktadir.";
                
            }
            return  loginResult;
        }
        public async Task<LoginResult> PhoneLogin(LoginRequest loginRequest)
        {
            var loginResult = new LoginResult();
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
                    userResponseModele.RoleId = User.Result.RoleId;
                    var generateTokenResult = TokenProcess.GenerateToken(userResponseModele, 25);
                    loginResult.Token = generateTokenResult.Token;
                    loginResult.Message = generateTokenResult.Message;
                }

            }
            else
            {
                loginResult.Message = "Bu telefon sistemde bulunmamaktadir.";
            }
            return  loginResult;
        }
        public async Task<LoginResult> UserNameLogin(LoginRequest loginRequest)
        {
            var loginResult = new LoginResult();
            loginResult.Success = false;
            loginResult.Message= "";
            loginResult.Token = null;
            var User = _userDal.GetAsyc(p => p.UserName == loginRequest.UserName);
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
                    userResponseModele.RoleId = User.Result.RoleId;
                    var generateTokenResult = TokenProcess.GenerateToken(userResponseModele, 25);
                    loginResult.Token = generateTokenResult.Token;
                    loginResult.Message = generateTokenResult.Message;
                }

            }
            else
            {
                loginResult.Message = "Bu Kullanıcı ismi sistemde bulunmamaktadir.";
            }
            return loginResult;
        }
        public  async Task<List<UserResponseModele>> GetUserByPhoneAsyc(string Phone)
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            var Users= _userDal.GetAllAsyc(p => p.PhoneNumber.ToLower().Contains(Phone.ToLower()));
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
            return userResponseModeles;
        }
        public async Task<List<UserResponseModele>> GetUserByEMailAsyc(string EMail)
        {
            List<UserResponseModele> userResponseModeles = new List<UserResponseModele>();
            var Users = _userDal.GetAllAsyc(p => p.EMail.ToLower().Contains(EMail.ToLower()));
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
            return userResponseModeles;
        }
        public  void ChangePassword(string OldPassword, string NewPassword, Guid UserId,out string Messege)
        {
            Messege = "";
            var user =   _userDal.GetAsyc(p=>p.Id==UserId);
            if (user != null)
            {
                if (VerifySHA256Hash(OldPassword, user.Result.Password) == false)
                {
                    Messege = "Şifre hatalı tekrar deneyiniz.";

                }
                else
                {
                    User User=JsonConvert.DeserializeObject<User>(user.Result.ToString());
                    ValidationTool.Validate(new UserValidator(), user.Result);
                    User.Password = ComputeSHA256Hash(User.Password);
                    _userDal.UpdateAsyc(User);
                    Messege = "Şifre değiştirme işlemi başarili.";
                }
            }
            else
            {
                Messege = "Bir hata Oluştu.";
            }
        }
        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            var loginResult = new LoginResult();
            loginResult.Success = false;
            loginResult.Message = "";
            loginResult.Token = null;

            try
            {
                var user = _userDal.GetAsyc(p => p.UserName.Equals(loginRequest.UserName, StringComparison.Ordinal) || p.EMail.Equals(loginRequest.UserName, StringComparison.Ordinal) || p.PhoneNumber.Equals(loginRequest.UserName, StringComparison.Ordinal));
                if (user.Result != null)
                {
                    if (VerifySHA256Hash(loginRequest.Password, user.Result.Password) == false)
                    {
                        loginResult.Message = "Şifre hatalı tekrar deneyiniz.";
                        user = null;
                    }
                    else
                    {
                        loginResult.Message = "Giriş başarılı.";
                        loginResult.Success = true;
                        UserResponseModele userResponseModele = new UserResponseModele();
                        userResponseModele.Name = user.Result.Name;
                        userResponseModele.SureName = user.Result.SureName;
                        userResponseModele.PhoneNumber = user.Result.PhoneNumber;
                        userResponseModele.EMail = user.Result.EMail;
                        userResponseModele.Id = user.Result.Id;
                        userResponseModele.IsDelete = user.Result.IsDelete;
                        userResponseModele.StarDate = user.Result.StarDate;
                        userResponseModele.SecretKey = user.Result.SecretKey;
                        userResponseModele.UserName = user.Result.UserName;
                        userResponseModele.Password = user.Result.Password;
                        userResponseModele.RoleId = user.Result.RoleId;
                        var generateTokenResult= TokenProcess.GenerateToken(userResponseModele, 25);

                        loginResult.Token = generateTokenResult.Token;
                        loginResult.Message = generateTokenResult.Message;
                       
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
    }
}
