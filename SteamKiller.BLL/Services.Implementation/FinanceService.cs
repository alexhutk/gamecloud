using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Implementation
{
    public class FinanceService : IFinanceService
    {
        private IUnitOfWork unitOfWork;
        private IFinanceProfileRepository profileRepository;

        public FinanceService(IUnitOfWork _u, IFinanceProfileRepository _profile)
        {
            unitOfWork = _u;
            profileRepository = _profile;
        }

        public async Task<int> AddFinanceProfile(FinanceProfileDTO finDTO)
        {
            if (finDTO != null)
            {
                FinanceProfile profile = new FinanceProfile
                {
                    Address = finDTO.Address,
                    BankName = finDTO.BankName,
                    IbanNumber = finDTO.IbanNumber,
                    AccountId = finDTO.AccountId
                };

                try
                {
                    await profileRepository.AddAsync(profile);
                }
                catch
                {
                    return -1;
                }

                if (!await unitOfWork.SaveAsync())
                    return -3;

                return profile.Id;
            }

            return -2;
        }

        public async Task<FinanceProfileDTO> GetAccountFinanceProfile(int id)
        {
            FinanceProfile profile = await profileRepository.FindByIdAsync(id);

            if (profile != null)
            {
                FinanceProfileDTO finDTO = new FinanceProfileDTO
                {
                    Address = profile.Address,
                    BankName = profile.BankName,
                    IbanNumber = profile.IbanNumber
                };

                if (!await unitOfWork.SaveAsync())
                    return null;

                return finDTO;
            }

            return null;
        }

        public async Task<bool> RemoveFinanceProfile(int id)
        {
            if (await profileRepository.DeleteAsync(id))
            {
                if (!await unitOfWork.SaveAsync())
                    return false;

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateFinanceProfile(FinanceProfileDTO finDTO)
        {
            if (finDTO != null)
            {
                FinanceProfile profile = new FinanceProfile
                {
                    Address = finDTO.Address,
                    BankName = finDTO.BankName,
                    IbanNumber = finDTO.IbanNumber
                };

                try
                {
                    await profileRepository.UpdateAsync(profile);
                }
                catch
                {
                    return false;
                }

                if (!await unitOfWork.SaveAsync())
                    return false;

                return true;
            }

            return false;
        }
    }
}
