using Microsoft.AspNetCore.Http;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Implementation.Repositories
{
    public class AvatarRepository : IAvatarRepository
    {
        string avatarPath;

        public AvatarRepository(string av)
        {
            avatarPath = av;
        }

        public async Task<string> Save(IFormFile item, string name)
        {
            if (item != null && item.Length > 0)
            {
                var fileName = name + item.FileName;
                var filePath = Path.Combine(avatarPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                }

                return filePath;
            }
            return String.Empty;
        }
    }
}
