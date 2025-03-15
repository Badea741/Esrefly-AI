using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.AI;

namespace Esrefly.AiAgent;

public class CommunicationHub(IChatClient chat) : Hub
{
    public async Task SendPrompt(string prompt)
    {
        await foreach (var item in chat.GetStreamingResponseAsync(new ChatMessage(ChatRole.User, prompt)))
        {
            await Clients.All.SendAsync("ReceiveMessage", item.Text);
        };
    }
}
