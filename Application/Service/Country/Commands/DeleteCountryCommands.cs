using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Entity.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public static class DeleteCountryCommands
    {
        public static async Task<bool> DeleteCountry(ICountryRepository repository, int idCountry)
        {
            try
            {

                var resp = await repository.Delete(idCountry);

                if (!resp)
                {
                    throw new EntityNotDelete("No se encontro el registro");
                }

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}