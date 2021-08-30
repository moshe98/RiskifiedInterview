using System;
namespace paymentGatewaySimulation.Model.Validators
{
    public static class DecimalValidationExtension
    {
        public static bool IsValidAmountForCharging(this decimal amount)
        {
            return amount > 0;
        }
    }
}
