using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.AI;
using Newtonsoft.Json;

namespace Esrefly.AiAgent;

public class CommunicationHub(IChatClient chat, Features.User.Read.IQueryService queryService) : Hub
{
    public async Task SendPrompt(string userId, string prompt)
    {
        var userData = await queryService.GetUserFinancialData(new Features.User.Read.Query(userId), CancellationToken.None);
        var userDataChatMessage = new ChatMessage(ChatRole.User, JsonConvert.SerializeObject(userData));
        await foreach (var item in chat.GetStreamingResponseAsync([userDataChatMessage, new ChatMessage(ChatRole.User, prompt)]))
        {
            await Clients.All.SendAsync("ReceiveMessage", item.Text);
        };
    }
}