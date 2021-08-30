using System;
namespace paymentGatewaySimulation.Model.Requests.CreditCardApiRequests
{
    public class VisaChargeRequest : BaseCreditCardChargeRequest
    {
        public string fullName { get; set; }
        public string number { get; set; }
        public string expiration { get; set; }
        public string cvv { get; set; }
        public string totalAmount { get; set; }
    }
}
