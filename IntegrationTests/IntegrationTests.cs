using System.Net.Http.Json;
using Xunit;
using StockApp.Application.DTOs;

namespace StockApp.IntegrationTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly object userRegisterDTO;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterAndLogin_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var userRegisterDto = new UserRegisterDTO
            {
                UserName = "testuser",
                Password = "password",
                Role = "User"
            };

            var userLoginDTO = new UserLoginDTO
            {
                UserName = "testuser",
                Password = "password"
            };

            // Register
            var registerResponse = await _client.PostAsJsonAsync("/api/users/register", userRegisterDTO);
            registerResponse.EnsureSuccessStatusCode();

            // Login
            var loginResponse = await _client.PostAsJsonAsync("/api/token/login", userLoginDTO);
            loginResponse.EnsureSuccessStatusCode();

            var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponseDTO>();

            // Assert
            Assert.NotNull(tokenResponse);
            Assert.NotNull(tokenResponse.Token);
            Assert.True(tokenResponse.Expiration > DateTime.UtcNow);
        }
    }
}
