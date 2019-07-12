using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using SteamKiller.BLL.Interfaces;
using SteamKiller.DAL.Interfaces;
using SteamKiller.BLL.Services;
using System.Security.Claims;
using SteamKiller.BLL.Entities;
using SteamKiller.DPL.Controllers;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.DPL.Models;
using SteamKiller.DAL.Entites;
using SteamKiller.BLL.Services.Interfaces;

namespace SteamKiller.Tests.BLL.Tests
{
    public class AccountServicesTests
    {
        IUnitOfWork fUnitOfWork = null;
        IAccountRepository fAccRepository = null;
        IAppAccRepository fAppAccRepository = null;
        IAccountService accService = null;
        IAchievmentService achService = null;

        [Fact]
        public async void LoginReturnsClaimsIdentityIfAccountExists()
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(e => e.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(()=>Task.FromResult(1));
            IAccountService service = new AccountService(fUnitOfWork, mock.Object, fAppAccRepository);

            ClaimsIdentity claimsIdentity = await service.Login(new AccountDTO { Name = "Tolya", Password = "123" });

            Assert.NotNull(claimsIdentity);
        }

        [Fact]
        public async void LoginReturnsNullIfAccountNotExists()
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(e => e.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(() => Task.FromResult(-1));
            IAccountService service = new AccountService(fUnitOfWork, mock.Object, fAppAccRepository);

            ClaimsIdentity claimsIdentity = await service.Login(new AccountDTO { Name = "Tolya", Password = "123" });

            Assert.Null(claimsIdentity);
        }

        [Fact]
        public async void GetUserApplicationReturnsListViewModel()
        {
            int id = 1;
            var mock = new Mock<IApplicationService>();
            mock.Setup(e => e.GetUserApplications(id)).Returns(GetUserApplications());
            AccountController controller = new AccountController(accService, mock.Object, achService);

            JsonResult result = await controller.UserApplications(id) as JsonResult;

            Assert.IsType<ApplicationListViewModel>(result.Value);
        }

        [Fact]
        public async void GetUserApplicationFormsCorrentListViewModel()
        {
            int id = 1;
            var mock = new Mock<IApplicationService>();
            mock.Setup(e => e.GetUserApplications(id)).Returns(GetUserApplications());
            AccountController controller = new AccountController(accService, mock.Object, achService);
            List<ApplicationEntryViewModel> lModel = new List<ApplicationEntryViewModel>
            {
                new ApplicationEntryViewModel{ Id = 1, Name = "ASBU"},
                new ApplicationEntryViewModel{ Id = 2, Name = "Blik"}
            };

            JsonResult result = await controller.UserApplications(id) as JsonResult;

            var model = result.Value as ApplicationListViewModel;

            for (int i = 0; i < model.EntryList.Count; i++)
            {
                Assert.Equal(lModel[i].Id, model.EntryList[i].Id);
            }
        }

        Task<ApplicationCollectionDTO> GetUserApplications()
        {
            return Task.FromResult(new ApplicationCollectionDTO
            {
                Applications = new List<Application> {
                    new Application{ Id = 1, Name = "ASBU" },
                    new Application{ Id = 2, Name = "Blik" }
                }
            });
        }
    }
}
