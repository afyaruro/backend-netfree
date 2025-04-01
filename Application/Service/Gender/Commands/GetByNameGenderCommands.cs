using Domain.Entity.Gender;
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public class GetByNameGenderCommands
    {
        public static async Task<bool> Exist(IGenderRepository repository, string name)
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