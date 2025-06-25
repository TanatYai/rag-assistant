using Microsoft.Extensions.Logging;
using RagAssistant.Share.Constants;
using RagAssistant.Share.Extensions;
using RagAssistant.Share.Helpers;
using RagAssistant.Share.Interfaces.Services;
using RagAssistant.Share.Models.Services.QdrantCreateCollection;
using RagAssistant.Share.Models.Services.QdrantInsertVector;
using RagAssistant.Share.Settings;
using System.Net.Http.Json;

namespace RagAssistant.Share.Services
{
    public class QdrantService : IQdrantService
    {
        private readonly ILogger<QdrantService> _logger;
        private readonly QdrantSettings _qdrantSettings;
        private readonly HttpClient _httpClient;

        public QdrantService(ILogger<QdrantService> logger, QdrantSettings qdrantSettings, HttpClient httpClient)
        {
            _logger = logger;
            _qdrantSettings = qdrantSettings;
            _httpClient = httpClient;
        }

        public async Task CreateCollectionAsync(string collectionName)
        {
            try
            {
                QdrantCreateCollectionRequest request = new()
                {
                    Vectors = new QdrantCreateCollectionItemRequest()
                    {
                        Size = 768, // nomic-embed-text dimension
                        Distance = "Cosine"
                    }
                };

                string createUrl = RequestHelpers.ToUrl(_qdrantSettings.BaseUrl, string.Format(EndpointConstants.Qdrant.CreateCollection, collectionName));
                await _httpClient.PutAsJsonAsync(createUrl, request);
            }
            catch (Exception ex)
            {
                _logger.Error($"Fail to create collection \"{collectionName}\"", ex);
                throw;
            }
        }

        public async Task DeleteCollectionAsync(string collectionName)
        {
            try
            {
                string deleteUrl = RequestHelpers.ToUrl(_qdrantSettings.BaseUrl, string.Format(EndpointConstants.Qdrant.DeleteCollectionByName, collectionName));
                await _httpClient.DeleteAsync(deleteUrl);
            }
            catch (Exception ex)
            {
                _logger.Error($"Fail to delete collection \"{collectionName}\"", ex);
                throw;
            }
        }

        public async Task CreateDocumentsAsync(string collectionName, List<QdrantInsertVectorItemRequest> items)
        {
            try
            {
                QdrantInsertVectorRequest request = new()
                {
                    Points = items
                };

                await _httpClient.PutAsJsonAsync(RequestHelpers.ToUrl(_qdrantSettings.BaseUrl, string.Format(EndpointConstants.Qdrant.CreateDocuments, collectionName)), request);
            }
            catch (Exception ex)
            {
                _logger.Error($"Fail to create documents into collection \"{collectionName}\"", ex);
                throw;
            }
        }

        //public async Task<List<QdrantSearchResponse>> SearchSimilarAsync(string collectionName, float[] embedding, int topK = 5)
        //{
        //    var payload = new
        //    {
        //        vector = embedding,
        //        limit = topK,
        //        with_payload = true
        //    };

        //    HttpResponseMessage res = await _httpClient.PostAsJsonAsync($"{_qdrantSettings.BaseUrl}/collections/{collectionName}/points/search", payload);
        //    res.EnsureSuccessStatusCode();

        //    string json = await res.Content.ReadAsStringAsync();
        //    JsonElement root = JsonDocument.Parse(json).RootElement;
        //    List<QdrantSearchResponse> results = [];

        //    foreach (JsonElement point in root.GetProperty("result").EnumerateArray())
        //    {
        //        JsonElement payloadData = point.GetProperty("payload");

        //        results.Add(new QdrantSearchResponse
        //        {
        //            Id = point.GetProperty("id").ToString(),
        //            Score = point.GetProperty("score").GetSingle(),
        //            Text = payloadData.GetProperty("text").GetString() ?? string.Empty,
        //            FileName = payloadData.GetProperty("file_name").GetString() ?? string.Empty,
        //            FileExtension = payloadData.GetProperty("file_extension").GetString() ?? string.Empty,
        //            ChunkIndex = payloadData.GetProperty("chunk_index").GetInt32()
        //        });
        //    }

        //    return results;
        //}
    }
}
