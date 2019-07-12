using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Services;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Interfaces;
using Xunit;

namespace SteamKiller.Tests.BLL.Tests
{
    public class LeaderboardServiceTests
    {
        private IUnitOfWork unitOfWork = null;
        private ILeaderboardRepository leaderRepository = null;
        private IAccLeaderRepository accLeaderRepository = null;
        private IApplicationRepository appRepository = null;

        [Fact]
        public async void AddLeaderboardReturnsMinusOneIfApplicationNotExists()
        {
            int appId = 1;
            var mock = new Mock<IApplicationRepository>();
            mock.Setup(e=>e.ContainsAsync(appId)).Returns(()=>Task.FromResult(false));
            ILeaderboardService service = new LeaderboardService(unitOfWork, leaderRepository, accLeaderRepository, mock.Object);

            var result = await service.AddLeaderboard(appId, "");

            Assert.Equal(result, -1);
        }

        [Fact]
        public async void AddLeaderboardVerifyUpdateIfLeaderboardExists()
        {
            int appId = 1;
            var appMock = new Mock<IApplicationRepository>();
            var leaderMock = new Mock<ILeaderboardRepository>();
            var unitMock = new Mock<IUnitOfWork>();
            appMock.Setup(e => e.ContainsAsync(appId)).Returns(() => Task.FromResult(true));
            appMock.Setup(e => e.GetApplicationLeaderboard(appId)).Returns(() => Task.FromResult(new Leaderboard { Id = 1, Name = "Asbu_leaderboard" }));
            ILeaderboardService service = new LeaderboardService(unitMock.Object, leaderMock.Object, accLeaderRepository, appMock.Object);

            var result = await service.AddLeaderboard(appId, "");

            leaderMock.Verify(e => e.UpdateAsync(It.IsAny<Leaderboard>()));
        }

        [Fact]
        public async void AddLeaderboardVerifyAddIfLeaderboardNotExists()
        {
            int appId = 1;
            var appMock = new Mock<IApplicationRepository>();
            var leaderMock = new Mock<ILeaderboardRepository>();
            var unitMock = new Mock<IUnitOfWork>();
            appMock.Setup(e => e.ContainsAsync(appId)).Returns(() => Task.FromResult(true));
            appMock.Setup(e => e.GetApplicationLeaderboard(appId)).Returns(() => Task.FromResult((Leaderboard) null));
            ILeaderboardService service = new LeaderboardService(unitMock.Object, leaderMock.Object, accLeaderRepository, appMock.Object);

            var result = await service.AddLeaderboard(appId, "");

            leaderMock.Verify(e => e.AddAsync(It.IsAny<Leaderboard>()));
        }
    }
}
