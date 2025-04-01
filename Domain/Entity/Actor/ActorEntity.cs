using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity.Base;

namespace Domain.Entity.Actor
{
    public class ActorEntity : BaseEntity<int>
    {
        public string firsName { get; set; }
        public string lastName { get; set; }
        public int idCountry { get; set; }

    }
}