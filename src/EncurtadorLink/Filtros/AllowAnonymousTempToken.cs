using EncurtadorLink.Shared.Library.Classes;

namespace EncurtadorLink.Filtros
{
    public class AllowAnonymousTempToken : Attribute, IEndpointFilter
    {
        private IConfiguration _configuration;

        public AllowAnonymousTempToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            try
            {
                var tokenValido = Criptografador.TokenTemporarioValido(context.HttpContext.Request.Query["temp_token"]!, _configuration["TempTokenKey"]!);

                if (tokenValido)
                    return await next(context);
            }
            catch (Exception) { }

            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.HttpContext.Response.WriteAsync("Token invalido.");

            return await next(context);
        }
    }
}
