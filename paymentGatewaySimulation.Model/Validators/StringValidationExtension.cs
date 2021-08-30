using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using paymentGatewaySimulation.Model.Requests;

namespace paymentGatewaySimulation.Model.Validators
{
    public static class ValidationExtension
    {
        public static bool IsValidCreditCompanyName(this string str)
        {
            var allowedNames = new List<string>() { Consts.VISA, Consts.MASTERCARD };
            var creditCompanyNameLowerCased = str.Trim().ToLower().ToLower();
            return allowedNames.Contains(creditCompanyNameLowerCased);
        }

        public static bool IsValidCreditCardExpirationDate(this string str)
        {
            var regex = new Regex(Consts.CREDIT_CARD_EXPIRATION_DATE_PATTERN);
            return regex.IsMatch(str);
        }

        public static bool IsValidCvv(this string str)
        {
            var regex = new Regex(Consts.CVV_PATTERN);
            return regex.IsMatch(str);
        }
    }
}
