using Microsoft.EntityFrameworkCore;
using PaymentGatewayDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Infrastructure.Data.InMemory
{
    public class PaymentGatewayDemoContext:DbContext
    {
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public PaymentGatewayDemoContext(DbContextOptions options):base (options)
        {
            LoadMerchantDetails();
            LoadPaymentDetails();
        }

        public void LoadPaymentDetails()
        {
            PaymentDetails details = new ()
                { 
                  CreditCard="123456789987654",
                  Amount=1200.0m,
                  Currency="GBP",
                  MercentIdentifier="12345789",
                  PaymentId=Guid.NewGuid().ToString(),
                  CVV ="123",
                  ExpiryMonthYear="12/24" ,
                  PaymentStatus="Success"
                  
            };
            PaymentDetails.Add(details);
        }

        public void LoadMerchantDetails()
        {
            Merchant merchant = new ()
            {
                MerchantId = "123456789",
                MerchantName = "ABC Pvt Ltd.",
                ClientId = "abcdret187543"
            };
            Merchants.Add(merchant);
        }
    }
}
