using Microsoft.AspNetCore.Mvc;
using Moq;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Application.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StockApp.UnitTests.Controllers
{
    public class TokenControllerTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var tokenController = new TokenController(authServiceMock.Object);

            var expectedToken = "token";
            var expectedExpiration = DateTime.UtcNow.AddMinutes(60);

            authServiceMock.Setup(service => service.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new TokenResponseDTO
                {
                    Token = expectedToken,
                    Expiration = expectedExpiration
                });

            var userLoginDto = new UserLoginDTO
            {
                UserName = "testuser",
                Password = "password"
            };

            // Act
            var result = await tokenController.Login(UserLoginDTO) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<TokenResponseDTO>(result.Value);

            var tokenResponse = result.Value as TokenResponseDTO;
            Assert.Equal(expectedToken, tokenResponse.Token);
            Assert.Equal(expectedExpiration, tokenResponse.Expiration);
        }
    }
}