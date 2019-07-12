using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SteamKiller.BLL.Services.Interfaces
{
    public interface ISecurityService
    {
        string GenerateEncodedJwt(ClaimsIdentity identity);
        string GeneratePasswordHash(string password);
        bool VerifyPassword(string enteredPassword, string passwordHash);
    }
}
