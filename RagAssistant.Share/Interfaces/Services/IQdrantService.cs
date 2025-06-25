using RagAssistant.Share.Models.Services.QdrantInsertVector;

namespace RagAssistant.Share.Interfaces.Services
{
    public interface IQdrantService
    {
        Task CreateCollectionAsync(string collectionName);
        Task DeleteCollectionAsync(string collectionName);
        Task CreateDocumentsAsync(string collectionName, List<QdrantInsertVectorItemRequest> items);
        //Task<List<QdrantSearchResponse>> SearchSimilarAsync(string collectionName, float[] embedding, int topK = 5);
    }
}
