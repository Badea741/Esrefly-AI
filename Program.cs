using Esrefly;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var configuration = builder.Configuration;
var aiAgentOptions = configuration.GetSection("AiAgent").Get<Esrefly.AiAgent.Options>()!;
builder.Services.AddSingleton<IChatClient>(new OllamaChatClient(aiAgentOptions.Endpoint, aiAgentOptions.ModelName));



builder.Services.AddScoped(typeof(Esrefly.Features.Shared.Repositories.IRepository<>), typeof(Esrefly.Features.Shared.Repositories.Repository<>));
builder.Services.AddScoped<Esrefly.Features.Expenses.Services.IExpenseService, Esrefly.Features.Expenses.Services.ExpenseService>();
builder.Services.AddScoped(typeof(Esrefly.Features.Shared.Repositories.IRepository<>), typeof(Esrefly.Features.Shared.Repositories.Repository<>));
builder.Services.AddScoped<Esrefly.Features.Expenses.Services.IExpenseService, Esrefly.Features.Expenses.Services.ExpenseService>();
builder.Services.AddScoped<Esrefly.Features.Incomes.Services.IIncomeService, Esrefly.Features.Incomes.Services.IncomeService>();
builder.Services.AddScoped<Esrefly.Features.Goals.Services.IGoalService, Esrefly.Features.Goals.Services.GoalService>();


var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<List<string>>()!;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins([.. allowedOrigins])
               .AllowAnyMethod()
               .AllowAnyHeader()
        .AllowCredentials();
    });
});
var connectionString = configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection String: {connectionString}");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(connectionString, option => option.EnableRetryOnFailure())
.EnableDetailedErrors()
.EnableSensitiveDataLogging());


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors("AllowAll");
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapHub<Esrefly.AiAgent.CommunicationHub>("/agent");


app.Run();
