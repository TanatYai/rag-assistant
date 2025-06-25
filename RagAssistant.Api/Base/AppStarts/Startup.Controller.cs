using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text.Json.Serialization;

namespace RagAssistant.Api.Base.AppStarts
{
    public static partial class Startup
    {
        public static void AddControllerService(this IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 3 * 1024 * 1024; // Set to 3MB (3 * 1024 * 1024 bytes)
            });

            services.AddControllers(
                opt =>
                {

                }
                ).ConfigureApiBehaviorOptions(options =>
                {

                }).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
        }
    }
}
