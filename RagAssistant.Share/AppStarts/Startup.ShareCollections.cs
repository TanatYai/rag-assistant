using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RagAssistant.Share.Interfaces.Managers;
using RagAssistant.Share.Interfaces.Services;
using RagAssistant.Share.Managers;
using RagAssistant.Share.Services;
using RagAssistant.Share.Settings;

namespace RagAssistant.Share.AppStarts
{
    public static partial class Startup
    {
        public static void AddFileCollection(this IServiceCollection services)
        {
            services.AddHttpClient<IFileServices, FileServices>();
            services.AddSingleton<IFileManagers, FileManagers>();
            services.AddSingleton<IPdfManagers, PdfManagers>();
            services.AddSingleton<IMsWordManagers, MsWordManagers>();
            services.AddSingleton<IMsExcelManagers, MsExcelManagers>();
        }

        public static void AddMsGraphCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection(nameof(MsGraphSettings)).Get<MsGraphSettings>()!);
            services.AddSingleton<IMsGraphServices, MsGraphServices>();
        }

        public static void AddOllamaCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection(nameof(OllamaSettings)).Get<OllamaSettings>()!);
            services.AddHttpClient<IOllamaServices, OllamaServices>();
        }

        public static void AddQdrantCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection(nameof(QdrantSettings)).Get<QdrantSettings>()!);
            services.AddHttpClient<IQdrantService, QdrantService>();
        }
    }
}
