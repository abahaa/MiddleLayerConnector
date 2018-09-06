using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json.Linq;

namespace BackEndConnector.Interceptors
{
    public class AuthenticationInterceptor
    {
        public static CodeLabException AuthenticateUser(HttpRequest request)
        {
            if (request.Method.ToLower() == "post")
            {
                string requestBody = ReadBodyAsString(request);
                JObject body = JObject.Parse(requestBody);
                if (body["token"] == null)
                {

                    CodeLabException codelabExp = new CodeLabException
                    {
                        ErrorCode = (int)ErrorCode.General_Error,
                        SubErrorCode = (int)GeneralError.Token_Not_Exist,
                        ErrorReferenceNumber = "UU-266169856"
                    };

                    //codelabExp.ExtraInfoMessage = "call failed because " + Constants.GeneralErrorDic[GeneralError.Some_Thing_Went_Wrong];
                    codelabExp.ExtraInfoMessage = Constants.GeneralErrorDic[GeneralError.Token_Not_Exist];

                    return codelabExp;
                }
            }
            return null;
        }

        public static string ReadBodyAsString(HttpRequest request)
        {
            var initialBody = request.Body; // Workaround
            try
            {
                request.EnableRewind();

                using (StreamReader reader = new StreamReader(request.Body))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
            finally
            {
                // Workaround so MVC action will be able to read body as well
                request.Body = initialBody;
            }
            return string.Empty;
        }
    }
}
