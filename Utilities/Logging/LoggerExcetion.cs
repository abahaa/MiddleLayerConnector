using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Utilities.Logging
{
    class LoggerExcetion : Exception
    {


        public LoggerExcetion() : base()
        { }
        

        public LoggerExcetion(string message) : base(message)
        { }

        public LoggerExcetion(string message, Exception innerException) : base(message, innerException)
        { }

        public LoggerExcetion(LoggerFailureType type) : base()
        {
            LoggerErrorType = type;
        }

        public LoggerExcetion(string message, LoggerFailureType type) : base(message)
        {
            LoggerErrorType = type;
        }

        public LoggerExcetion(string message, Exception innerException, LoggerFailureType type) : base(message, innerException)
        {
            LoggerErrorType = type;
        }

        public LoggerFailureType LoggerErrorType { get; set; }
    }
}
