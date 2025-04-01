using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Entity.Country;
using Domain.Entity.Response;
using Domain.Port;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Country
{
    public class CountryRepository : ICountryRepository
    {

        private readonly SqlServerContext _context;
        public CountryRepository(SqlServerContext context)
        {
            _context = context;
        }
        public async Task<CountryEntity> Add(CountryEntity entity)
        {
            try
            {
                _context.country.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el country.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var country = await _context.country.FindAsync(id);
                if (country == null) return false;

                _context.country.Remove(country);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el country.", ex);
            }
        }

        public async Task<ResponseEntity<CountryEntity>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                var totalRecords = await _context.country.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize); // Calcula el total de páginas

                var countries = await _context.country
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var resp = new ResponseEntity<CountryEntity>("Paises Obtenidos", countries);

                resp.totalPages = totalPages;
                resp.totalRecords = totalRecords;

                return resp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de países con paginación.", ex);
            }
        }

        public Task<List<CountryEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CountryEntity?> GetById(int id)
        {
            try
            {
                return await _context.country.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el country por ID.", ex);
            }
        }

        public async Task<CountryEntity?> GetByName(string name)
        {
            try
            {
                return await _context.country
                    .FirstOrDefaultAsync(c => c.name == name);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el country por nombre.", ex);
            }
        }


        public async Task<bool> Update(CountryEntity entity)
        {
            try
            {
                var existingEntity = await _context.country.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    throw new EntityNotFoundException("El pais que se intenta actualizar no existe.");
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