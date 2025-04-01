using Application.Common.Exceptions;
using Domain.Entity.Director;
using Domain.Entity.Response;
using Domain.Port;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Director
{
    public class DirectorRepository : IDirectorRepository
    {

        private readonly SqlServerContext _context;
        public DirectorRepository(SqlServerContext context)
        {
            _context = context;
        }
        public async Task<DirectorEntity> Add(DirectorEntity entity)
        {
            try
            {
                _context.director.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el director.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var Director = await _context.director.FindAsync(id);
                if (Director == null) return false;

                _context.director.Remove(Director);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el director.", ex);
            }
        }

        public async Task<ResponseEntity<DirectorEntity>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                var totalRecords = await _context.director.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var directors = await _context.director
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var resp = new ResponseEntity<DirectorEntity>("Directores Obtenidos", directors);

                resp.totalPages = totalPages;
                resp.totalRecords = totalRecords;

                return resp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de directores con paginación.", ex);
            }
        }

        public async Task<DirectorEntity?> GetById(int id)
        {
            try
            {
                return await _context.director.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el director por ID.", ex);
            }
        }


        public async Task<bool> Update(DirectorEntity entity)
        {
            try
            {
                var existingEntity = await _context.director.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    throw new EntityNotFoundException("El director que se intenta actualizar no existe.");
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                return await _context.SaveChangesAsync() > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}