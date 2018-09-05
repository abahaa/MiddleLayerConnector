using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BackEndConnector.Interceptors
{
    public class AuthenticationInterceptor
    {
        public static bool AuthenticateUser(object requestBody)
        {
           JObject body =  JObject.FromObject(requestBody);
            if(body["token"] == null)
            {
                return false;
            }
            return true;
        }
    }
}
