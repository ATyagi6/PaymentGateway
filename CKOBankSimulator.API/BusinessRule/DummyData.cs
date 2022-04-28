using CKOBankSimulator.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CKOBankSimulator.API.BusinessRule
{
    public static class DummyData
    {
        private static readonly List<MerchantCardDetails> _merchant;
        static DummyData()
        {
            _merchant = LoadMerchantDetails();
        }

        private static List<MerchantCardDetails> LoadMerchantDetails()
        {
            List<MerchantCardDetails> merchantObjects = new ()
            {
                    new () { CreditCard = "123456789987654", CreditLimit = 100000.0m,CVV = "123", ExpiryMonthYear = "12/24",MercentId = "123456789" }

            };

            return merchantObjects;
        }

        public static MerchantCardDetails MerchantCardDetails(string merchantId)
        {
            if (merchantId is null)
            {
                return null;
            }
            else
            {
                var merchnantDetail = GetMerchantDetails(merchantId);
                return merchnantDetail;
            }
        }

        private static MerchantCardDetails GetMerchantDetails(string merchantId)
        {
            return _merchant.Where(x => x.MercentId == merchantId).FirstOrDefault();
        }
    }
}
