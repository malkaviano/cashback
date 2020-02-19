using System;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateways
{
    public class CashbackClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CashbackClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICashbackClient Create()
        {
            return _serviceProvider.GetRequiredService<CashbackClient>();
        }
    }
}