using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace LGG.Global.Library.Extensoes
{
    public static class JsonExtensao
    {
        public static bool SafeDeserialize<T>(string json, ILogger logger, out T? result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null)
                {
                    logger.LogWarning("Desserialização retornou null para {Type}", typeof(T).Name);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao desserializar JSON para {Type}", typeof(T).Name);
                result = default;
                return false;
            }
        }
    }
}