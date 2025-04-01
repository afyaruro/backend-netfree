using Application.Common.Exceptions;
using Application.Service.User.Created.Dto;
using Application.Service.User.Created.Mapping;
using Application.Service.User.Dto;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.User.Commands
{
    public static class LoginCommands
    {
        public static async Task<ResponseEntity<UserOutputDto>> Login(IUserRepository repository, LoginDto dto)
        {
            try
            {

                var resp = await repository.GetByMail(dto.mail);

                if (resp == null)
                {
                    throw new EntityNotFoundException("El Usuario no existe");
                }

                var helper = new PasswordEncryptionHelper();
                bool isValid = helper.VerifyPassword(resp.password, dto.password, resp.mail);
                if (isValid)
                {
                    var response = new ResponseEntity<UserOutputDto>("Inicio Exitoso", MappingUser.EntityToOutputDto(resp));
                    return response;
                }
                else
                {
                    throw new NotAuthException("Contraseña Incorrecta");
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}