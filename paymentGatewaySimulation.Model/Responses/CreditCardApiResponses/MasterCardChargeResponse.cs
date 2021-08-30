using System;
namespace paymentGatewaySimulation.Model.Responses.CreditCardApiResponses
{
    public class MasterCardChargeResponse : BaseCreditCardChargeResponse
    {
        public string decline_reason { get; set; }
    }
}
