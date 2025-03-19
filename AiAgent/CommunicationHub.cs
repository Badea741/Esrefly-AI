using Esrefly.Features.Shared.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Newtonsoft.Json;

namespace Esrefly.AiAgent;

public class CommunicationHub(IChatClient chat,
    Features.User.Read.IQueryService queryService, ApplicationDbContext context) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var userId = httpContext?.Request.Query["userId"].ToString() ?? "Unknown";

        Console.WriteLine($"User Connected: {userId}");

        // Store the user connection if needed
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);

        await base.OnConnectedAsync();
    }

    public async Task SendPrompt(string userId, string prompt)
    {
        var userPrompts = await context.Users
            .Include(x =>
                x.Prompts
                .Where(x => DateOnly.FromDateTime(x.CreatedDate.Date) ==
                    DateOnly.FromDateTime(DateTimeOffset.UtcNow.Date))
                .OrderByDescending(x => x.CreatedDate)
                .Take(3))
            .Where(x => x.ExternalId == userId)
            .FirstOrDefaultAsync() ?? throw new Exception("User not found");

        var userData = await queryService.GetUserFinancialData(
            new Features.User.Read.Query(userId), CancellationToken.None);
        var userDataChatMessage = new ChatMessage(ChatRole.System, JsonConvert.SerializeObject(userData));
        var userDayHistoryMessages = userPrompts.Prompts.Select(x => new ChatMessage(ChatRole.System, x.Value));
        var response = "";
        await foreach (var item in chat.GetStreamingResponseAsync(
            [userDataChatMessage, .. userDayHistoryMessages, new ChatMessage(ChatRole.User, prompt)]))
        {
            response += item.Text;
            await Clients.Group(userId).SendAsync("ReceiveMessage", item.Text);
        };
        userPrompts.Prompts.Add(new Prompt(prompt, response));
        await context.SaveChangesAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}