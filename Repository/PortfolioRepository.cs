using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Data.Migrations;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IStockRepository _StockRepo;
        private readonly FinanceContext _context;

        public PortfolioRepository(IStockRepository stockRepo, FinanceContext context)
        {
            _StockRepo = stockRepo;
            _context = context;
        }

        public async Task<Portifolio?> CreatePortifolio(Portifolio portifolio)
        {
            await _context.Portifolios.AddAsync(portifolio);
            await _context.SaveChangesAsync();

            return await _context
                .Portifolios.Include(port => port.Stock)
                .FirstOrDefaultAsync(port =>
                    port.UserId == portifolio.UserId && port.StockId == portifolio.StockId
                );
        }

        public async Task<bool> DeletePortifolio(string UserId, int StockId)
        {
            var val = await _context
                .Portifolios.Where(port => port.StockId == StockId && port.UserId == UserId)
                .ExecuteDeleteAsync();
            return val > 0;
        }

        public async Task<bool> ExistPortifolio(Portifolio port)
        {
            return await _context.Portifolios.AnyAsync(portifolio =>
                portifolio.StockId == port.StockId && portifolio.UserId == port.UserId
            );
        }

        public async Task<List<Stock?>> GetStocks(string userId)
        {
            return await _context
                .Portifolios.Where(port => port.UserId == userId)
                .Include(port => port.Stock)
                .Select(port => port.Stock)
                .ToListAsync();
        }
    }
}
