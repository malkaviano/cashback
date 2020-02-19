using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Domain.Interfaces;
using Domain.Models;
using AutoMapper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService service;
        private readonly IMapper mapper;

        public SalesController(ISalesService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/sales
        [HttpGet]
        public async Task<IActionResult> Get(String cpf)
        {
            try
            {
                var result = await service.Get();

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
                await service.Create(sales);

                return Created(new Uri($"/api/sales/#{sales.Cpf}", UriKind.Relative), sales);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }

        // PUT api/sales/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] SalesPut dto)
        {
            var sales = mapper.Map<Sales>(dto);

            sales.Id = id;

            try
            {
                await service.Update(sales);

                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE api/sales/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.Delete(id);

                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}