using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Auth;
using api.Models;

namespace api.Mapping
{
    public static class AuthMapping
    {
        public static AppUser ToAppUser(this RegisterDto registerDto){
            return new AppUser{
                UserName = registerDto.UserName,
                Name = registerDto.Name,
                Email = registerDto.Email
            };
        }
        public static AppUser ToAppUserWithId(this RegisterDto registerDto){
            return new AppUser{
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Name = registerDto.Name,
            };
        }


        public static LoginDto ToLoginDto(this AppUser user){
            return new LoginDto{
                Password = user.Password,
                UserName = user.UserName!
            };

            //TODO:  Fix , password is encrpted while it is in AppUser mode
        }
    }
}