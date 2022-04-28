using FluentAssertions;
using PaymentGatewayDemo.Core.Helper;
using System;
using Xunit;

namespace PaymentGatewayDemo.Core.UnitTest
{
    public class MaskingHelperUnitTest
    {

        [Theory]
        [InlineData("1111111111111111", "XXXXXXXXXXXX1111")]
        public void Should_Mask_CreditCard_Correctly(string creditCard, string expected)
        {
            string actual = MaskingHelper.MaskCreditCard(creditCard);
            actual.Should().BeEquivalentTo(expected);

        }
    }
}
