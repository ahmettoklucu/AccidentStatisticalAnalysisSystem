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
       public static bool ValidateToken(string token, string secretKey)
       {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
       }

    public static TokenPayload DecodeToken(string token, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        SecurityToken validatedToken;
        var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

        var tokenPayload = new TokenPayload
        {
            Id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value,
            RoleId = claimsPrincipal.FindFirst(ClaimTypes.Role).Value,
            EMail = claimsPrincipal.FindFirst(ClaimTypes.Email).Value,
            PhoneNumber = claimsPrincipal.FindFirst(ClaimTypes.MobilePhone).Value,
            UserName = claimsPrincipal.FindFirst(ClaimTypes.Name).Value,
            ValidityDatetime = validatedToken.ValidTo
        };
    
        return tokenPayload;
    }

        public static  void GenerateToken(HttpContext httpContext, ref LoginResult generateTokenResult,  UserResponseModele user, int ExpireMinute)
        {            
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
                    string StrSecretKey = "9BEF3695-8687-42B9-B473-A9BDD260984E";
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
                }
                
            }
            catch (Exception ex)
            {
                generateTokenResult.Message = "Sistem Yöneticisi ile görüşmeniz gerekmektedir.";                
            }            
        }
    }
}
    

