using Microsoft.Extensions.Logging;
using RagAssistant.Share.Extensions;
using RagAssistant.Share.Interfaces.Services;

namespace RagAssistant.Share.Services
{
    public class FileServices : IFileServices
    {
        private readonly ILogger<FileServices> _logger;
        private readonly HttpClient _httpClient;

        public FileServices(ILogger<FileServices> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<Stream?> DownloadFileAsync(string fileName, string fileUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode(); // will throw if status code is not success

                return await response.Content.ReadAsStreamAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Fail to download file \"{fileName}\"", ex);
                return null;
            }
        }
    }
}
