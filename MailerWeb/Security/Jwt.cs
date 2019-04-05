using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace MailerWeb.Security
{
    public class Jwt
    {
        private const string Secret = "cCrl50ViLseEpTYDEHNPJlMKL6icVWQC";

        public static IEnumerable<Claim> DecodeToken(string token)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var subject = tokenHandler.ReadJwtToken(token);
            //TODO tokenHandler.CanValidateToken
            return subject.Claims;
        }

        public static string GenerateToken(string login, string key, string vector, int expireMinutes = 43200)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Login", login),
                    new Claim("Key", key),
                    new Claim("IV", vector)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var sToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(sToken);

            return token;
        }
    }
}