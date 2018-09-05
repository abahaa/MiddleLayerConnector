using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Controllers;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.Core
{
    public class PaymentManager
    {
        private IBEPaymentManager Payment;

        public PaymentManager(IBEPaymentManager Payment)
        {
            this.Payment = Payment;
        }

        public MerchantPaymentResponse EstimateTransactionDetails(MerchantPaymentRequest request)
        {
            try
            {
                return Payment.EstimateTransactionDetails(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
