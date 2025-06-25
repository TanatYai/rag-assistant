namespace RagAssistant.Share.Settings
{
    public class MsGraphSettings
    {
        public string BaseUrl { get; set; } = default!;
        public bool UseClientSecretCredential { get; set; }
        public string TenantId { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string DriveId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
    }
}
