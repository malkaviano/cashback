using System;
using Xunit;
using Domain.Models;
using Moq;
using Domain.Services;
using Domain.Interfaces;
using Domain.Values;
using System.Threading.Tasks;

namespace Domain.Tests.Services
{
    public class SalesServicesTests
    {
        [Theory]
        [InlineData("15350946056", SalesStatus.APPROVED)]
        [InlineData("153.509.460-56", SalesStatus.APPROVED)]
        [InlineData("11122233344", SalesStatus.VALIDATING)]
        [InlineData("111.222.333-44", SalesStatus.VALIDATING)]
        public async void Should_Create_Sales_With_Correct_Status(string cpf, string status)
        {
            var mockSalesRepo = new Mock<ISalesRepository>();
            var mockStrategy = new Mock<ICashbackStrategy>();

            var service = new SalesService(mockSalesRepo.Object, mockStrategy.Object);

            var sales = new Sales
            {
                Cpf = cpf
            };

            await service.Create(sales);

            Assert.Equal(status, sales.Status);
        }

        [Fact]
        public async void Should_Update_Validating_Status()
        {
            var mockSalesRepo = new Mock<ISalesRepository>();
            var mockStrategy = new Mock<ICashbackStrategy>();

            mockSalesRepo.Setup(r => r.FindByCode(It.IsAny<string>())).ReturnsAsync(new Sales { Status = SalesStatus.VALIDATING });

            var service = new SalesService(mockSalesRepo.Object, mockStrategy.Object);

            await service.Update("xpto", 100, DateTime.Now);
        }

        [Fact]
        public async void Should_Dont_Update_Sales_NotFound()
        {
            var mockSalesRepo = new Mock<ISalesRepository>();
            var mockStrategy = new Mock<ICashbackStrategy>();

            mockSalesRepo.Setup(r => r.FindByCode(It.IsAny<string>())).ReturnsAsync(default(Sales));

            var service = new SalesService(mockSalesRepo.Object, mockStrategy.Object);

            await Assert.ThrowsAsync<Exception>(async () => { await service.Update("xpto", 100, DateTime.Now); });
        }

        [Fact]
        public async void Should_Dont_Update_Sales_Wrong_Status()
        {
            var mockSalesRepo = new Mock<ISalesRepository>();
            var mockStrategy = new Mock<ICashbackStrategy>();

            mockSalesRepo.Setup(r => r.FindByCode(It.IsAny<string>())).ReturnsAsync(new Sales { Status = SalesStatus.APPROVED });

            var service = new SalesService(mockSalesRepo.Object, mockStrategy.Object);

            await Assert.ThrowsAsync<Exception>(async () => { await service.Update("xpto", 100, DateTime.Now); });
        }
    }
}