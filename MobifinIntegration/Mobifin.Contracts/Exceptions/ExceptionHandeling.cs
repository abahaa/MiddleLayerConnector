using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace  CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Exceptions
{
    public class ExceptionHandeling
    {
        public static int x()
        {
            return 2;
        }
        public static void FireError(int ErrorCode,int SubErrorCode , string ExtraInfo = "")
        {
            CodeLabException codelabExp = new CodeLabException
            {
                ErrorCode = ErrorCode,
                SubErrorCode = SubErrorCode,
                ErrorReferenceNumber = "UU-266169856"
            };

            //codelabExp.Data.Add("LoggedInId", 88);
            //codelabExp.Data.Add("NoOfTrials", 3);

            codelabExp.ExtraInfoMessage = "call failed because " + ExtraInfo;
            throw codelabExp;
        }

        public static ObjectResult GenerateErrorResponse(CodeLabException codelabExp)
        {
            List<CodeLabException> allErrors = new List<CodeLabException>();

            allErrors.Add(codelabExp);

            ObjectResult res = new ObjectResult(allErrors);
            res.ContentTypes.Add("application/json");

            var formatterSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            formatterSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            JsonOutputFormatter formatter = new JsonOutputFormatter(formatterSettings, ArrayPool<Char>.Create());

            res.Formatters.Add(formatter);
            res.StatusCode = 490;
            return res;
        }
    }

    [Serializable]
    public class CodeLabException : Exception
    {
        int _ErrorCode = -1;
        string _ErrorReferenceNumber = "";
        int _SubErrorCode = -1;
        string _ExtraInfoMessage = string.Empty;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("errorCode", ErrorCode);
            info.AddValue("errorRefNO", ErrorReferenceNumber);
            info.AddValue("subErrorCode", SubErrorCode);
            info.AddValue("extraInfoMessage", ExtraInfoMessage);
            base.GetObjectData(info, context);
        }
        //Dictionary<string , object> _AssiciatedParameters = new Dictionary<string, object>();

        /// <summary>
        /// error code for the problem.
        /// Example: InvalidEntityDefinition
        /// </summary>
        /// 
        [JsonProperty("errorCode")]
        public int ErrorCode
        {
            get => _ErrorCode;
            set => _ErrorCode = value;
        }

        [JsonProperty("errorRefNO")]
        public string ErrorReferenceNumber
        {
            get => _ErrorReferenceNumber;
            set => _ErrorReferenceNumber = value;
        }


        /// <summary>
        /// a more specific code to the above one. This is optional
        /// Example: InvalidUserNameLength
        /// </summary>
        /// 
        [JsonProperty("subErrorCode")]
        public int SubErrorCode
        {
            get => _SubErrorCode;
            set => _SubErrorCode = value;
        }

        [JsonProperty("extraInfoMessage")]
        public string ExtraInfoMessage { get => _ExtraInfoMessage; set => _ExtraInfoMessage = value; }


        public CodeLabException() : base()
        {

        }

        public CodeLabException(string message) : base(message)
        { }

        public CodeLabException(string message, Exception innerException) : base(message, innerException)
        { }

        protected CodeLabException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
