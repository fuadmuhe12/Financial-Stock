using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Auth
{
    public class RegisterResponceDto
    {
        public string? Email { get; set; }
        public string? UserName{ get; set; }
        public string? Token { get; set; }
    }
}