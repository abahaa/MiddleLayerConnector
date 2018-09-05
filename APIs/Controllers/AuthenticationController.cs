using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Controllers;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts;

namespace BackEndConnector.APIs.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IBEAuthenticationManager Authentication;

        public AuthenticationController(IBEAuthenticationManager Authentication)
        {
            this.Authentication = Authentication;
        }

        [HttpPost("ValidateCredentials")]
        public IActionResult ValidateCredentials([FromBody]LoginRequest request)
        {
            try
            {
               return Ok(Authentication.ValidateCredentials(request));
            }
            catch(CodeLabException ex)
            {
                return ExceptionHandeling.GenerateErrorResponse(ex);
            }
            catch(Exception ex)
            {
                CodeLabException codelabExp = new CodeLabException
                {
                    ErrorCode = (int)ErrorCode.General_Error,
                    SubErrorCode = (int)GeneralError.Some_Thing_Went_Wrong,
                    ErrorReferenceNumber = "UU-266169856"
                };

                //codelabExp.ExtraInfoMessage = "call failed because " + Constants.GeneralErrorDic[GeneralError.Some_Thing_Went_Wrong];
                codelabExp.ExtraInfoMessage = ex.Message;
                return ExceptionHandeling.GenerateErrorResponse(codelabExp);
            }
        }
    }
}