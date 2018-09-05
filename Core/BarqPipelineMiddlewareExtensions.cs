using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.Core
{
    public static class BarqPipelineMiddlewareExtensions
    {
        public static IApplicationBuilder UseBarqPipline(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BarqPipelineMiddleware>();
        }
    }
}
