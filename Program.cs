using BackEnd_LevelUp.Data;
using BackEnd_LevelUp.Interfaces;
using BackEnd_LevelUp.Repositories;
using BackEnd_LevelUp.Services;
using BackEnd_LevelUp.validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=recommendations.db"));

builder.Services.AddScoped<IGameRecommendationRepository, GameRecommendationRepository>();
builder.Services.AddHttpClient<IFreeToGameClient, FreeToGameClient>(client =>
{
    client.BaseAddress = new Uri("https://www.freetogame.com/api/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RecommendationRequestValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();