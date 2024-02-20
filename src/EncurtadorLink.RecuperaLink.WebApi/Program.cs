using Microsoft.AspNetCore.Mvc;
using EncurtadorLink.Shared.Library;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.UseCases.Library.Services;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IUrlMongoDbLazyService), typeof(UrlMongoDbLazyService));

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.MapGet("teste/monitoramento", [AllowAnonymous] () => {
    return Results.Ok("Api no ar.");
});

app.Run();

