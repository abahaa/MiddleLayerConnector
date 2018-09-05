using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Controllers;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CodeLab.Barq.BackEndConnector.BackEndMockups.Wrappers
{
    class BEPaymentManager : IBEPaymentManager
    {
        public ConfirmPaymentResponse CheckTransactionStatus(CheckStatusRequest request)
        {
            HttpResponseMessage response = MockupsAPIsCaller.CallAPI(request, "api/Payment/MerchantToSR/CheckTransactionStatus");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string ResponseString = (response.Content.ReadAsStringAsync()).Result;
                ConfirmPaymentResponse confirmPaymentResponse = JsonConvert.DeserializeObject<ConfirmPaymentResponse>(ResponseString);
                return confirmPaymentResponse;
            }
            else if (response.StatusCode == (HttpStatusCode)490)
            {
                throw MockupsAPIsCaller.GenerateError(response);
            }
            throw new Exception();
        }

        public MerchantPaymentResponse EstimateTransactionDetails(MerchantPaymentRequest request)
        {
            HttpResponseMessage response = MockupsAPIsCaller.CallAPI(request, "api/Payment/MerchantToSR/EstimateTransactionDetails");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string ResponseString = (response.Content.ReadAsStringAsync()).Result;
                MerchantPaymentResponse merchantPaymentResponse = JsonConvert.DeserializeObject<MerchantPaymentResponse>(ResponseString);
                return merchantPaymentResponse;
            }
            else if (response.StatusCode == (HttpStatusCode)490)
            {
                throw MockupsAPIsCaller.GenerateError(response);
            }
            throw new Exception();
        }

        public ConfirmPaymentResponse PerformTransaction(ConfirmPaymentRequest request)
        {
            HttpResponseMessage response = MockupsAPIsCaller.CallAPI(request, "api/Payment/MerchantToSR/PerformTransaction");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string ResponseString = (response.Content.ReadAsStringAsync()).Result;
                ConfirmPaymentResponse confirmPaymentResponse = JsonConvert.DeserializeObject<ConfirmPaymentResponse>(ResponseString);
                return confirmPaymentResponse;
            }
            else if (response.StatusCode == (HttpStatusCode)490)
            {
                throw MockupsAPIsCaller.GenerateError(response);
            }
            throw new Exception();
        }
    }
}
