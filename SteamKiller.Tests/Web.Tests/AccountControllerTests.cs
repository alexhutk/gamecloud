using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Entities;
using SteamKiller.DAL.Entites;
using System.Threading.Tasks;
using SteamKiller.DPL.Controllers;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.DPL.Models;
using System.Security.Claims;
using SteamKiller.BLL.Services.Interfaces;

namespace SteamKiller.Tests.Web.Tests
{
    public class AccountControllerTests
    {
        private IApplicationService appService = null;
        private IAchievmentService achService = null;

        [Fact]
        public async void GetAllReturnsJsonResult()
        {
            var mock = new Mock<IAccountService>();
            mock.Setup(serv => serv.GetAll()).Returns(GetAll());
            AccountController controller = new AccountController(mock.Object, appService, achService);

            JsonResult result = await controller.GetAll() as JsonResult;

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async void GetAllModelIsAccountCollectionViewModel()
        {
            var mock = new Mock<IAccountService>();
            mock.Setup(serv => serv.GetAll()).Returns(GetAll());
            AccountController controller = new AccountController(mock.Object, appService, achService);
            List<Account> listModel = (List<Account>)GetAll().Result.Accounts;

            JsonResult result = await controller.GetAll() as JsonResult;

            var model = Assert.IsAssignableFrom<AccountCollectionViewModel>(result.Value);
            Assert.Equal(model.EntryList.Count, listModel.Count);
        }

        [Fact]
        public async void CanNotAddAccountWithIncorrectPasswords()
        {
            AccountAddViewModel accToAdd = new AccountAddViewModel { Name = "Vitalya", Password = "134", ConfirmPassword = "123" };
            var mock = new Mock<IAccountService>();
            AccountController controller = new AccountController(mock.Object, appService, achService);

            JsonResult result = await controller.AddNewAccount(accToAdd) as JsonResult;

            Assert.NotNull(result);
            Assert.IsType<FailedStatus>(result.Value);
        }

        [Fact]
        public async void CanLoginWithCorrentCredentials()
        {
            AccountLoginViewModel lModel = new AccountLoginViewModel { Name = "Tolya", Password = "1234" };
            AccountDTO accDTO = new AccountDTO { Name = lModel.Name, Password = lModel.Password };
            var mock = new Mock<IAccountService>();
            mock.Setup(serv => serv.Login(It.IsAny<AccountDTO>())).Returns(Login());
            AccountController controller = new AccountController(mock.Object, appService, achService);

            JsonResult result = await controller.Login(lModel) as JsonResult;

            var resModel = result.Value as AccountJWTViewModel;
            Assert.NotNull(resModel);
            Assert.NotNull(resModel.JWTToken);
        }

        Task<AccountCollectionDTO> GetAll()
        {
            return Task.FromResult(new AccountCollectionDTO
            {
                Accounts = new List<Account>
                {
                    new Account{Id = 1, Name = "Tolya", Password = "1234"},
                    new Account{Id = 2, Name = "Petya", Password = "235"}
                }
            });
        }

        Task<AccountCollectionDTO> GetByApplicationId()
        {
            return Task.FromResult(new AccountCollectionDTO
            {
                Accounts = new List<Account>
                {
                    new Account{Id = 1, Name = "Tolya", Password = "1234"},
                    new Account{Id = 2, Name = "Petya", Password = "235"}
                }
            });
        }

        Task<ClaimsIdentity> Login()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "Tolya"),
                };

            return Task.FromResult(new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType));
        }
    }
}
