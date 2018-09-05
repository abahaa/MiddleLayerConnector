using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.Core
{
    [Flags]
    public enum InterceptorBehavior
    {
        ContinueToNext = 0,
        SkipFollowingInterceptors = 2,
        SkipDefaultHandler = 4
    }
}
