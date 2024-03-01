using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using EncurtadorLink.Shared.Library;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.UseCases.Library.Services;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API - RecuperaLink", Version = "v1" });
});

#if DEBUG
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
#else
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#endif

builder.Services.AddScoped(typeof(IUrlMongoDbLazyService), typeof(UrlMongoDbLazyService));

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment() || true)
{
  app.UseSwagger();
  app.UseSwaggerUI(c => 
  {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "API - RecuperaLink v1");
  });
}

app.MapGet("{codigo}", [AllowAnonymous] async (string codigo, [FromServices] IUrlMongoDbLazyService urlMongoLazyService) =>
{
    try
    {
        if (codigo.Length == Constantes.TAMANHO_CODIGO_UNICO)
        {
            var urlMongo = await urlMongoLazyService.ConsultarPorCodigoAsync(codigo);

            if (urlMongo is not null)
                return Results.Redirect(urlMongo.UrlLonga);
        }
    }
    catch (Exception) { }

    return Results.NotFound();
});

app.MapGet("teste/ping", [AllowAnonymous] () => {
    return Results.Ok("Api - RecuperaLink esta online.");
});

app.Run();