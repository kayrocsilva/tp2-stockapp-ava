using Microsoft.AspNetCore.Mvc;
using Moq;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace StockApp.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task Register_ValidUser_ReturnsOk()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var usersController = new UsersController(userRepositoryMock.Object);

            var userRegisterDto = new UserRegisterDTO
            {
                UserName = "testuser",
                Password = "password",
                Role = "User"
            };

            // Act
            var result = await usersController.Register(userRegisterDto) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            // Verify repository method was called
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}