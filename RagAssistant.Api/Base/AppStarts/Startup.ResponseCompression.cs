using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace RagAssistant.Api.Base.AppStarts
{
    public static partial class Startup
    {
        public static void AddResponseCompressionService(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        public static void UseResponseCompressionService(this IApplicationBuilder app)
        {
            app.UseResponseCompression();
        }
    }
}
