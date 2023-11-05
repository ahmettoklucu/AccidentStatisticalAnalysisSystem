using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Abstract
{
    public interface IUserService
    {
        Task<List<UserResponseModele>> GetAllAsyc();
        Task<List<UserResponseModele>> GetUserByUserNameAsyc(string userName);
        Task<List<UserResponseModele>> GetUserByPhoneAsyc(string Phone);
        Task<List<UserResponseModele>> GetUserByEMailAsyc(string EMail);
        Task<UserResponseModele> GetAsyc(Guid UserId);
        bool AddAsyc(User user, out string Mesaj);
        bool UpdateAsyc(User user, out string Mesaj);
        void DeleteAsyc(User user);
        Task<LoginResult> Login(LoginRequest loginRequest);
        Task<LoginResult> EmailLogin(LoginRequest loginRequest);
        Task<LoginResult> PhoneLogin(LoginRequest loginRequest);
        Task<LoginResult> UserNameLogin( LoginRequest loginRequest);
        void ChangePassword(string OldPassword,string NewPassword,Guid UserId,out string Messege);
    }
}
