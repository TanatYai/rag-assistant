namespace RagAssistant.Share.Interfaces.Services
{
    public interface IFileServices
    {
        Task<Stream?> DownloadFileAsync(string fileName, string fileUrl);
    }
}
