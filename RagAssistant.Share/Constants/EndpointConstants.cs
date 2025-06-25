namespace RagAssistant.Share.Constants
{
    public static class EndpointConstants
    {
        public static class MsGraph
        {
            public const string GetMsOneDriveFolder = "drives/{0}/root:/{1}";
        }

        public static class Ollama
        {
            public const string GetEmbedding = "api/embeddings";
        }

        public static class Qdrant
        {
            public const string CreateCollection = "collections/{0}";
            public const string DeleteCollectionByName = "collections/{0}";
            public const string CreateDocuments = "collections/{0}/points?wait=true&ordering=weak";
        }
    }
}
