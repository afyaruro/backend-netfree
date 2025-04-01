using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Base.Dto
{
    public class PaginationDto
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}