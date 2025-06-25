using RagAssistant.Share.Enums;

namespace RagAssistant.Share.Constants
{
    public static class GlobalConstants
    {
        public static readonly List<string> SupportedFileExtensions =
        [
            FileExtensionEnums.Pdf.Value,
            FileExtensionEnums.MsWordXml.Value,
            FileExtensionEnums.MsExcelXml.Value
        ];
    }
}
