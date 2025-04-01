
using Domain.Entity.Base;
using Domain.Entity.Country;

namespace Domain.Entity.Director
{
    public class DirectorEntity : BaseEntity<int>
    {
        public string firsName { get; set; }
        public string lastName { get; set; }
        public int idCountry { get; set; }

    }
}