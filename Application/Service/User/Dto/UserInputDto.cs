using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Created.Dto
{
    public class UserInputDto
    {
        public string mail { get; set; }
        public string password { get; set; }
        public string firsName { get; set; }
        public string lastName { get; set; }
        public string genero { get; set; }
    }
}