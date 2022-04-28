using System;

namespace PaymentGatewayDemo.Core.Helper
{
   public static class MaskingHelper
    {
        public static string MaskCreditCard(string  creditCard)
        {
            string XXCreditCard = "XXXXXXXXXXXX";
            return String.Format("{0}{1}", XXCreditCard, creditCard.Substring(creditCard.Length - 4, 4));

        }
    }
}
