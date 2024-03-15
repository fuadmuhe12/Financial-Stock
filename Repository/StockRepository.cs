using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mapping;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository(FinanceContext context) : IStockRepository
{
    private readonly FinanceContext _context = context;

    public async Task AddStockAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStockByIdAsync(int id)
    {
        await _context.Stocks.Where(curStock => curStock.Id == id).ExecuteDeleteAsync();
    }

    public async Task<List<Stock>> GetStockAsync(QueryObject query)
    {
        var stocks = _context.Stocks.Include(stock => stock.Comments).ThenInclude(comment => comment.AppUser).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase )){
                stocks  =  query.IsDecending ? stocks.OrderByDescending(stocks => stocks.Symbol) : stocks.OrderBy(stocks => stocks.Symbol);
            }
            else if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase)){
                stocks  =  query.IsDecending ? stocks.OrderByDescending(stocks => stocks.CompanyName) : stocks.OrderBy(stocks => stocks.CompanyName);
            }
            else if (query.SortBy.Equals("Purchase", StringComparison.OrdinalIgnoreCase)){
                stocks  =  query.IsDecending ? stocks.OrderByDescending(stocks => stocks.Purchase) : stocks.OrderBy(stocks => stocks.Purchase);
            }
            else if (query.SortBy.Equals("LastDiv", StringComparison.OrdinalIgnoreCase)){
                stocks  =  query.IsDecending ? stocks.OrderByDescending(stocks => stocks.LastDiv) : stocks.OrderBy(stocks => stocks.LastDiv);
            }
            else if (query.SortBy.Equals("Industry", StringComparison.OrdinalIgnoreCase)){
                stocks  =  query.IsDecending ? stocks.OrderByDescending(stocks => stocks.Industry) : stocks.OrderBy(stocks => stocks.Industry);
            }
            else if (query.SortBy.Equals("MarketCap", StringComparison.OrdinalIgnoreCase)){
                stocks  =  query.IsDecending ? stocks.OrderByDescending(stocks => stocks.MarketCap) : stocks.OrderBy(stocks => stocks.MarketCap);
            }
        }

        int Skip = (query.PageNumber-1)*query.PageSize;
        stocks = stocks.Skip(Skip).Take(query.PageSize);
        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetStockByIdAsync(int id)
    {
        return await _context
            .Stocks.Include(stock => stock.Comments).ThenInclude(comment => comment.AppUser)
            .FirstOrDefaultAsync(stock => stock.Id == id);
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> StockExist(int id)
    {
        return await _context.Stocks.AnyAsync(stock => stock.Id == id);
    }

    public async Task<Stock?> UpdateStock(int id, UpdateDto stockDto)
    {
        var ExistingStock = await GetStockByIdAsync(id);
        if (ExistingStock is null)
        {
            return null;
        }
        else
        {
            var NewStock = stockDto.ToStockWithId(id);
            _context.Entry(ExistingStock).CurrentValues.SetValues(NewStock);
            await _context.SaveChangesAsync();
            return NewStock;
        }
    }
}
