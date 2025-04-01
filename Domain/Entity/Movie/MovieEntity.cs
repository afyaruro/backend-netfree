using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity.Actor;
using Domain.Entity.Base;

namespace Domain.Entity.Movie
{
    public class MovieEntity : BaseEntity<int>
    {
        public string title { get; set; }
        public string review { get; set; }
        public string coverImage { get; set; }
        public string codeTrailer { get; set; }

        public int idGenero { get; set; }
        public int idCountry { get; set; }
        public int idDirector { get; set; }

    }
}