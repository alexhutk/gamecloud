using Microsoft.AspNetCore.Mvc;
using SteamKiller.DPL.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SteamKiller.Tests.Web.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsViewResult()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexReturnsNotNull()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
        }
    }
}
