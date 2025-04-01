using Application.Common.Exceptions;
using Domain.Entity.Movie;
using Domain.Entity.Response;
using Domain.Port;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Movie
{
    public class MovieRepository : IMovieRepository
    {
        private readonly SqlServerContext _context;
        public MovieRepository(SqlServerContext context)
        {
            _context = context;
        }
        public async Task<MovieEntity> Add(MovieEntity entity)
        {
            try
            {
                _context.movie.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la pelicula.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var Movie = await _context.movie.FindAsync(id);
                if (Movie == null) return false;

                _context.movie.Remove(Movie);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la pelicula.", ex);
            }
        }

        public async Task<ResponseEntity<MovieEntity>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                var totalRecords = await _context.movie.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize); // Calcula el total de páginas

                var movies = await _context.movie
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var resp = new ResponseEntity<MovieEntity>("Peliculas Obtenidos", movies);

                resp.totalPages = totalPages;
                resp.totalRecords = totalRecords;

                return resp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de peliculas con paginación.", ex);
            }
        }

        public async Task<MovieEntity?> GetById(int id)
        {
            try
            {
                return await _context.movie.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la pelicula por ID.", ex);
            }
        }


        public async Task<bool> Update(MovieEntity entity)
        {
            try
            {
                var existingEntity = await _context.movie.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    throw new EntityNotFoundException("La pelicula que se intenta actualizar no existe.");
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