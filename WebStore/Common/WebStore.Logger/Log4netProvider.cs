using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4netProvider : ILoggerProvider
    {
        private readonly string _log4netConfigFile;
        private readonly ConcurrentDictionary<string, Log4netLogger> _loggers = new ConcurrentDictionary<string, Log4netLogger>();
        public Log4netProvider(string log4netConfigFile)
        {
            _log4netConfigFile = log4netConfigFile;

        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation);
        }

        private Log4netLogger CreateLoggerImplementation(string arg)
        {
            return new Log4netLogger(
                arg,
                Parselog4NetConfigFile(_log4netConfigFile));
        }

        private XmlElement Parselog4NetConfigFile(string filename)
        {
            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(File.OpenRead(filename));
            return log4NetConfig["log4net"];

        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
