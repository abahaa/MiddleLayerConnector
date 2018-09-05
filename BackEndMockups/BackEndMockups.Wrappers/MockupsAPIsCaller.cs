using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.BackEndMockups.Wrappers
{
    public class MockupsAPIsCaller
    {
        private static readonly HttpClient client = new HttpClient();
        private static  string StrartingURL = "http://localhost:8090/";

        public static HttpResponseMessage CallAPI(object request,string url)
        {
            string JsonLoginRequest = JsonConvert.SerializeObject(request);
            string fullURL = StrartingURL + url;
            var response = client.PostAsync(fullURL, new StringContent(JsonLoginRequest, Encoding.UTF8, "application/json"));
            return response.Result;
        }

        public static CodeLabException GenerateError(HttpResponseMessage response)
        {
            string ResponseString = (response.Content.ReadAsStringAsync()).Result;
            List<CodeLabException> exceptions = JsonConvert.DeserializeObject<List<CodeLabException>>(ResponseString);
            return exceptions[0];
        }
    }
}
