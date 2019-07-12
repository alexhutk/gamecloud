using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SteamKiller.BLL.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetAccountId(this ClaimsPrincipal User)
        {
            Claim id = User.FindFirst("Id");

            if (id == null)
                return -1;

            string idStr = id.Value;

            if (idStr != null)
                return Convert.ToInt32(idStr);
            else return -1;
        }

        public static string GetAccountAvatar(this ClaimsPrincipal User)
        {
            Claim avatar = User.FindFirst("Avatar");

            if (avatar == null)
                return String.Empty;

            string idStr = avatar.Value;

            if (idStr != null)
                return idStr;
            else return String.Empty;
        }
    }
}
