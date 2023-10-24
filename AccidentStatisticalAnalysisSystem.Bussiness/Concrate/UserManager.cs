using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
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
        private IUserDal _userDal;
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
        public async Task<User> GetAsyc(Guid UserId)
        {
            return await _userDal.GetAsyc(p => p.Id == UserId);
        }
        public async Task<List<User>> GetAllAsyc()
        {
            return await _userDal.GetAllAsyc();
        }
        public async Task<List<User>> GetUserByUserNameAsyc(string userName)
        {
            return await _userDal.GetAllAsyc(p => p.UserName.ToLower().Contains(userName.ToLower()));
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
        public   bool EmailLogin(string Email, string password, out string Messege,out Token token)
        {
            bool result = false;
            Messege = "";
            token = null;
            var User = _userDal.GetAsyc(p => p.EMail == Email);
            if (User != null)
            {
                if (VerifySHA256Hash(password,User.Result.Password) == false)
                {
                    Messege = "Şifre hatalı tekrar deneyiniz.";
                    User = null;

                }
                else
                {
                    Messege = "Giriş başarili.";
                    UserResponseModele userResponseModele = new UserResponseModele();
                    userResponseModele.Name=User.Result.Name;
                    userResponseModele.SureName=User.Result.SureName;
                    userResponseModele.PhoneNumber=User.Result.PhoneNumber;
                    userResponseModele.EMail=User.Result.EMail;
                    userResponseModele.Id=User.Result.Id;
                    userResponseModele.IsDelete = User.Result.IsDelete;
                    userResponseModele.StarDate=User.Result.StarDate;
                    userResponseModele.SecretKey=User.Result.SecretKey;
                    userResponseModele.UserName=User.Result.UserName;
                    userResponseModele.Password = User.Result.Password;
                    userResponseModele.RoleId=User.Result.RoleId;
                    TokenProcess.GenerateToken(userResponseModele, 25, out token, out Messege);
                    result = true;

                }

            }
            else
            {
                Messege = "Bu Email Adresi sistemde bulunmamaktadir.";
            }
            return  result;
        }
        public bool PhoneLogin(string Phone, string password, out string Messege,out Token token)
        {
            bool result = false;
            Messege = "";
            token = null;
            var User = _userDal.GetAsyc(p => p.PhoneNumber == Phone);
            if (User != null)
            {
                if (VerifySHA256Hash(password, User.Result.Password) == false)
                {
                    Messege = "Şifre hatalı tekrar deneyiniz.";
                    User = null;
                }
                else
                {
                    Messege = "Giriş başarili.";
                    result = true;

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
                    TokenProcess.GenerateToken(userResponseModele, 25, out token, out Messege);
                }

            }
            else
            {
                Messege = "Bu telefon sistemde bulunmamaktadir.";
            }
            return  result;
        }
        public  bool UserNameLogin(string UserName, string password, out string Messege,out Token token)
        {
            bool result = false;
            Messege = "";
            token = null;
            var User = _userDal.GetAsyc(p => p.UserName == UserName);
            if (User != null)
            {
                if (VerifySHA256Hash(password, User.Result.Password) == false)
                {
                    Messege = "Şifre hatalı tekrar deneyiniz.";
                    User = null;
                }
                else
                {
                    Messege = "Giriş başarili.";
                    result = true;
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
                    TokenProcess.GenerateToken(userResponseModele, 25, out token, out Messege);
                }

            }
            else
            {
                Messege = "Bu Kullanıcı ismi sistemde bulunmamaktadir.";
            }
            return  result;
        }
        public  async Task<List<User>> GetUserByPhoneAsyc(string Phone)
        {
            return await _userDal.GetAllAsyc(p => p.PhoneNumber.ToLower().Contains(Phone.ToLower()));
        }
        public async Task<List<User>> GetUserByEMailAsyc(string EMail)
        {
            return await _userDal.GetAllAsyc(p => p.EMail.ToLower().Contains(EMail.ToLower()));
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
    }
}
