using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Implementation
{
    public interface IResourceService
    {
        Task<string> SaveAvatar(IFormFile avatar, string name);
        string GetDefaultAvatar();
    }
}
