using Azure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using RagAssistant.Share.Constants;
using RagAssistant.Share.Extensions;
using RagAssistant.Share.Helpers;
using RagAssistant.Share.Interfaces.Services;
using RagAssistant.Share.Settings;

namespace RagAssistant.Share.Services
{
    public class MsGraphServices : IMsGraphServices
    {
        private readonly ILogger<MsGraphServices> _logger;
        private readonly MsGraphSettings _msGraphSettings;
        private readonly GraphServiceClient _graphClient;

        public MsGraphServices(ILogger<MsGraphServices> logger, MsGraphSettings msGraphSettings)
        {
            _logger = logger;
            _msGraphSettings = msGraphSettings;

            if (_msGraphSettings.UseClientSecretCredential)
            {
                // client secret credential (no need to login)
                ClientSecretCredential credential = new(
                    _msGraphSettings.TenantId,
                    _msGraphSettings.ClientId,
                    _msGraphSettings.ClientSecret
                );

                _graphClient = new GraphServiceClient(credential);
            }
            else
            {
                // device code credential (need to login)
                DeviceCodeCredentialOptions options = new()
                {
                    TenantId = _msGraphSettings.TenantId,
                    ClientId = _msGraphSettings.ClientId,
                    DeviceCodeCallback = (info, cancellationToken) =>
                    {
                        _logger.Info(info.Message);
                        return Task.CompletedTask;
                    }
                };

                DeviceCodeCredential credential = new(options);

                _graphClient = new GraphServiceClient(credential);
            }
        }

        public async Task<List<DriveItem>> GetFilesInFolderAsync(string driveId, string folderPath)
        {
            try
            {
                // get folder
                Drive? folder = await _graphClient
                    .Drives[driveId]
                    .WithUrl(RequestHelpers.ToUrl(_msGraphSettings.BaseUrl, string.Format(EndpointConstants.MsGraph.GetMsOneDriveFolder, driveId, Uri.EscapeDataString(folderPath))))
                    .GetAsync();

                if (folder?.Id == null)
                {
                    _logger.Warning($"Folder \"{folderPath}\" not found.");
                    return [];
                }

                // get files in folder
                DriveItemCollectionResponse? children = await _graphClient
                    .Drives[driveId]
                    .Items[folder?.Id]
                    .Children
                    .GetAsync();

                return children?.Value?.ToList() ?? [];
            }
            catch (Exception ex)
            {
                _logger.Error($"Fail to get files in folder \"{folderPath}\"", ex);
                return [];
            }
        }

        public async Task<Stream?> DownloadFileAsync(string driveId, string itemId, string fileName)
        {
            try
            {
                // get file stream
                Stream? stream = await _graphClient
                    .Drives[driveId]
                    .Items[itemId]
                    .Content
                    .GetAsync();

                if (stream == null)
                {
                    _logger.Warning($"Cannot load file \"{fileName}\".");
                    return null;
                }

                // convert to memory stream
                MemoryStream memoryStream = new();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                return memoryStream;
            }
            catch (Exception ex)
            {
                _logger.Error($"Fail to download file \"{fileName}\"", ex);
                return null;
            }
        }
    }
}
