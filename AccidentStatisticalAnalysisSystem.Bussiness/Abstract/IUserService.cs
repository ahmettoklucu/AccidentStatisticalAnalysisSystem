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
        Task<string> GetUserByUserNameAsyc(string userName);
        Task<string> GetUserByPhoneAsyc(string Phone);
        Task<string> GetUserByEMailAsyc(string EMail);
        Task<UserResponseModele> GetAsyc(Guid UserId);
        Task<ResultModele> AddAsyc(UserResponseModele userResponseModele);
        Task<ResultModele> UpdateAsyc(UserResponseModele userResponseModele);
        Task<ResultModele> DeleteAsyc(Guid UserId);
        LoginResult Login(LoginRequest loginRequest );
        LoginResult EmailLogin(LoginRequest loginRequest);
        LoginResult PhoneLogin(LoginRequest loginRequest);
        LoginResult UserNameLogin( LoginRequest loginRequest);
        ResultModele ChangePassword(string OldPassword,string NewPassword,Guid UserId);
        public bool TokenRenewal(string Token, ref LoginResult loginResult);
        Task<List<GetAllUserResponse>> GetAllAsyc();
    }
}
