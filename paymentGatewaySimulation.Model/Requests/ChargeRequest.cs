using System;
using System.Collections.Generic;
using paymentGatewaySimulation.Model.Requests.CreditCardApiRequests;
using paymentGatewaySimulation.Model.Validators;
namespace paymentGatewaySimulation.Model.Requests
{
    /// <summary>
    /// represents the gateway's schema for credit card charge request. Called by merchants clients.
    /// </summary>
    public class ChargeRequest : BaseCreditCardChargeRequest
    {
        public ChargeRequest()
        {
        }

        public string FullName { get; set; }

        public string CreditCardNumber { get; set; }

        public string CreditCardCompany { get; set; }

        public string ExpirationDate { get; set; }

        public string Cvv { get; set; }

        public decimal Amount { get; set; }

        public bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(FullName) ||
               string.IsNullOrWhiteSpace(CreditCardNumber) ||
               string.IsNullOrWhiteSpace(CreditCardCompany) ||
               string.IsNullOrWhiteSpace(ExpirationDate) ||
               string.IsNullOrWhiteSpace(Cvv) ||
               !CreditCardCompany.IsValidCreditCompanyName() ||
               !ExpirationDate.IsValidCreditCardExpirationDate() ||
               !Amount.IsValidAmountForCharging()
               )
            {
                return false;
            }

            return true;
        }
    }
}
