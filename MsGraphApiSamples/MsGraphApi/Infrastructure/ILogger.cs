using System;

namespace MsGraphApi.Infrastructure
{
    public interface ILogger
    {
        void LogException(Exception exception);
    }
}
