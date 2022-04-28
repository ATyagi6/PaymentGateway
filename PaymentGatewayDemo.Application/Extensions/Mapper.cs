using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Application.DTOs;
using PaymentGatewayDemo.Core.Entities;
using PaymentGatewayDemo.Core.Exceptions;
using PaymentGatewayDemo.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Application.Extensions
{
    public static class Mapper
    {
        public static PaymentDetailsDTO PaymentDetailsEntityToDTO(this PaymentDetails payment)
        {
            if (payment is null)
            {
                return null;
            }
            return new PaymentDetailsDTO
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                CreditCard = MaskingHelper.MaskCreditCard(payment.CreditCard),
                Currency = payment.Currency,
                CVV = payment.CVV,
                ExpiryMonthYear = payment.ExpiryMonthYear,
                PaymentDate = System.DateTime.Now,
                PaymentStatus = payment.PaymentStatus

            };
        }

        public static PaymentDetails PaymenDetailsCommandToEntity(this CreatePaymentCommand payment)
        {
            if (payment is null)
            {
                return null;
            }
            return new PaymentDetails
            {
                PaymentId = Guid.NewGuid().ToString(),
                Amount = payment.Amount,
                CreditCard = payment.CreditCard,
                Currency = payment.Currency,
                CVV = payment.CVV,
                ExpiryMonthYear = payment.ExpiryMonthYear,
                MercentIdentifier = payment.MerchantID


            };
        }
    }
}