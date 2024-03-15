using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateDto
    {
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Industry { get; set; } = string.Empty;
        [Required]
        public long MarketCap { get; set; }
        [Required]
        [Range(1, 1000_000_000)]
        public decimal LastDiv { get; set; }
        [Required]
        public decimal Purchase { get; set; }
    }
}
