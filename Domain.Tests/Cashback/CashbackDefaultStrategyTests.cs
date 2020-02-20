using System;
using Xunit;
using Domain.Models;
using Moq;
using Domain.Cashback;

namespace Domain.Tests.Models
{
    public class CashbackDefaultStrategyTests
    {
        [Theory]
        [InlineData("2020-10-05T10:00:00.000", "2020-10-30T20:00:00.000", "2020-10-10T12:00:00.000", 100, 10)]
        [InlineData("2020-01-15T10:00:00.000", "2020-12-30T20:00:00.000", "2020-09-10T12:00:00.000", 1200, 180)]
        [InlineData("2020-01-15T14:00:00.000", "2020-12-05T20:00:00.000", "2020-08-10T08:00:00.000", 2000, 400)]
        [InlineData("2020-01-15T10:00:00.000", "2020-09-30T20:00:00.000", "2020-12-10T12:00:00.000", 1200, 0)]
        public void Should_Apply_Percentage_And_Set_CashbackValue(string startStr, string endStr, string dataStr, decimal value, decimal expected)
        {
            var startDate = DateTime.Parse(startStr);
            var endDate = DateTime.Parse(endStr);

            var strategy = new CashbackDefaultStrategy(startDate, endDate);

            var sales = new Sales { Value = value, Data = DateTime.Parse(dataStr) };

            strategy.Apply(sales);

            Assert.Equal(expected, sales.CashbackValue);
        }

        [Theory]
        [InlineData("2020-10-05T10:00:00.000", "2020-10-30T20:00:00.000", "2020-10-10T12:00:00.000", 100, 10)]
        [InlineData("2020-01-15T10:00:00.000", "2020-12-30T20:00:00.000", "2020-09-10T12:00:00.000", 1200, 15)]
        [InlineData("2020-01-15T14:00:00.000", "2020-12-05T20:00:00.000", "2020-08-10T08:00:00.000", 2000, 20)]
        [InlineData("2020-01-15T10:00:00.000", "2020-09-30T20:00:00.000", "2020-12-10T12:00:00.000", 1200, 0)]
        public void Should_Apply_Percentage_And_Set_CashbackPercentage(string startStr, string endStr, string dataStr, decimal value, decimal expected)
        {
            var startDate = DateTime.Parse(startStr);
            var endDate = DateTime.Parse(endStr);

            var strategy = new CashbackDefaultStrategy(startDate, endDate);

            var sales = new Sales { Value = value, Data = DateTime.Parse(dataStr) };

            strategy.Apply(sales);

            Assert.Equal(expected, sales.CashbackPercentage);
        }
    }
}