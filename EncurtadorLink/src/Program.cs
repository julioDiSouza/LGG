using EncurtadorLink.Model;
using EncurtadorLink.Services;
using EncurtadorLink.DataBase;
using EncurtadorLink.Entidades;
using EncurtadorLink.Extensoes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var strConexao = builder.Configuration.GetConnectionString("DataBaseMysql");

// Add services to the container.
builder.Services.AddSingleton(builder.Configuration.GetSection("ConfiguracaoCodigoUnico").Get<ConfiguracaoCodigoUnico>()!);
builder.Services.AddDbContext<ContextoDb>(options => options.UseMySql(strConexao, ServerVersion.AutoDetect(strConexao)));
builder.Services.AddScoped<UrlEncurtadorService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.AplicarMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//GERA O LINK PARA ENCURTAR URL
app.MapPost("api/encurtar", async (
    UrlRequisicaoModel requisicao, 
    UrlEncurtadorService urlEncurtadorService, 
    ContextoDb contextoDb, 
    HttpContext httpContext 
    ) => 
{
    if (!Uri.TryCreate(requisicao.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("URL é inválida");
    }

    var codigo = await urlEncurtadorService.GerarCodigoUnico();

    var urlEncurtada = new UrlEncurtada
    {
        Id = Guid.NewGuid(),
        UrlLonga = requisicao.Url,
        Codigo = codigo,
        UrlCurta = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{codigo}",
        DataInclusaoUtc = DateTime.Now.ToUniversalTime()
    };

    contextoDb.UrlEncurtadas.Add(urlEncurtada);
    await contextoDb.SaveChangesAsync();

    return Results.Ok(urlEncurtada.UrlCurta);
});

//RECUPERA O LINK ENCURTADO DA TABELA
app.MapGet("api/{codigo}", async (string codigo, ContextoDb contextoDb) => {
    var urlEncurtada = await contextoDb.UrlEncurtadas
    .FirstOrDefaultAsync(u => u.Codigo == codigo);

    if (urlEncurtada is null)
        return Results.NotFound();

    return Results.Redirect(urlEncurtada.UrlLonga);
});

//MONITORAMENTO
app.MapGet("api/monitoramento", () => {
    return Results.Ok("Api no ar!");
});

app.Run();
