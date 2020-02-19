using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Domain.Interfaces;
using Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService salesService;
        private readonly IResellerService resellerService;
        private readonly IMapper mapper;

        public SalesController(ISalesService salesService, IResellerService resellerService, IMapper mapper)
        {
            this.salesService = salesService;
            this.resellerService = resellerService;
            this.mapper = mapper;
        }

        // GET api/sales
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var reseller = await resellerService.GetByEmail(User.Identity.Name);
                var result = await salesService.GetByCpf(reseller.Cpf);

                if (result == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }

        }

        // POST api/sales
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesPost dto)
        {
            var sales = mapper.Map<Sales>(dto);

            try
            {
                await salesService.Create(sales);

                return StatusCode(201);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }

        // PUT api/sales
        [HttpPut]
        public async Task<IActionResult> Patch([FromBody] SalesPut dto)
        {
            try
            {
                await salesService.Update(dto.Code, dto.Value, dto.Data);

                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE api/sales/code
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            try
            {
                await salesService.Delete(code);

                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}