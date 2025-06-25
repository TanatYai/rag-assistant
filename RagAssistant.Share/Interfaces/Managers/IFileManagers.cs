namespace RagAssistant.Share.Interfaces.Managers
{
    public interface IFileManagers
    {
        string ExtractText(string fileName, Stream stream);
    }
}
