using Azure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Jose;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel;
using AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel;
using Microsoft.AspNetCore.Authentication;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Security
{
    public static class TokenProcess
    {
        public static bool DecodeToken(string JWT, string SecretKey, out Token? token, out string Error)
        {
            bool result = false;
            token = null;
            Error = "";

            try
            {
                string token1 = JWT;
                var secretKey = Encoding.UTF8.GetBytes(SecretKey);
                string json = Jose.JWT.Decode(token1, secretKey, JwsAlgorithm.HS256);
                var DynToken = JsonConvert.DeserializeObject<dynamic>(json);
                token = new Token();
                token.RoleId = Convert.ToInt16(DynToken.RoleId);
                token.Id = DynToken.Id.Value;
                token.EMail = DynToken.Email.Value;
                token.PhoneNumber = DynToken.PhoneNumber.Value;
                token.UserName = DynToken.UserName.Value;

                if (token != null && token.Id != null)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return result;
        }

        public static LoginResult GenerateToken(HttpContext httpContext, UserResponseModele user, int ExpireMinute)
        {
            LoginResult generateTokenResult = new LoginResult();
            generateTokenResult.Token = null;
            generateTokenResult.Success = false;
            generateTokenResult.Message = "";

            try
            {
                if (user != null && httpContext != null)
                {
                    var ClaimToken = new Dictionary<string, object>()
                    {
                        { "UserId", user.Id },
                        { "RoleId", user.RoleId },
                        { "Email", user.EMail },
                        { "PhoneNumber", user.PhoneNumber },
                        { "UserName", user.UserName },
                    };

                    string StrSecretKey = user.SecretKey.ToString();
                    var SecretKey = Encoding.UTF8.GetBytes(StrSecretKey);

                    string Jwt = Jose.JWT.Encode(ClaimToken, SecretKey, JwsAlgorithm.HS256);

                    generateTokenResult.Token = new Token();
                    generateTokenResult.Token.JWT = Jwt;
                    generateTokenResult.Token.ValidMinute = ExpireMinute;
                    generateTokenResult.Token.SecretKey = user.SecretKey;
                    generateTokenResult.Token.ValidityDatetime = DateTime.Now.AddMinutes(ExpireMinute);
                    generateTokenResult.Success = true;
                    generateTokenResult.Token.Id = user.Id;
                    generateTokenResult.Token.RoleId = user.RoleId;
                    generateTokenResult.Token.EMail = user.EMail;
                    generateTokenResult.Token.PhoneNumber = user.PhoneNumber;
                    generateTokenResult.Token.UserName = user.UserName;

                    // Claims oluşturma
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Email, user.EMail),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                        new Claim(ClaimTypes.Name, user.UserName),
                    };

                    // ClaimsIdentity oluşturma
                    var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                    // ClaimsPrincipal oluşturma
                    var principal = new ClaimsPrincipal(identity);

                    // HttpContext'e kimliği doğrulama işlemi ekleme
                    httpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal).Wait(); // Asenkron işlemi senkron hale getirme
                }

                return generateTokenResult;
            }
            catch (Exception ex)
            {
                generateTokenResult.Message = ex.Message;
                return generateTokenResult;
            }
        }
    }
}

