using Microsoft.AspNetCore.SignalR;

namespace DataDemo.Common;
public class NotiHub : Hub
{
    // Phương thức này sẽ được gọi từ ASP.NET Core để gửi thông báo đến Angular
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
