using Polly;
using Polly.Retry;

namespace Application.ApiPolicy;

public static class PollyPolicy
{
    public static AsyncRetryPolicy CreateRetryPolicy(Microsoft.Extensions.Logging.ILogger 
        logger ,TimeSpan time,int retryCount = 3)
    {
        return Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryCount: retryCount, 
            sleepDurationProvider:retryTime=>time * retryTime,
            onRetry:(exception,time,retryCount,context)
             =>
            {
                logger.LogInformation($"خطای فراخوانی وب سرویس - تلاش ناموفق {exception.Message}");
            });
    }
} 
