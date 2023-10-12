using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsyc();
        Task<List<User>> GetUserByUserNameAsyc(string userName);
        Task<List<User>> GetUserByPhoneAsyc(string Phone);
        Task<List<User>> GetUserByEMailAsyc(string EMail);
        Task<User> GetAsyc(Guid UserId);
        void AddAsyc(User user, out string Mesaj);
        void UpdateAsyc(User user, out string Mesaj);
        void DeleteAsyc(User user);
        bool EmailLogin(string Email, string password, out string Messege,out string Token);
        bool PhoneLogin(string Phone, string password, out string Messege,out string Token);
        bool UserNameLogin(string UserName, string password, out string Messege, out string Token);
    }
}
