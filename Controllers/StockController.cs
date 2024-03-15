using api.Dtos;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mapping;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]

public class StockController(IStockRepository stockRepo) : Controller
{
    private readonly IStockRepository _stockRepo = stockRepo;
    const string GetStockEndpoint = "stock";

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        var Stocks = await _stockRepo.GetStockAsync(query);

        var showStocks = Stocks.Select(stock => stock.ToShowUserStockDto());

        return Ok(showStocks);
    }

    [HttpGet("{id:int}", Name = GetStockEndpoint)]
    [Authorize]
    public async Task<IActionResult> GetByID(int id)
    {
        var stock = await _stockRepo.GetStockByIdAsync(id);
        if (stock is null)
        {
            return NotFound();
        }

        return Ok(stock.ToShowUserStockDto());
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
    {
        Stock stock = stockDto.ToEntityFromCreateStockDto();

        await _stockRepo.AddStockAsync(stock);

        return CreatedAtRoute(GetStockEndpoint, new { id = stock.Id }, stock.ToShowUserStockDto());
    }

    [HttpPut()]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDto stockDto)
    {
        var result = await _stockRepo.UpdateStock(id, stockDto);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result.ToShowUserStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        /* Stock? stock =  _context.Stocks.FirstOrDefault(x => x.Id == id);
        if (stock is null) return NotFound();

        _context.Stocks.Remove(stock); */

        await _stockRepo.DeleteStockByIdAsync(id);
        return NoContent();
    }
}
