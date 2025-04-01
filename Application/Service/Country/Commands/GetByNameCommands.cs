using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.Country.Dto;
using Domain.Entity.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Country.Commands
{
    public class GetByNameCommands
    {
        public static async Task<bool> Exist(ICountryRepository repository, string name)
        {
            try
            {
                var resp = await repository.GetByName(name);

                if (resp == null)
                {
                    return false;
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