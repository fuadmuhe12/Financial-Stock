using api.Dtos.Portfolio;
using api.Models;

namespace api.Mapping;

public static class PortfolioMapping
{
    public static Portifolio ToPortifolio(this CreatePortfolioDto portfolioDto)
    {
        return new Portifolio { UserId = portfolioDto.UserId!, StockId = portfolioDto.StockId };
    }
}
