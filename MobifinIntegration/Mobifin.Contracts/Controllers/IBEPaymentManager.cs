
using  CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Requests;
using CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Responses;

namespace  CodeLab.Barq.BackEndConnector.Mobifin.Contracts.Controllers
{
    public interface IBEPaymentManager
    {
        ConfirmPaymentResponse CheckTransactionStatus(CheckStatusRequest request);
        MerchantPaymentResponse EstimateTransactionDetails( MerchantPaymentRequest request);
        ConfirmPaymentResponse PerformTransaction(ConfirmPaymentRequest request);
    }
}