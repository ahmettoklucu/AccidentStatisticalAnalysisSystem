using AccidentStatisticalAnalysisSystem.Bussiness.Abstract;
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
        public void AddAsyc(User user, out string mesaj)
        {
            mesaj = "";
            var UserName = _userDal.GetAsyc(p => p.UserName == user.UserName);
            if (UserName != null)
            {
                mesaj = "Kullanıcı adi sistemde kayıtlıdır.";
            }
            else if (_userDal.GetAsyc(p => p.PhoneNumber == user.PhoneNumber) != null)
            {
                mesaj = "Telefon numarası sistemde kayıtlıdır.";
            }
            else if (_userDal.GetAsyc(p => p.EMail == user.EMail) != null)
            {
                mesaj = "EMail sistemde kayıtlıdır.";
            }
            else
            {

                ValidationTool.Validate(new UserValidator(), user);
                user.Password = ComputeSHA256Hash(user.Password);
                user.RoleId = 2;
                _userDal.AddAsyc(user);
                mesaj = "Kayıt işlemi başarili";
            }
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
        public void UpdateAsyc(User user, out string mesaj)
        {
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
                mesaj = "Kullanıcı ekleme işlemi başarili";
                ValidationTool.Validate(new UserValidator(), user);
                user.Password = ComputeSHA256Hash(user.Password);
                _userDal.UpdateAsyc(user);
            }
        }
        public  bool EmailLogin(string Email, string password, out string Messege,out Token token)
        {
            bool result = false;
            Messege = "";
            token = null;
            var User = _userDal.GetAsyc(p => p.EMail == Email);
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
                    User user = JsonConvert.DeserializeObject<User>(User.Result.ToString());
                    TokenProcess.GenerateToken(user, 25, out token, out Messege);

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
                    User user = JsonConvert.DeserializeObject<User>(User.Result.ToString());
                    TokenProcess.GenerateToken(user, 25, out token, out Messege);
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
                if (VerifySHA256Hash(password, user1.Result.Password) == false)
                {
                    Messege = "Şifre hatalı tekrar deneyiniz.";
                    User = null;
                }
                else
                {
                    Messege = "Giriş başarili.";
                    result = true;
                    User user = JsonConvert.DeserializeObject<User>(User.Result.ToString());
                    TokenProcess.GenerateToken(user, 25,out Token,out Messege);
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

    }
}
