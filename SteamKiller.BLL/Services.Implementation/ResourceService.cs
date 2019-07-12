using Microsoft.AspNetCore.Http;
using SteamKiller.BLL.Entities.Configurations;
using SteamKiller.DAL.Implementation.Repositories;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Implementation
{
    public class ResourceService : IResourceService
    {
        ResourceConfiguration configuration;
        IAvatarRepository avatarRepository;

        public ResourceService(ResourceConfiguration c)
        {
            configuration = c;

            avatarRepository = new AvatarRepository(configuration.ROOT_PATH + configuration.AVATAR_PATH);
        }

        public async Task<string> SaveAvatar(IFormFile avatar, string name)
        {
            await avatarRepository.Save(avatar, name);

            return Path.Combine(configuration.AVATAR_PATH, name + avatar.FileName);
        }

        public string GetDefaultAvatar()
        {
            return Path.Combine(configuration.AVATAR_PATH, "default_avatar.png");
        }
    }
}
