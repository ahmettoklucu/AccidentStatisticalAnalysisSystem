using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.RequestModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.AspNetCore.Http;
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
        Task<string> GetAllAsyc();
        Task<string> GetUserByUserNameAsyc(string userName);
        Task<string> GetUserByPhoneAsyc(string Phone);
        Task<string> GetUserByEMailAsyc(string EMail);
        Task<UserResponseModele> GetAsyc(Guid UserId);
        Task<ResultModele> AddAsyc(User user);
        Task<ResultModele> UpdateAsyc(User user);
        Task<ResultModele> DeleteAsyc(User user);
        LoginResult Login(LoginRequest loginRequest );
        Task<LoginResult> EmailLogin(LoginRequest loginRequest);
        Task<LoginResult> PhoneLogin(LoginRequest loginRequest);
        Task<LoginResult> UserNameLogin( LoginRequest loginRequest);
        Task<ResultModele> ChangePassword(string OldPassword,string NewPassword,Guid UserId);
    }
}
