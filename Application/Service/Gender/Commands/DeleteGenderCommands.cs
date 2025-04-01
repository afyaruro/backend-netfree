
using Application.Common.Exceptions;
using Domain.Entity.Gender;
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public static class DeleteGenderCommands
    {
        public static async Task<bool> DeleteGender(IGenderRepository repository, int idGender)
        {
            try
            {

                var resp = await repository.Delete(idGender);

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