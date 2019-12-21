using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Logger
{
    public static class Log4netExtensions
    {
        public static ILoggerFactory AddLog4net(this ILoggerFactory factory, string log4netConfigFile) 
        {
            factory.AddProvider(new Log4netProvider(log4netConfigFile));
            return factory;
        }

        public static ILoggerFactory AddLog4net(this ILoggerFactory factory)
        {
            factory.AddLog4net("log4net.config");
            return factory;
        }
    }
}
