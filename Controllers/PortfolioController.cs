using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data.Migrations;
using api.Dtos.Portfolio;
using api.Extensions;
using api.Interfaces;
using api.Mapping;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;

        public PortfolioController(
            IPortfolioRepository portfolioRepo,
            UserManager<AppUser> userManager,
            IStockRepository stockRepo
        )
        {
            _portfolioRepo = portfolioRepo;
            _userManager = userManager;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortolios()
        {
            var userId = User.GetUserId();
            if (userId != null)
            {
                var stocks = await _portfolioRepo.GetStocks(userId);
                return Ok(stocks);
            }
            return NotFound("User not Found");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePortfolioDto portfolioDto)
        {
            portfolioDto.UserId ??= User.GetUserId();

            if (!await _stockRepo.StockExist(portfolioDto.StockId)){
                return BadRequest("Stock not Found");
            }

            if (await _userManager.FindByIdAsync(portfolioDto.UserId!) is null)
            {
                return BadRequest("User not Found");
            }

            var port = portfolioDto.ToPortifolio();
            if (await _portfolioRepo.ExistPortifolio(port)){
                return BadRequest("Portifolio Already Exist");
            }

            await _portfolioRepo.CreatePortifolio(port);
            return Created();
        }

        [HttpDelete]
        [Route("{StockId}")]
        [Authorize]

        public async Task<IActionResult> DeletePortifolio([FromRoute] int StockId){
            var userId = User.GetUserId()!;
            await _portfolioRepo.DeletePortifolio(userId, StockId);

            return Ok();


        }

    }
}
