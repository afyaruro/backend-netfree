
using Application.Common.Exceptions;
using Application.Service.Director.Dto;
using Application.Service.Director.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Base.Dto;

namespace Application.Service.Director.Commands
{
    public static class GetDirectorsCommands
    {
        public static async Task<ResponseEntity<DirectorOutputDto>> GetAll(IDirectorRepository repository, CountryService service, PaginationDto dto)
        {
            try
            {

                var resp = await repository.GetAll(dto.pageNumber, dto.pageSize);

                if (resp.listEntity!.Count == 0 || resp == null)
                {
                    throw new EntityNotFoundException("No se encontraron registros");
                }

                var outputResponse = new ResponseEntity<DirectorOutputDto>(resp.message, await MappingDirector.ListEntityToListOutputDto(resp.listEntity, service));
                outputResponse.totalPages = resp.totalPages;
                outputResponse.totalRecords = resp.totalRecords;

                return outputResponse;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}