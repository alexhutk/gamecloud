using Microsoft.EntityFrameworkCore;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Interfaces;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private IUnitOfWork unitOfWork;
        private ILeaderboardRepository leaderRepository;
        private IAccLeaderRepository accLeaderRepository;
        private IApplicationRepository appRepository;

        public LeaderboardService(IUnitOfWork _unit, ILeaderboardRepository lR, IAccLeaderRepository aR, IApplicationRepository apR)
        {
            unitOfWork = _unit;
            leaderRepository = lR;
            accLeaderRepository = aR;
            appRepository = apR;
        }

        public async Task<long> AddScoreToLeaderboard(AccLeaderDTO accDTO)
        {
            AccLeader item = new AccLeader() { LeaderboardId = accDTO.LeaderboardId, AccountId = accDTO.AccountId, Score = accDTO.Score};

            if (await leaderRepository.ContainsAsync(accDTO.LeaderboardId))
            {
                await accLeaderRepository.AddAsync(item);

                if (await unitOfWork.SaveAsync())
                    return item.Id;
            }

            return -1;

        }

        public async Task<int> AddLeaderboard(int appId, string name)
        {
            int id = -1;
            Leaderboard leaderboard = new Leaderboard();

            if (await appRepository.ContainsAsync(appId))
            {
                Leaderboard oldLeaderboard = await appRepository.GetApplicationLeaderboard(appId);
                
                if (oldLeaderboard != null)
                {
                    id = oldLeaderboard.Id;
                    oldLeaderboard.Name = name;
                    await leaderRepository.UpdateAsync(oldLeaderboard);             
                }
                else
                {
                    leaderboard = new Leaderboard { Name = name, ApplicationId = appId };
                    await leaderRepository.AddAsync(leaderboard);
                }

                if (await unitOfWork.SaveAsync())
                {
                    if (id == -1)
                        id = leaderboard.Id;

                    return id;
                }
            }

            return id;
        }

        public async Task<bool> DeleteLeaderboard(int lId)
        {
            bool success = await leaderRepository.DeleteAsync(lId);

            if (success)
            {
                await accLeaderRepository.DeleteAsync(e => e.LeaderboardId == lId);
                return await unitOfWork.SaveAsync();
            }

            return success;
        }

        public async Task<IEnumerable<LeaderboardEntryDTO>> GetLeaderboardEntriesAsync(int id)
        {
            return await accLeaderRepository.GetLeaderboardEntriesById(id)
                .Select(e => new LeaderboardEntryDTO
                {
                    UserName = e.Account.Name,
                    Score = e.Score
                })
                .OrderBy(e=>e.Score).AsNoTracking().ToListAsync();
        }

        public async Task<string> GetLeaderboardNameAsync(int id)
        {
            Leaderboard l = await leaderRepository.FindByIdAsync(id);

            if (l != null)
                return l.Name;
            else
                return null;
        }

        public async Task<LeaderboardSignatureDTO> GetLeaderboardsByAppId(int appId)
        {
            Leaderboard lead = await appRepository.GetApplicationLeaderboard(appId);

            if (lead == null)
                return null;

            LeaderboardSignatureDTO leadDTO = new LeaderboardSignatureDTO
            {
                Id = lead.Id,
                Name = lead.Name
            };

            return leadDTO;
        }

        public async Task<LeaderboardDTO> GetLeaderboardById(int id)
        {
            var leaderboard = await leaderRepository.GetLeaderboardWithAccountInfoById(id);

            if (leaderboard != null)
            {
                LeaderboardDTO result = new LeaderboardDTO();
                result.Id = id;
                result.Name = leaderboard.Name;

                foreach (var child in leaderboard.AccLeaders)
                {
                    result.EntryList.Add(new LeaderboardEntryDTO
                    {
                        UserName = child.Account.Name,
                        Score = child.Score
                    });
                }

                return result;
            }

            return null;
        }
    }
}
