
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests
{
    [Serializable]
    public class OTPValidatorRequest : BaseRequest
    {

        [JsonProperty("Otp")]
        public string Otp { get; set; }

        public override bool ValidateObject()
        {
            bool ret = false;
            if (Otp != null)
            {
                ret = true;
            }
            return ret & base.ValidateObject();

        }
    }

}
