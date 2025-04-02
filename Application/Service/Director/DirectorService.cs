using Application.Service.Director.Dto;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Service.Director.Commands;
using Application.Base.Dto;

namespace Application.Service.Director
{
    public class DirectorService
    {
        private readonly IDirectorRepository _repository;
        public DirectorService(IDirectorRepository repository) => _repository = repository;

        public async Task<ResponseEntity<DirectorOutputDto>> Create(DirectorInputDto dto, CountryService service) => await CreatedDirectorCommands.CreateDirector(_repository, service, dto);
        public async Task<ResponseEntity<DirectorOutputDto>> GetAll(CountryService service, PaginationDto dto) => await GetDirectorsCommands.GetAll(_repository, service, dto);
        public async Task<bool> Delete(int id) => await DeleteDirectorCommands.DeleteDirector(_repository, id);
        public async Task<ResponseEntity<DirectorOutputDto>> GetById(int id, CountryService service) => await GetByIdDirectorCommands.GetById(_repository, service, id);
        public async Task<bool> Update(DirectorUpdateDto dto, CountryService service) => await UpdateDirectorCommands.UpdateDirector(_repository, service, dto);
        public async Task<ResponseEntity<DirectorOutputDto>> GetByIdDirectorName(int id, CountryService service) => await GetByIdDirectorNameCommands.GetByIdDirectorName(_repository, id, service);

    }
}