using Microsoft.Extensions.Logging;
using System;
namespace SprintMicroService.Services.Logger
{
    public interface ILoggerService
    {
        void Log(LogLevel level, string method, string message, Exception error = null);
    }
}
