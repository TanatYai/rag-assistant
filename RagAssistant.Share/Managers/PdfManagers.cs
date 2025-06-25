using RagAssistant.Share.Interfaces.Managers;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace RagAssistant.Share.Managers
{
    public class PdfManagers : IPdfManagers
    {
        public string ExtractText(Stream stream)
        {
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            StringBuilder sb = new();

            using PdfDocument pdf = PdfDocument.Open(memoryStream);
            foreach (Page page in pdf.GetPages())
            {
                sb.AppendLine(page.Text);
            }

            return sb.ToString();
        }
    }
}
