using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base.Dto;
using Application.Service.Country.Commands;
using Application.Service.Country.Dto;
using Application.Service.User.Created.Dto;
using Domain.Entity.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Country
{
    public class CountryService
    {
        private readonly ICountryRepository _repository;
        public CountryService(ICountryRepository repository) => _repository = repository;

        public async Task<ResponseEntity<CountryOutputDto>> Create(CountryInputDto dto) => await CreatedCountryCommands.CreateCountry(_repository, dto);
        public async Task<ResponseEntity<CountryOutputDto>> GetAll(PaginationDto dto) => await GetCountriesCommands.GetAll(_repository, dto);
        public async Task<bool> Delete(int id) => await DeleteCountryCommands.DeleteCountry(_repository, id);
        public async Task<ResponseEntity<CountryOutputDto>> GetById(int id) => await GetByIdCountryCommands.GetById(_repository, id);
        public async Task<bool> Update(CountryUpdateDto dto) => await UpdateCountryCommands.UpdateCountry(_repository, dto);
        public async Task<string> GetByIdCountryName(int id) => await GetByIdCountryNameCommands.GetByIdCountryName(_repository, id);


    }
}