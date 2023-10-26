using AccidentStatisticalAnalysisSystem.Bussiness.Security;
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
        bool AddAsyc(User user, out string Mesaj);
        bool UpdateAsyc(User user, out string Mesaj);
        void DeleteAsyc(User user);
        bool Login(string Email, string password, out string Messege, out Token token);
        bool EmailLogin(string Email, string password, out string Messege,out Token token);
        bool PhoneLogin(string Phone, string password, out string Messege,out Token token);
        bool UserNameLogin(string UserName, string password, out string Messege, out Token token);
        void ChangePassword(string OldPassword,string NewPassword,Guid UserId,out string Messege);
    }
}
