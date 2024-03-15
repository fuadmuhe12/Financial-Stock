using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Portifolio
    {
        public required string UserId { get; set; }
        public int StockId { get; set; }
        public Stock? Stock { get; set; }
        public AppUser? user { get; set; }
    }
}
