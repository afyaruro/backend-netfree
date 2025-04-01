using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Actor.Dto;
using Application.Service.Actor.Mapping;
using Application.Service.Country;
using Domain.Entity.Actor;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Actor.Commands
{
    public static class GetActorsCommands
    {
        public static async Task<ResponseEntity<ActorOutputDto>> GetAll(IActorRepository repository, CountryService service, PaginationDto dto)
        {
            try
            {

                var resp = await repository.GetAll(dto.pageNumber, dto.pageSize);

                if (resp.listEntity!.Count == 0 || resp == null)
                {
                    throw new EntityNotFoundException("No se encontraron registros");
                }

                var outputResponse = new ResponseEntity<ActorOutputDto>(resp.message, await MappingActor.ListEntityToListOutputDto(resp.listEntity, service));
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