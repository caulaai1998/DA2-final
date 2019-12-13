using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Api.Models
{
    public class ResponseUser
    {
        public string Ten { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public ResponseUser(AppUser u,string r, string token)
        {
            Ten = u.FullName;
            Role = r;
            Token = token;
        }
    }
}
