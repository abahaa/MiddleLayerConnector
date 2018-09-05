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
            RequestLoggerInterceptors.LogRequest(new object[] { context.Request.Body, context.Request.Headers });
            if(AuthenticationInterceptor.AuthenticateUser(context.Request.Body) == false)
            {
                CodeLabException codelabExp = new CodeLabException
                {
                    ErrorCode = (int)ErrorCode.General_Error,
                    SubErrorCode = (int)GeneralError.Some_Thing_Went_Wrong,
                    ErrorReferenceNumber = "UU-266169856"
                };

                //codelabExp.ExtraInfoMessage = "call failed because " + Constants.GeneralErrorDic[GeneralError.Some_Thing_Went_Wrong];
                codelabExp.ExtraInfoMessage ="Didn't Recieve token";

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
