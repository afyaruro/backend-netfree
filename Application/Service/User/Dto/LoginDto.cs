using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Dto
{
    public class LoginDto
    {
        public string mail { get; set; }
        public string password { get; set; }
    }
}