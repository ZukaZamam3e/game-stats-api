using FastEndpoints;
using FastEndpoints.Swagger;
using GameStats.API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseFastEndpoints()
        .UseSwaggerGen(uiConfig: c =>
        {
            c.DocExpansion = "list"; // or "full" for all expanded, "list" for tags only
        });
}
else
{
    app.UseFastEndpoints();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();


app.Run();
