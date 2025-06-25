namespace RagAssistant.Share.Interfaces.Services
{
    public interface IOllamaServices
    {
        Task<float[]> GetEmbeddingAsync(string text);
    }
}
