using Microsoft.AspNetCore.SignalR;

namespace StockApp.API.Hubs
{
    public class StockHub : Hub
    {
        public async Task SendStockUpdate(string productId, int newStock)
        {
            await Clients.All.SendAsync("ReceiveStockUpdate", productId, newStock);
        }
    }
}