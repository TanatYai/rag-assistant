using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RagAssistant.Share.Interfaces.Managers;

namespace RagAssistant.Share.Managers
{
    public class MsWordManagers : IMsWordManagers
    {
        public string ExtractText(Stream stream)
        {
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            using WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, false);
            Body? body = doc.MainDocumentPart?.Document.Body;

            return body?.InnerText ?? "";
        }
    }
}
