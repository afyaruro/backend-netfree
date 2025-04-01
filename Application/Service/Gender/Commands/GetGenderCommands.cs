
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Gender.Dto;
using Application.Service.Gender.Mapping;
using Domain.Entity.Gender;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Gender.Commands
{
    public static class GetCountriesCommands
    {
        public static async Task<ResponseEntity<GenderOutputDto>> GetAll(IGenderRepository repository, PaginationDto dto)
        {
            try
            {

                var resp = await repository.GetAll(dto.pageNumber, dto.pageSize);

                if (resp.listEntity!.Count == 0 || resp == null)
                {
                    throw new EntityNotFoundException("No se encontraron registros");
                }

                var outputResponse = new ResponseEntity<GenderOutputDto>(resp.message, MappingGender.ListEntityToListOutputDto(resp.listEntity));
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