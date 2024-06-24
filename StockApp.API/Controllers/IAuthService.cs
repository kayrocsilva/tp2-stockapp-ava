namespace StockApp.UnitTests.Controllers
{
    public interface IAuthService
    {
        void AuthenticateAsync(string v1, string v2);
    }
}