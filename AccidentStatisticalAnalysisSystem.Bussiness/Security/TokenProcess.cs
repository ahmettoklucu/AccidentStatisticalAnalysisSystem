using Azure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Security
{
    public static class TokenProcess
    {
        public static bool DecodeToken(string JWT, string SecretKey, out Token? token,out string Error)
        {
            bool result=false;
            token = null;
            Error = "";
            try
            {
                string token1 = JWT;
                var secretKey = Encoding.UTF8.GetBytes(SecretKey);
                string json = Jose.JWT.Decode(token1, secretKey, JwsAlgorithm.HS256);
                //"KullaniciId":1,"BayiId":156555,"Email":"tuncgulec@hotmail.com","Guid":"0934AAD4EED64DDC8ABE4BD40C46D586       
                var DynToken = JsonConvert.DeserializeObject<dynamic>(json);
                token = new Token();
                token.RoleId = Convert.ToInt16(DynToken.RoleId);
                token.Id = DynToken.Id.Value;
                token.EMail = DynToken.Email.Value;
                token.PhoneNumber = DynToken.PhoneNumber.Value;
                token.UserName= DynToken.UserName.Value;

                if (token != null && token.Id != null)
                {

                    result = true;
                }
                

            }
            catch (Exception ex)
            {
              Error=ex.Message;
            }
            return result;
        }
        public static void  GenerateToken(User user, int ExpireMinute, out Token token,out string Error)
        {

            token = null;
            bool result = false;
            Error = "";
            try
            {
                if (user != null)
                {

                    var ClaimToken = new Dictionary<string, object>()
                    {
                        { "UserId", user.Id },
                        { "RoleId", user.RoleId },
                        { "Email", user.EMail },
                        { "PhoneNumber" , user.PhoneNumber },
                        {"UserName",user.UserName},
                    };

                    //byte[] SecretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

                    string StrSecretKey = user.SecretKey.ToString();
                    var SecretKey = Encoding.UTF8.GetBytes(StrSecretKey);
                    string ByteBisi = SecretKey.ToString();

                    string Jwt = Jose.JWT.Encode(ClaimToken, SecretKey, JwsAlgorithm.HS256);
                    token = new Token();
                    token.JWT = Jwt;
                    token.ValidMinute = ExpireMinute;
                    token.SecretKey = user.SecretKey;
                    token.ValidityDatetime = new DateTime().AddMinutes(20);
                    result= true;
                }
            }
            catch (Exception ex)
            {
                Error=ex.Message;
            }

        }

    }
}
