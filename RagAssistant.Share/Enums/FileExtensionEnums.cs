using Ardalis.SmartEnum;

namespace RagAssistant.Share.Enums
{
    public class FileExtensionEnums : SmartEnum<FileExtensionEnums, string>
    {
        public static readonly FileExtensionEnums Pdf = new(".pdf", "PDF File");
        public static readonly FileExtensionEnums MsWordXml = new(".docx", "Microsoft Word File");
        public static readonly FileExtensionEnums MsExcelXml = new(".xlsx", "Microsoft Excel File");

        private FileExtensionEnums(string value, string name) : base(name, value)
        {
        }
    }
}
