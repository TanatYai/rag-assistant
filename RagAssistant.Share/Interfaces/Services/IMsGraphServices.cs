using Microsoft.Graph.Models;

namespace RagAssistant.Share.Interfaces.Services
{
    public interface IMsGraphServices
    {
        Task<List<DriveItem>> GetFilesInFolderAsync(string driveId, string folderPath);
        Task<Stream?> DownloadFileAsync(string driveId, string itemId, string fileName);
    }
}
