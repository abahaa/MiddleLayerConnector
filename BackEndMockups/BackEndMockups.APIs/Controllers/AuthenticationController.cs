using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using BarqMockupsLib;
using Microsoft.Extensions.Configuration;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts;


namespace CodeLab.Barq.BackEndConnector.BackEndMockups.APIs.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthenticationController : Controller
    {
        private BarqBECoreMockContext Context;


        public AuthenticationController(IConfiguration configuration, BarqBECoreMockContext Context)
        {
            this.Context = Context;
        }

        [HttpGet]
        public int Get()
        {
            return 2;
        }


        [HttpPost("ValidateCredentials")]
        public IActionResult ValidateCredentials([FromBody]LoginRequest request)
        {
            if(!request.ValidateObject())
            {
                try
                {
                    ExceptionHandeling.FireError((int)ErrorCode.General_Error, (int)GeneralError.Nullable_Request, Constants.GeneralErrorDic[GeneralError.Nullable_Request]);
                }
                catch (CodeLabException codelabExp)
                {
                    return ExceptionHandeling.GenerateErrorResponse(codelabExp);
                }
            }
            LoginResponse response = new LoginResponse();
            AccountRep accountRep = new AccountRep(Context);
            int y = ExceptionHandeling.x();
            Account CurrentAccount = accountRep.ValidateCorrectCredentials(request.BasicInfo.MobileNumberInfo.Number, request.Password);
            if (CurrentAccount != null)
            {
                response.LoginStatus = 1;
                response.AdditionalInfo = "Login Success with password " + request.Password + "\n"
                    + "Basic Info: " + request.BasicInfo.ToString();
            }

            else
            {
                //List<CodeLabException> allErrors = new List<CodeLabException>();
                try
                {
                   ExceptionHandeling.FireError((int)ErrorCode.Wrong_Input_Error,(int)WrongInputError.Wrong_Wallet_Number_OR_Password,Constants.WrongInputDic[WrongInputError.Wrong_Wallet_Number_OR_Password]);
                }
                catch (CodeLabException codelabExp)
                {
                    return ExceptionHandeling.GenerateErrorResponse(codelabExp);
                }
            }
            return Ok(response);

        }

        [HttpPost("ValidateOTPLogin")]
        public IActionResult ValidateOTPLogin([FromBody]OTPValidatorRequest request)
        {
            if (!request.ValidateObject())
            {
                try
                {
                    ExceptionHandeling.FireError((int)ErrorCode.General_Error, (int)GeneralError.Nullable_Request, Constants.GeneralErrorDic[GeneralError.Nullable_Request]);
                }
                catch (CodeLabException codelabExp)
                {
                    return ExceptionHandeling.GenerateErrorResponse(codelabExp);
                }
            }
            OTPValidatorResponse response = new OTPValidatorResponse();
            AccountRep accountRep = new AccountRep(Context);
            Account CurrentAccount = accountRep.GetByMSDIN(request.BasicInfo.MobileNumberInfo.Number);
            if (CurrentAccount == null)
            {
                try
                {
                    ExceptionHandeling.FireError((int)ErrorCode.Wrong_Input_Error, (int)WrongInputError.Wrong_Wallet_Number, Constants.WrongInputDic[WrongInputError.Wrong_Wallet_Number]);
                }
                catch (CodeLabException codelabExp)
                {
                    return ExceptionHandeling.GenerateErrorResponse(codelabExp);
                }
            }
            OTPRep oTPRep = new OTPRep(Context);
            bool access = oTPRep.ValidateOTP(request.Otp, CurrentAccount.LastOtpid);
            if (access == true)
            {
                response.Status = 1;
                response.CurrentBalance = (double)CurrentAccount.Balance;
                response.AdditionalInfo = "Login successfully with otp " + request.Otp + "\n"
                    + "Basic Info: " + request.BasicInfo.ToString();
            }
            else
            {
                try
                {
                    ExceptionHandeling.FireError((int)ErrorCode.Wrong_Input_Error, (int)WrongInputError.Wrong_Otp, Constants.WrongInputDic[WrongInputError.Wrong_Otp]);
                }
                catch (CodeLabException codelabExp)
                {
                    return ExceptionHandeling.GenerateErrorResponse(codelabExp);
                }
            }
            return Ok(response);
        }


    }



}