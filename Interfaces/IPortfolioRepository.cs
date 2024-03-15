using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        public Task<List<Stock?>> GetStocks(string userId) ;

        public Task<Portifolio?> CreatePortifolio(Portifolio portifolio);
        public Task<bool> ExistPortifolio(Portifolio portifolio);
        public Task<bool> DeletePortifolio(string UserId, int StockId);
    }
}