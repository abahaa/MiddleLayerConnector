using CodeLab.Barq.BackEndConnector.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndConnector.Interceptors
{
    public class RequestLoggerInterceptors
    {
        public static void LogRequest(object[] request)
        {
            DefaultLogger.LogTrace("", request);
        }
    }
}
