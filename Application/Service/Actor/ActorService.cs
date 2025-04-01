
using Application.Base.Dto;
using Application.Service.Actor.Commands;
using Application.Service.Actor.Dto;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Actor
{
    public class ActorService
    {
        private readonly IActorRepository _repository;
        public ActorService(IActorRepository repository) => _repository = repository;

        public async Task<ResponseEntity<ActorOutputDto>> Create(ActorInputDto dto, CountryService service) => await CreatedActorCommands.CreateActor(_repository, service, dto);
        public async Task<ResponseEntity<ActorOutputDto>> GetAll(CountryService service, PaginationDto dto) => await GetActorsCommands.GetAll(_repository, service, dto);
        public async Task<bool> Delete(int id) => await DeleteActorCommands.DeleteActor(_repository, id);
        public async Task<ResponseEntity<ActorOutputDto>> GetById(int id, CountryService service) => await GetByIdActorCommands.GetById(_repository, service, id);
        public async Task<bool> Update(ActorUpdateDto dto, CountryService service) => await UpdateActorCommands.UpdateActor(_repository, service, dto);


    }
}