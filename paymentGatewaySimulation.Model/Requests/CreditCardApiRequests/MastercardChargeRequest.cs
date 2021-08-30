using System;
namespace paymentGatewaySimulation.Model.Requests.CreditCardApiRequests
{
    public class MastercardChargeRequest : BaseCreditCardChargeRequest
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string card_number { get; set; }
        public string expiration { get; set; }
        public string cvv { get; set; }
        public string charge_amount { get; set; }
    }
}
