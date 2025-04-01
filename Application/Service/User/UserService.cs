using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Service.User.Commands;
using Application.Service.User.Created.Commands;
using Application.Service.User.Created.Dto;
using Application.Service.User.Dto;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.User
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) => _repository = repository;

        public async Task<ResponseEntity<UserOutputDto>> Create(UserInputDto dto) => await CreatedUserCommands.CreateUser(_repository, dto);

        public async Task<ResponseEntity<UserOutputDto>> Login(LoginDto dto) => await LoginCommands.Login(_repository, dto);


    }
}