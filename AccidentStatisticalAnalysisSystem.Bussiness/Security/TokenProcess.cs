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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

        public static async Task<LoginResult> GenerateToken(HttpContext httpContext, UserResponseModele user, int ExpireMinute)
        {
            LoginResult generateTokenResult = new LoginResult();
            generateTokenResult.Token = null;
            generateTokenResult.Success = false;
            generateTokenResult.Message = "";
            try
            {
                if (user != null && httpContext != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Email, user.EMail),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                        new Claim(ClaimTypes.Name, user.UserName),
                    };
                    string StrSecretKey = "9BEF3695 - 8687 - 42B9 - B473 - A9BDD260984E";
                    var SecretKey = Encoding.UTF8.GetBytes(StrSecretKey);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddMinutes(ExpireMinute),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    generateTokenResult.Token = new Token();
                    generateTokenResult.Token.JWT = tokenHandler.WriteToken(token);
                    generateTokenResult.Token.ValidMinute = ExpireMinute;
                    generateTokenResult.Token.SecretKey = user.SecretKey;
                    generateTokenResult.Token.ValidityDatetime = DateTime.Now.AddMinutes(ExpireMinute);
                    generateTokenResult.Success = true;
                    generateTokenResult.Token.Id = user.Id;
                    generateTokenResult.Token.RoleId = user.RoleId;
                    generateTokenResult.Token.EMail = user.EMail;
                    generateTokenResult.Token.PhoneNumber = user.PhoneNumber;
                    generateTokenResult.Token.UserName = user.UserName;
                    await httpContext.AuthenticateAsync();
                    await httpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
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
    

