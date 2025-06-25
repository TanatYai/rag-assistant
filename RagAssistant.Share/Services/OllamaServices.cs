using Microsoft.Extensions.Logging;
using RagAssistant.Share.Constants;
using RagAssistant.Share.Extensions;
using RagAssistant.Share.Helpers;
using RagAssistant.Share.Interfaces.Services;
using RagAssistant.Share.Models.Services.OllamaEmbedding;
using RagAssistant.Share.Settings;
using System.Net.Http.Json;

namespace RagAssistant.Share.Services
{
    public class OllamaServices : IOllamaServices
    {
        private readonly ILogger<OllamaServices> _logger;
        private readonly OllamaSettings _ollamaSettings;
        private readonly HttpClient _httpClient;

        public OllamaServices(ILogger<OllamaServices> logger, OllamaSettings ollamaSettings, HttpClient httpClient)
        {
            _logger = logger;
            _ollamaSettings = ollamaSettings;
            _httpClient = httpClient;
        }

        public async Task<float[]> GetEmbeddingAsync(string text)
        {
            try
            {
                OllamaEmbeddingRequest request = new()
                {
                    Model = "nomic-embed-text",
                    Prompt = text
                };

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(RequestHelpers.ToUrl(_ollamaSettings.BaseUrl, EndpointConstants.Ollama.GetEmbedding), request);
                response.EnsureSuccessStatusCode();

                OllamaEmbeddingResponse? json = await response.Content.ReadFromJsonAsync<OllamaEmbeddingResponse>();
                return json?.Embedding ?? [];
            }
            catch (Exception ex)
            {
                _logger.Error($"Fail to embedded text", ex);
                return [];
            }
        }
    }
}
