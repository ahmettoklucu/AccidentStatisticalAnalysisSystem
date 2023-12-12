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
      
       public static bool ValidateToken(string token)
       {
           
            bool result = false;
            var claim= DecodeToken(token);
            if (claim != null)
            {
                if(claim.Success==true)    
                {
                    if (claim.ValidityDatetime > DateTime.UtcNow)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
       }
        public static TokenPayload DecodeToken(string token)
        {
                string secretKey = "9BEF3695868742B9B473A9BDD260984EJSAKDH15475349DASKL";
                var tokenPayload = new TokenPayload();
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    SecurityToken validatedToken;
                    var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                        tokenPayload.Id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
                        tokenPayload.RoleId = claimsPrincipal.FindFirst(ClaimTypes.Role).Value;
                        tokenPayload.EMail = claimsPrincipal.FindFirst(ClaimTypes.Email).Value;
                        tokenPayload.PhoneNumber = claimsPrincipal.FindFirst(ClaimTypes.MobilePhone).Value;
                        tokenPayload.UserName = claimsPrincipal.FindFirst(ClaimTypes.Name).Value;
                        tokenPayload.ValidityDatetime = validatedToken.ValidTo;
                        tokenPayload.Success = true;
                }
                catch
                {
                    tokenPayload.Success=false;
                }
            return tokenPayload;
        }
        public static  void GenerateToken( ref LoginResult generateTokenResult,  UserResponseModele user, int ExpireMinute)
        {            
            generateTokenResult.Token = null;
            generateTokenResult.Success = false;
            generateTokenResult.Message = "";
            try
            {
                if (user != null )
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Email, user.EMail),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                        new Claim(ClaimTypes.Name, user.UserName),
                    };
                    string StrSecretKey = "9BEF3695868742B9B473A9BDD260984EJSAKDH15475349DASKL";
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
    

