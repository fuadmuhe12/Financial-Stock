using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace api.Dtos;

public  class CreateStockDto{
    [Required]
    [MaxLength(10)]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MinLength(5)]
    [MaxLength(255)]
    public string CompanyName { get; set; }  = string.Empty;
    [Required]
    [MinLength(5)]
    [MaxLength(255)]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1, 1000_000_0000)]
    public long MarketCap { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [Range(1, 1000_000_0000)]
    public decimal Purchase { get; set; }
}