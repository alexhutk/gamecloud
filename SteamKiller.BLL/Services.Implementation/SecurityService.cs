using Microsoft.IdentityModel.Tokens;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DPL.Identity.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SteamKiller.BLL.Services.Implementation
{
    public class SecurityService : ISecurityService
    {
        const int SALT_LENGTH = 16;
        const int PASS_HASH_LENGTH = 20;
        const int ITER_COUNT = 1000;

        public string GenerateEncodedJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GeneratePasswordHash(string password)
        {
            var salt = GenerateSalt(SALT_LENGTH);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITER_COUNT);
            byte[] hash = pbkdf2.GetBytes(PASS_HASH_LENGTH);

            byte[] hashBytes = new byte[SALT_LENGTH + PASS_HASH_LENGTH];
            Array.Copy(salt, 0, hashBytes, 0, SALT_LENGTH);
            Array.Copy(hash, 0, hashBytes, SALT_LENGTH, PASS_HASH_LENGTH);

            string savedPassword = Convert.ToBase64String(hashBytes);

            return savedPassword;
        }

        public bool VerifyPassword(string enteredPassword, string passwordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, SALT_LENGTH);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, ITER_COUNT);
            byte[] hash = pbkdf2.GetBytes(PASS_HASH_LENGTH);

            for (int i = 0; i < PASS_HASH_LENGTH; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }

        private byte[] GenerateSalt(int length)
        {
            byte[] salt;

            new RNGCryptoServiceProvider().GetBytes(salt = new byte[length]);

            return salt;
        }
    }
}
