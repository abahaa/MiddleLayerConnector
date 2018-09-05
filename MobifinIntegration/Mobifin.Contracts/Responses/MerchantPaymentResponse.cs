using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses
{
    public class Amount
    {
        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("serviceFees")]
        public double ServiceFees { get; set; }

        [JsonProperty("commission")]
        public double Commission { get; set; }
    }

    public class MerchantPaymentResponse
    {
        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

    }

}
