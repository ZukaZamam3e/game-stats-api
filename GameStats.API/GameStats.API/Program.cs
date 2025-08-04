using FluentValidation;
using GameStats.API.Abstract;
using GameStats.API.SetUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterEndpointsFromAssemblyContaining<IApiMarker>();

builder.Services.AddGameStatsDb(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<IApiMarker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapEndpoints();

app.Run();
