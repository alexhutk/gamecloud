using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Interfaces;
using SteamKiller.DPL.Controllers;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.DPL.Models;

namespace SteamKiller.Tests.Web.Tests
{
    public class LeaderboardControllerTests
    {
        IAccountService accService;

        [Fact]
        public async void GetApplicationLeaderboardReturnsLeaderboardEntityViewModel()
        {
            int appId = 1;
            var mock = new Mock<ILeaderboardService>();
            mock.Setup(e => e.GetLeaderboardsByAppId(appId)).Returns(GetApplicationLeaderboard());
            LeaderboardController controller = new LeaderboardController(mock.Object, accService);

            JsonResult result = await controller.GetApplicationLeaderboard(appId) as JsonResult;

            Assert.NotNull(result);
            Assert.IsType<LeaderboardEntityViewModel>(result.Value);
        }

        Task<LeaderboardSignatureDTO> GetApplicationLeaderboard()
        {
            return Task.FromResult(new LeaderboardSignatureDTO { Id = 1, Name = "ASBU_Leaderboard" });
        }
    }
}
