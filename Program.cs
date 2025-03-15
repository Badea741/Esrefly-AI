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


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("localhost:5000")
               .AllowAnyMethod()
               .AllowAnyHeader()
        .AllowCredentials(); // Required for SignalR
    });
});

var app = builder.Build();

app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<Esrefly.AiAgent.CommunicationHub>("/agent");


app.Run();
