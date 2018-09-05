using System;

namespace CodeLab.Barq.BackEndConnector.Core
{
    public class InterceptorConfig
    {
        public string ClassName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Enabled { get; set; } = false;
        public InterceptorBehavior Behavior { get; set; } = InterceptorBehavior.ContinueToNext;
        public bool IsResponseGenerator { get; set; } = false;
    }
}
