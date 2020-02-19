using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ResellerController : ControllerBase
    {
        private readonly IResellerService service;
        private readonly IMapper mapper;

        public ResellerController(IResellerService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/reseller/cpf
        [HttpGet("{cpf}")]
        public async Task<IActionResult> Get(String cpf)
        {
            try
            {
                var result = await service.GetByCpf(cpf);

                if (result == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(result);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        // POST api/reseller
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ResellerDto dto)
        {
            var reseller = mapper.Map<Reseller>(dto);

            try
            {
                await service.Create(reseller);

                return Created(new Uri($"/api/resseler/#{reseller.Cpf}", UriKind.Relative), reseller);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }
    }
}