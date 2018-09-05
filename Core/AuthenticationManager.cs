using CodeLab.Barq.BackEndConnector.Mobifin.Contracts;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Controllers;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.Core
{
    public class AuthenticationManager 
    {
        private IBEAuthenticationManager Authentication;

        public AuthenticationManager(IBEAuthenticationManager Authentication)
        {
            this.Authentication = Authentication;
        }

        public LoginResponse ValidateCredentials(LoginRequest request)
        {
            try
            {
                return Authentication.ValidateCredentials(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
