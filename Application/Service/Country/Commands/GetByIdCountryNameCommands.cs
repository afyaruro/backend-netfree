
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public static class GetByIdCountryNameCommands
    {
        public static async Task<string> GetByIdCountryName(ICountryRepository repository, int idCountry)
        {
            try
            {

                var resp = await repository.GetById(idCountry);

                if (resp == null)
                {
                    return "El pais no existe";
                }

                return resp.name;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}