using Microsoft.Graph.Models;
using RagAssistant.Job.Constants;
using RagAssistant.Share.Constants;
using RagAssistant.Share.Extensions;
using RagAssistant.Share.Interfaces.Managers;
using RagAssistant.Share.Interfaces.Services;
using RagAssistant.Share.Models.Services.QdrantInsertVector;
using RagAssistant.Share.Settings;

namespace RagAssistant.Job.Jobs
{
    public class SyncFileJob
    {
        private readonly MsGraphSettings _msGraphSettings;
        private readonly IFileManagers _fileManagers;
        private readonly IMsGraphServices _msGraphServices;
        private readonly IOllamaServices _iollamaServices;
        private readonly IQdrantService _qdrantService;

        public SyncFileJob(MsGraphSettings msGraphSettings, IFileManagers fileManagers, IMsGraphServices msGraphServices, IOllamaServices iollamaServices, IQdrantService qdrantService)
        {
            _msGraphSettings = msGraphSettings;
            _fileManagers = fileManagers;
            _msGraphServices = msGraphServices;
            _iollamaServices = iollamaServices;
            _qdrantService = qdrantService;
        }

        public async Task RunAsync()
        {
            // delete (is exist) and create new collection
            await _qdrantService.DeleteCollectionAsync(AppConstants.DestinationCollectionName);
            await _qdrantService.CreateCollectionAsync(AppConstants.DestinationCollectionName);

            // get files in folder on OneDrive
            List<DriveItem> files = await _msGraphServices.GetFilesInFolderAsync(_msGraphSettings.DriveId, AppConstants.SourceFolderPath);
            files = files.Where(i => GlobalConstants.SupportedFileExtensions.Contains(Path.GetExtension(i.Name!).ToLowerInvariant())).ToList();

            foreach (DriveItem file in files)
            {
                // get file content
                Stream? stream = await _msGraphServices.DownloadFileAsync(_msGraphSettings.DriveId, file.Id!, file.Name!);
                if (stream == null)
                {
                    continue;
                }

                // extract file context to plain text
                string text = _fileManagers.ExtractText(file.Name!, stream);
                if (string.IsNullOrWhiteSpace(text))
                {
                    continue;
                }

                // split plain text to chunk by paragraph
                List<string> chunks = text.CleanText().ChunkByParagraph();

                List<QdrantInsertVectorItemRequest> items = [];
                for (int i = 0; i < chunks.Count; i++)
                {
                    string chunk = chunks[i];

                    // embedding by Ollama
                    float[]? embedding = await _iollamaServices.GetEmbeddingAsync(chunk);
                    if (embedding == null || embedding.Length == 0)
                    {
                        continue;
                    }

                    // prepare data item
                    items.Add(new QdrantInsertVectorItemRequest
                    {
                        Id = Guid.NewGuid().ToString(),
                        Vector = embedding,
                        Payload = new QdrantInsertVectorItemMetadataRequest()
                        {
                            ChunkIndex = i,
                            FileName = file.Name!,
                            FileExtension = Path.GetExtension(file.Name!).ToLowerInvariant(),
                            Text = chunk
                        }
                    });
                }

                // insert document
                if (items.Any())
                {
                    await _qdrantService.CreateDocumentsAsync(AppConstants.DestinationCollectionName, items);
                }
            }
        }
    }
}
