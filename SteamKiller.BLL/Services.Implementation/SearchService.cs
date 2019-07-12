using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DAL.Entities.Complex;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Implementation
{
    public class SearchService : ISearchService
    {
        private IAppAccRepository accRepository;

        public SearchService(IAppAccRepository _acc)
        {
            accRepository = _acc;
        }

        public async Task<ApplicationCollectionDTO> SearchApplication(int accId, string query)
        {
            ApplicationCollectionDTO appDTO = new ApplicationCollectionDTO();

            IEnumerable<ApplicationComplex> apps = await accRepository.SearchApplication(accId, query);

            foreach (var child in apps)
            {
                appDTO.Applications.Add(new ApplicationDTO
                {
                    Id = child.Id,
                    Name = child.Name,
                    ImageData = child.ImageData,
                    ImageMimeType = child.ImageMimeType,
                    IsAdmin = child.IsAdmin
                });
            }

            return appDTO;
        }
    }
}
