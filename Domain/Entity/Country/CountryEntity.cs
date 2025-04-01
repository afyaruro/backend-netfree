using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity.Base;

namespace Domain.Entity.Country
{
    public class CountryEntity : BaseEntity<int>
    {
        public string name { get; set; }

    }
}