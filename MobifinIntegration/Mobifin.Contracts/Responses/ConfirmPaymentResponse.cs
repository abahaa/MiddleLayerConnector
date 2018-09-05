using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses
{
    public class ConfirmPaymentResponse
    {
        [JsonProperty("transactionStatus")]
        public int TransactionStatus { get; set; }

        [JsonProperty("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("currentBalance")]
        public double CurrentBalance { get; set; }


        [JsonProperty("completionDateTime")]
        public string CompletionDateTime { get; set; }

        [JsonProperty("totalAmount")]
        public double TotalAmount { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
    }

}
