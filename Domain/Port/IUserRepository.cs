
using Domain.Entity.User;

namespace Domain.Port
{
    public interface IUserRepository
    {

        Task<UserEntity?> GetByMail(string mail);
        Task<UserEntity?> GetById(int id);
        Task<UserEntity> Add(UserEntity entity);


    }
}