using Microsoft.AspNetCore.SignalR;

namespace BibliotecaApp.Etapa4.API.Hubs;

public class LibraryHub : Hub
{
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.Caller.SendAsync("JoinedRoom", roomName);
    }
}
