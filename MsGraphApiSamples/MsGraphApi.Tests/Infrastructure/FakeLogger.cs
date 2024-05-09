using MsGraphApi.Infrastructure;
using System;

namespace MsGraphApi.Tests.Infrastructure
{
    internal class FakeLogger : ILogger
    {
        public Exception Exception { get; set; }

        public void LogException(Exception exception)
        {
            Exception = exception;
        }
    }
}
