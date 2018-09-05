using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests
{
    public class MerchantPaymentRequest : BaseRequest
    {
        [JsonProperty("toWalletNO")]
        public string ToWalletNumber { get; set; }

        [JsonProperty("amount")]
        public long? Amount { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("transactionRef")]
        public string TransactionRef { get; set; }

        public override bool ValidateObject()
        {
            bool ret = false;
            if (TransactionId != null && ToWalletNumber != null && CurrencyCode !=null && TransactionRef != null && Amount != null)
            {
                ret = true;
            }
            return ret & base.ValidateObject();

        }

    }


}
