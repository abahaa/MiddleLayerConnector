using System.Collections.Generic;

namespace CodeLab.Barq.BackEndConnector.Core
{
    public interface IBarqConfigurationManager
    {
        string LoggingConfigFilePath { get; }
        List<InterceptorConfig> RegisteredInterceptors { get; }
    }
}