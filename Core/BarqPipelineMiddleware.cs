using BackEndConnector.Interceptors;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Core
{
    public class BarqPipelineMiddleware
    {
        private readonly RequestDelegate _next;

        public BarqPipelineMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //RequestLoggerInterceptors.LogRequest(new object[] { context.Request.Body, context.Request.Headers });
            CodeLabException codelabExp = AuthenticationInterceptor.AuthenticateUser(context.Request);
            if (codelabExp != null)
            {
                context.Response.StatusCode = 490;
                context.Response.ContentType = "application/json";
                string jsonString = JsonConvert.SerializeObject(codelabExp);
                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
               // to stop futher pipeline execution 
               return;  
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
