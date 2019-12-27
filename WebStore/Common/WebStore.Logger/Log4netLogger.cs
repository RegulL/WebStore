using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4netLogger : ILogger
    {
        private readonly ILog log;
        private ILoggerRepository loggerRepository;

        public Log4netLogger(string name, XmlElement element)
        {
            loggerRepository = LogManager.CreateRepository(
            Assembly.GetEntryAssembly(),
            typeof(log4net.Repository.Hierarchy.Hierarchy));
            log = LogManager.GetLogger(loggerRepository.Name, name);
            log4net.Config.XmlConfigurator.Configure(
                loggerRepository,
                element);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return log.IsFatalEnabled;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    return log.IsDebugEnabled;
                case LogLevel.Error:
                    return log.IsErrorEnabled;
                case LogLevel.Information:
                    return log.IsInfoEnabled;
                case LogLevel.Warning:
                    return log.IsWarnEnabled;
                case LogLevel.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            if (formatter == null) 
            {
                throw new ArgumentException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message) && exception == null)
                return;

            switch (logLevel)
            {
                case LogLevel.Critical:
                    log.Fatal(message);
                    break;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    log.Debug(message);
                    break;
                case LogLevel.Error:
                    log.Error(message);
                    break;
                case LogLevel.Information:
                    log.Info(message);
                    break;
                case LogLevel.Warning:
                    log.Warn(message);
                    break;
                case LogLevel.None:
                    break;
                default:
                    log.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                    log.Info(message, exception);
                    break;

            }
        }
        
        
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
