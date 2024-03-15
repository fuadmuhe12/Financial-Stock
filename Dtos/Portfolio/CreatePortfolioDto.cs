using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Claims;
using api.Data.Migrations;
using api.Extensions;

namespace api.Dtos.Portfolio;

public class CreatePortfolioDto
{
    public string? UserId { get; set; } = null;
    
    [Required]
    public required int StockId { get; set; }
}
