using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Controllers;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.BackEndMockups.Wrappers
{
    public class BEAuthenticationManager : IBEAuthenticationManager
    {
        public LoginResponse ValidateCredentials(LoginRequest request)
        {
            HttpResponseMessage response = MockupsAPIsCaller.CallAPI(request, "api/Auth/ValidateCredentials");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string ResponseString = (response.Content.ReadAsStringAsync()).Result;
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(ResponseString);
                return loginResponse;
            }
            else if(response.StatusCode == (HttpStatusCode)490)
            {
                throw MockupsAPIsCaller.GenerateError(response);
            }
            throw new Exception();
        }

        public OTPValidatorResponse ValidateOTPLogin(OTPValidatorRequest request)
        {
            HttpResponseMessage response = MockupsAPIsCaller.CallAPI(request, "api/Auth/ValidateOTPLogin");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string ResponseString = (response.Content.ReadAsStringAsync()).Result;
                OTPValidatorResponse oTPValidatorResponse = JsonConvert.DeserializeObject<OTPValidatorResponse>(ResponseString);
                return oTPValidatorResponse;
            }
            else if (response.StatusCode == (HttpStatusCode)490)
            {
               throw MockupsAPIsCaller.GenerateError(response);
            }
            throw new Exception();
        }
    }
}
