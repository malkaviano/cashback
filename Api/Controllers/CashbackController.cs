using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Gateways;
using Api.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CashbackController : ControllerBase
    {
        private readonly CashbackClientFactory cashbackFactory;

        public CashbackController(CashbackClientFactory cashbackFactory)
        {
            this.cashbackFactory = cashbackFactory;
        }

        // GET api/cashback/cpf
        [HttpGet("{cpf}")]
        public async Task<ActionResult> Get(string cpf)
        {
            try
            {
                var client = cashbackFactory.Create();

                return new OkObjectResult(await client.GetCashback(cpf));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
