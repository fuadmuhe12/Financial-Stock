using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Stock;
using api.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Mapping
{
    public static class StockMapping
    {
        public static Stock ToEntityFromCreateStockDto(this CreateStockDto stockDto)
        {
            return new Stock
            {
                CompanyName = stockDto.CompanyName,
                Symbol = stockDto.Symbol,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                LastDiv = stockDto.LastDiv,
                Purchase = stockDto.Purchase
            };
        }

        public static Stock ToStock(this UpdateDto stockDto)
        {
            return new Stock
            {
                CompanyName = stockDto.CompanyName,
                Symbol = stockDto.Symbol,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                LastDiv = stockDto.LastDiv,
                Purchase = stockDto.Purchase
            };
        }

        public static Stock ToStockWithId(this UpdateDto stockDto, int id)
        {
            return new Stock
            {
                Id = id,
                CompanyName = stockDto.CompanyName,
                Symbol = stockDto.Symbol,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                LastDiv = stockDto.LastDiv,
                Purchase = stockDto.Purchase
            };
        }

        public static Stock ToEntityWithId(this CreateStockDto stockDto, int id)
        {
            return new Stock
            {
                Id = id,
                CompanyName = stockDto.CompanyName,
                Symbol = stockDto.Symbol,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                LastDiv = stockDto.LastDiv,
                Purchase = stockDto.Purchase
            };
        }

        public static CreateStockDto ToDto(this Stock stock)
        {
            return new CreateStockDto
            {
                CompanyName = stock.CompanyName,
                Symbol = stock.Symbol,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                LastDiv = stock.LastDiv,
                Purchase = stock.Purchase
            };
        }

        public static ShowUserStockDto ToShowUserStockDto(this Stock stock)
        {
            return new ShowUserStockDto
            {
                Id = stock.Id,
                CompanyName = stock.CompanyName!,
                Symbol = stock.Symbol,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                LastDiv = stock.LastDiv,
                Purchase = stock.Purchase,
                Comments = stock.Comments.Select(comment => comment.ToViewDto()).ToList()
            };
        }
    }
}
