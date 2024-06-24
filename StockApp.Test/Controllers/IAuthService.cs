namespace StockApp.UnitTests.Controllers
{
    internal interface IAuthService
    {
        void AuthenticateAsync(string v1, string v2);
    }
}