using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Domain.Interfaces;
using Domain.Models;
using AutoMapper;
using Api.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResellerController : ControllerBase
    {
        private readonly IResellerService resellerService;
        private readonly AuthService authService;
        private readonly IMapper mapper;

        public ResellerController(IResellerService service, AuthService authService, IMapper mapper)
        {
            this.resellerService = service;
            this.authService = authService;
            this.mapper = mapper;
        }

        // GET api/reseller/cpf
        [HttpGet("{cpf}")]
        public async Task<IActionResult> Get(String cpf)
        {
            try
            {
                var result = await resellerService.GetByCpf(cpf);

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
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] ResellerPost dto)
        {
            var reseller = mapper.Map<Reseller>(dto);

            try
            {
                if (await authService.CreateUser(dto.Email, dto.Password))
                {
                    await resellerService.Create(reseller);

                    return Created(new Uri($"/api/resseler/#{reseller.Cpf}", UriKind.Relative), reseller);
                }
                else
                {
                    return new BadRequestObjectResult("Não foi possivel criar login");
                }

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }
    }
}