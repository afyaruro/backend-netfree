
using Application.Base.Dto;
using Application.Service.Gender.Commands;
using Application.Service.Gender.Dto;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Gender
{
    public class GenderService
    {
        private readonly IGenderRepository _repository;
        public GenderService(IGenderRepository repository) => _repository = repository;

        public async Task<ResponseEntity<GenderOutputDto>> Create(GenderInputDto dto) => await CreatedGenderCommands.CreateGender(_repository, dto);
        public async Task<ResponseEntity<GenderOutputDto>> GetAll(PaginationDto dto) => await GetCountriesCommands.GetAll(_repository, dto);
        public async Task<bool> Delete(int id) => await DeleteGenderCommands.DeleteGender(_repository, id);
        public async Task<ResponseEntity<GenderOutputDto>> GetById(int id) => await GetByIdGenderCommands.GetById(_repository, id);
        public async Task<bool> Update(GenderUpdateDto dto) => await UpdateGenderCommands.UpdateGender(_repository, dto);
        public async Task<ResponseEntity<GenderOutputDto>> GetByIdGenderName(int id) => await GetByIdGenderNameCommands.GetByIdGenderName(_repository, id);

    }
}