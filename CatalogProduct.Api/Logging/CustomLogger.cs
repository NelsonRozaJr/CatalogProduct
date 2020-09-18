using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace CatalogProduct.Api.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly CustomLoggerProviderConfiguration _config;

        private readonly string _loggerName;

        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            _config = config;
            _loggerName = name;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
            string path = Path.Combine(Environment.CurrentDirectory, "Log.txt");

            using (StreamWriter stream = new StreamWriter(path, true))
            {
                stream.WriteLine(message);
            }
        }
    }
}