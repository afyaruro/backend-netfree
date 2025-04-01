using Application.Common.Exceptions;
using Domain.Entity.MovieActor;
using Domain.Port;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.ActorMovie
{
    public class ActorMovieRepository : IMovieActorRepository
    {
        private readonly SqlServerContext _context;
        public ActorMovieRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<string>> ActorsByMovieId(int movieId)
        {
            var actorNames = await (from ma in _context.movieActor
                                    join a in _context.actor on ma.idActor equals a.Id
                                    where ma.idMovie == movieId
                                    select a.firsName + " " + a.lastName)
                                   .ToListAsync();

            return actorNames;
        }


        public async Task<bool> MovieActorExists(int movieId, int actorId)
        {
            return await _context.movieActor.AnyAsync(ma => ma.idMovie == movieId && ma.idActor == actorId);
        }


        public async Task<MovieActorEntity> Add(MovieActorEntity entity)
        {
            try
            {
                _context.movieActor.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el actor a la pelicula.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var MovieActor = await _context.movieActor.FindAsync(id);
                if (MovieActor == null) return false;

                _context.movieActor.Remove(MovieActor);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el actor de la pelicula.", ex);
            }
        }


    }
}