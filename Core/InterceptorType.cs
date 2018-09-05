using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.Core
{
    [Flags]
    public enum InterceptorType
    {
        PreRequestInterceptor = 0,
        PostRequestInterceptor = 2,
        PostResponseInterceptor = 4
    }
}
