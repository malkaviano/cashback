using System;
using Xunit;
using Domain.Models;
using Moq;

namespace Domain.Tests.Models
{
    public class IdentifiableTests
    {
        [Fact]
        public void Should_Sanitize_Cpf()
        {
            var mock = new Mock<Identifiable>();
            mock.CallBase = true;
            var some = mock.Object;

            some.Cpf = "123.123.123-44";

            Assert.Equal("12312312344", some.Cpf);
        }
    }
}