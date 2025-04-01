using Application.Common.Exceptions;
using Domain.Entity.Actor;
using Domain.Entity.Response;
using Domain.Port;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Actor
{
    public class ActorRepository : IActorRepository
    {

        private readonly SqlServerContext _context;
        public ActorRepository(SqlServerContext context)
        {
            _context = context;
        }
        public async Task<ActorEntity> Add(ActorEntity entity)
        {
            try
            {
                _context.actor.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el actor.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var actor = await _context.actor.FindAsync(id);
                if (actor == null) return false;

                _context.actor.Remove(actor);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el actor.", ex);
            }
        }


        public async Task<ResponseEntity<ActorEntity>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                var totalRecords = await _context.actor.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var actors = await _context.actor
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var resp = new ResponseEntity<ActorEntity>("Actores Obtenidos", actors);

                resp.totalPages = totalPages;
                resp.totalRecords = totalRecords;

                return resp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de actores con paginación.", ex);
            }
        }

        public async Task<ActorEntity?> GetById(int id)
        {
            try
            {
                return await _context.actor.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el actor por ID.", ex);
            }
        }


        public async Task<bool> Update(ActorEntity entity)
        {
            try
            {
                var existingEntity = await _context.actor.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    throw new EntityNotFoundException("El actor que se intenta actualizar no existe.");
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