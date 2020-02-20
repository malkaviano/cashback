using System;
using Microsoft.Extensions.Logging;

namespace Api.Helpers
{
    public static class LogHelper
    {
        public static void Error(ILogger logger, Exception ex)
        {
            logger.LogError(ex.Message);
            logger.LogError(ex.InnerException?.Message);
        }
    }
}