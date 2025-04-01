using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity.Base;

namespace Domain.Entity.User
{
    public class UserEntity : BaseEntity<int>
    {
        public string mail { get; set; }
        public string password { get; set; }
        public string firsName { get; set; }
        public string lastName { get; set; }
        public string genero { get; set; }


    }
}