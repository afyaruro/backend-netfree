using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity.Base;

namespace Domain.Entity.MovieActor
{
    public class MovieActorEntity : BaseEntity<int>
    {
        public int idMovie { get; set; }
        public int idActor { get; set; }

    }
}