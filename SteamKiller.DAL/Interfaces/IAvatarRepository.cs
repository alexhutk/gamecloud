using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IAvatarRepository
    {
        Task<string> Save(IFormFile item, string name);
    }
}
