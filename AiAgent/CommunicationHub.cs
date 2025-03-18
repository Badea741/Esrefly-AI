using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.AI;
using Newtonsoft.Json;

namespace Esrefly.AiAgent;

public class CommunicationHub(IChatClient chat, Features.User.Read.IQueryService queryService) : Hub
{
    public override Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var userId = httpContext?.Request.Query["userId"].ToString() ?? "Unknown";

        Console.WriteLine($"User Connected: {userId}");

        // Store the user connection if needed
        Groups.AddToGroupAsync(Context.ConnectionId, userId);

        return base.OnConnectedAsync();
    }

    public async Task SendPrompt(string userId, string prompt)
    {
        var userData = await queryService.GetUserFinancialData(new Features.User.Read.Query(userId), CancellationToken.None);
        var userDataChatMessage = new ChatMessage(ChatRole.System, JsonConvert.SerializeObject(userData));
        var message = "";
        await foreach (var item in chat.GetStreamingResponseAsync([userDataChatMessage, new ChatMessage(ChatRole.User, prompt)]))
        {
            message += item.Text;
            await Clients.Group(userId).SendAsync("ReceiveMessage", item.Text);
        };
        Console.WriteLine(message);
    }
}