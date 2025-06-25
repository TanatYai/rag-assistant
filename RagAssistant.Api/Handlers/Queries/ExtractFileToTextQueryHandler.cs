using MediatR;
using RagAssistant.Api.ApiDataObjects.ExtractFileToText;
using RagAssistant.Share.Interfaces.Managers;
using RagAssistant.Share.Interfaces.Services;

namespace RagAssistant.Api.Handlers.Queries
{
    public class ExtractFileToTextQueryHandler : IRequestHandler<ExtractFileToTextQueryRequest, ExtractFileToTextQueryResponse>
    {
        private readonly IFileServices _fileServices;
        private readonly IFileManagers _fileManagers;

        public ExtractFileToTextQueryHandler(IFileServices fileServices, IFileManagers fileManagers)
        {
            _fileServices = fileServices;
            _fileManagers = fileManagers;
        }

        public async Task<ExtractFileToTextQueryResponse> Handle(ExtractFileToTextQueryRequest request, CancellationToken cancellationToken)
        {
            ExtractFileToTextQueryResponse response = new()
            {
                FileName = request.FileName
            };

            // get file content
            Stream? stream = await _fileServices.DownloadFileAsync(request.FileName, request.FileDownloadUrl);
            if (stream == null)
            {
                return response;
            }

            // extract file context to plain text
            string text = _fileManagers.ExtractText(request.FileName, stream);
            if (string.IsNullOrWhiteSpace(text))
            {
                return response;
            }

            response.Text = text;

            return response;
        }
    }
}
