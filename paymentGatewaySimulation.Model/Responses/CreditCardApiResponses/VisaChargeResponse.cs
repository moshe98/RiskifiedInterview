using System;
namespace paymentGatewaySimulation.Model.Responses.CreditCardApiResponses
{
    public class VisaChargeResponse : BaseCreditCardChargeResponse
    {
        public string chargeResult { get; set; }
        public string resultReason { get; set; }
    }
}
