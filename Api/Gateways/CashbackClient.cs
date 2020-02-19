using Api.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateways
{
    public class CashbackClient : ICashbackClient
    {
        private readonly HttpClient _httpClient;

        public CashbackClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<CashbackDto> GetCashback(string cpf)
        {
            var request = CreateRequest(cpf);

            var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                        .ConfigureAwait(false);

            using (var contentStream = await result.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<CashbackDto>(
                    contentStream,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
                );
            }
        }

        private static HttpRequestMessage CreateRequest(string cpf)
        {
            return new HttpRequestMessage(HttpMethod.Get, $"/v1/cashback?cpf={cpf}");
        }
    }
}