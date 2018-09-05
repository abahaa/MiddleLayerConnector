using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.Core
{
    public class BarqConfigurationManager : IBarqConfigurationManager
    {
        public string LoggingConfigFilePath
        {
            get;
            protected set;
        }

        public List<InterceptorConfig> RegisteredInterceptors
        {
            get;
            protected set;
        }
    }
}
