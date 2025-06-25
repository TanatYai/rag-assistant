using Microsoft.Extensions.Logging;
using RagAssistant.Share.Enums;
using RagAssistant.Share.Extensions;
using RagAssistant.Share.Interfaces.Managers;

namespace RagAssistant.Share.Managers
{
    public class FileManagers : IFileManagers
    {
        private readonly ILogger<FileManagers> _logger;
        private readonly IPdfManagers _pdfManagers;
        private readonly IMsWordManagers _msWordManagers;
        private readonly IMsExcelManagers _msExcelManagers;

        public FileManagers(ILogger<FileManagers> logger, IPdfManagers pdfManagers, IMsWordManagers msWordManagers, IMsExcelManagers msExcelManagers)
        {
            _logger = logger;
            _pdfManagers = pdfManagers;
            _msWordManagers = msWordManagers;
            _msExcelManagers = msExcelManagers;
        }

        public string ExtractText(string fileName, Stream stream)
        {
            string ext = Path.GetExtension(fileName).ToLowerInvariant();

            switch (ext)
            {
                case var value when value == FileExtensionEnums.Pdf.Value:
                    return _pdfManagers.ExtractText(stream);

                case var value when value == FileExtensionEnums.MsWordXml.Value:
                    return _msWordManagers.ExtractText(stream);

                case var value when value == FileExtensionEnums.MsExcelXml.Value:
                    return _msExcelManagers.ExtractText(stream);

                default:
                    _logger.Warning($"Unsupported file type \"{ext}\" for \"{fileName}\".");
                    return string.Empty;
            }
        }
    }
}
