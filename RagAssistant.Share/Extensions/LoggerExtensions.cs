using Microsoft.Extensions.Logging;
using Serilog;

namespace RagAssistant.Share.Extensions
{
    public static class LoggerExtensions
    {
        public static void Info<T>(this ILogger<T> logger, string message)
        {
            Log.Information($"[{typeof(T).Assembly.GetName().Name}] [{typeof(T).Name}] {message}");
        }

        public static void Warning<T>(this ILogger<T> logger, string message)
        {
            Log.Warning($"[{typeof(T).Assembly.GetName().Name}] [{typeof(T).Name}] {message}");
        }

        public static void Error<T>(this ILogger<T> logger, string message, Exception ex)
        {
            Log.Error(ex, $"[{typeof(T).Assembly.GetName().Name}] [{typeof(T).Name}] {message}: {ex.Message}");
        }

        public static void Error<T>(this ILogger<T> logger, Exception ex)
        {
            Log.Error(ex, $"[{typeof(T).Assembly.GetName().Name}] [{typeof(T).Name}] {ex.Message}");
        }

        public static void Debug<T>(this ILogger<T> logger, string message)
        {
            Log.Debug($"[{typeof(T).Assembly.GetName().Name}] [{typeof(T).Name}] {message}");
        }
    }
}
