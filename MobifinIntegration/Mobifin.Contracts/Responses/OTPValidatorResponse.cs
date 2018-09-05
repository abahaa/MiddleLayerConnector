using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses
{
    public class OTPValidatorResponse
    {

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonProperty("currentBalance")]
        public double CurrentBalance { get; set; }
    }
}
