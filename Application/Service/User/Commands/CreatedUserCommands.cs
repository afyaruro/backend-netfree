using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.User.Commands;
using Application.Service.User.Created.Dto;
using Application.Service.User.Created.Mapping;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.User.Created.Commands
{
    public static class CreatedUserCommands
    {
        public static async Task<ResponseEntity<UserOutputDto>> CreateUser(IUserRepository repository, UserInputDto dto)
        {

            try
            {
                var helper = new PasswordEncryptionHelper();
                dto.password = helper.HashPassword(dto.password, dto.mail);

                if (await repository.GetByMail(dto.mail) != null)
                {
                    throw new EntityExistException("El usuario ya esta registrado");
                }

                var resp = repository.Add(MappingUser.InputDtoToEntity(dto));

                if (resp == null)
                {
                    throw new EntityNotFoundException("No se ha creado el usuario");
                }

                return new ResponseEntity<UserOutputDto>("Usuario Creado", MappingUser.EntityToOutputDto(await resp));

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}