using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Configuration.AddJsonFile("ocelot.Development.json", optional: false, reloadOnChange: true);
#else
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
#endif

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwaggerForOcelotUI();
app.UseOcelot().Wait();

app.MapGet("/", [AllowAnonymous] () => {
    return Results.Ok("Gateway esta online.");
});

app.Run();
