using System.Runtime.CompilerServices;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<Stock?> GetStockByIdAsync(int id);
    Task<List<Stock>> GetStockAsync(QueryObject query);
    Task SaveChangeAsync();

    Task AddStockAsync(Stock stock);

    Task<Stock?> UpdateStock(int id, UpdateDto stockDto);

    Task DeleteStockByIdAsync(int id);

    Task<bool> StockExist(int id);
}
