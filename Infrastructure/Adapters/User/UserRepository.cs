using Domain.Entity.User;
using Domain.Port;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters
{
    public class UserRepository : IUserRepository
    {

        private readonly SqlServerContext _context;
        public UserRepository(SqlServerContext context)
        {
            _context = context;
        }
        public async Task<UserEntity> Add(UserEntity entity)
        {
            try
            {
                _context.users.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario.", ex);
            }
        }

        public async Task<UserEntity?> GetById(int id)
        {
            try
            {
                return await _context.users.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por ID.", ex);
            }
        }

        public async Task<UserEntity?> GetByMail(string mail)
        {
            try
            {
                return await _context.users.FirstOrDefaultAsync(u => u.mail == mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por nombre de usuario.", ex);
            }
        }

    }
}