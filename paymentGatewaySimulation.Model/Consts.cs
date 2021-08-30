using System;
namespace paymentGatewaySimulation.Model
{
    public static class Consts
    {
        public const string VISA = "visa";
        public const string MASTERCARD = "mastercard";
        public const string CREDIT_CARD_EXPIRATION_DATE_PATTERN = @"^(0[1-9]|1[0-2])\/?([0-9]{2})$";
        public const string CVV_PATTERN = @"^[0-9]{3,4}$";
        public const string MERCHANT_IDENTIFIER_HEADER_KEY = "merchant-identifier";
        public const string APP_SETTINGS_MASTERCARD_CHARGE_URL_ENTRY = "https://interview.riskxint.com/";
        public const string APP_SETTINGS_VISA_CHARGE_URL_ENTRY = "https://interview.riskxint.com/";

        public const string CHARGE_CARD_DECLINED = "Card declined";
    }
}
