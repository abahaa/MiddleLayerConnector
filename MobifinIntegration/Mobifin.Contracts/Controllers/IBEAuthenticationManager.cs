using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses;


namespace  CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Controllers
{
    public interface IBEAuthenticationManager
    {

        LoginResponse ValidateCredentials(LoginRequest request);

        OTPValidatorResponse ValidateOTPLogin(OTPValidatorRequest request);

    }



}