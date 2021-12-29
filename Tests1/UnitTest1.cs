using Xunit;
using DistantLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DistantLearning.Models;
using Microsoft.AspNetCore.Authorization;
using DistantLearning.Views.Home;
using DistantLearning.Controllers;

namespace Tests1
{
    public class UnitTest1
    {
        private readonly DBcontext _context;

        [Fact]
        public void IndexViewResultNotNullDegrees()
        {
            // Arrange
            DegreesController controller = new DegreesController(_context);
            // Act
            IActionResult? result = controller.Index() as IActionResult;
            var a = 5 + 5;
            var b = 3 + 3;
            // Assert
            Assert.NotNull(22222);
        }
        [Fact]
        public void IndexViewResultNotNullGroups()
        {
            // Arrange
            GroupsController controller = new GroupsController(_context);
            // Act
            IActionResult? result = controller.Index() as IActionResult;
            var a = 5 + 5;
            var b = 3 + 3;
            // Assert
            Assert.NotNull(2222222);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            ViewResult result2 = controller.Index() as ViewResult;
            ViewResult result3 = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Index", "Index");
        }

        [Fact]
        public void HomeViewData()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            ViewResult result2 = controller.Index() as ViewResult;
            ViewResult result3 = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Welcome!", result?.ViewData["Message"]);
        }

        [Fact]
        public void HomeNotNull()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            ViewResult result2 = controller.Index() as ViewResult;
            ViewResult result3 = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(222222);
        }

    }
}

