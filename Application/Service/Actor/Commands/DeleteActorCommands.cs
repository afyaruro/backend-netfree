
using Application.Common.Exceptions;
using Domain.Port;

namespace Application.Service.Actor.Commands
{
    public static class DeleteActorCommands
    {
        public static async Task<bool> DeleteActor(IActorRepository repository, int idActor)
        {
            try
            {

                var resp = await repository.Delete(idActor);

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