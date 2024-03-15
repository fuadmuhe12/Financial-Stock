using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Data;
using api.Dtos.Auth;
using api.Interfaces;
using api.Mapping;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace api.Repository
{
    public class AuthRepository 
    {
        private readonly FinanceContext _context;
        private readonly IConfiguration config;

        public AuthRepository(FinanceContext context, IConfiguration configuration)
        {
            _context = context;
            config = configuration;
        }
/* 
        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user =>
                user.UserName == loginDto.UserName && user.Password == loginDto.Password
            );

            if (user is null)
            {
                return null;
            }

            var token = GenerateToken(user);
            return token;
        } */

        private string GenerateToken(AppUser user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role , user.Roles)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    config["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key")
                )
            );

            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            SecurityToken token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

    }
}
