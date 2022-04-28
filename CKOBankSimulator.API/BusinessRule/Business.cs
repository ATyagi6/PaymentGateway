using CKOBankSimulator.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CKOBankSimulator.API.BusinessRule
{
    public class Business : IBusiness
    {
        
        public bool CheckBankDetails(BankDetails bankDetails)
        {
            if (bankDetails is null)
            {
                return false;
            }

            var details = DummyData.MerchantCardDetails(bankDetails.MercentIdentifier);
            if (details is null)
            {
                return false;
            }
            return ValidateMerchantDetails(details, bankDetails);


        }

        private static bool  ValidateMerchantDetails(MerchantCardDetails details, BankDetails bankDetails)
        {
            if (details.CreditCard == bankDetails.CreditCard &&
                details.CVV == bankDetails.CVV &&
                details.ExpiryMonthYear == bankDetails.ExpiryMonthYear &&
                details.CreditLimit >= bankDetails.Amount)
            {
                return true;
            }
            return false;
        }
    }
}
