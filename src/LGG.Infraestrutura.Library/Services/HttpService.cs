using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Infraestrutura.Library.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            using var content = Serialize(data);

            using var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            return await Deserialize<TResponse>(response);
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data)
        {
            using var content = Serialize(data);

            using var response = await _httpClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();

            return await Deserialize<TResponse>(response);
        }

        public async Task DeleteAsync(string url)
        {
            using var response = await _httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
        }

        private StringContent Serialize<T>(T obj)
        {
            var json = JsonSerializer.Serialize(obj, _jsonOptions);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T?> Deserialize<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }
    }
}