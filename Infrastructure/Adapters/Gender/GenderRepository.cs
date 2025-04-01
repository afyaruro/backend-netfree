using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Entity.Gender;
using Domain.Entity.Response;
using Domain.Port;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Gender
{
    public class GenderRepository : IGenderRepository
    {

        private readonly SqlServerContext _context;
        public GenderRepository(SqlServerContext context)
        {
            _context = context;
        }
        public async Task<GenderEntity> Add(GenderEntity entity)
        {
            try
            {
                _context.gender.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el genero.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var Gender = await _context.gender.FindAsync(id);
                if (Gender == null) return false;

                _context.gender.Remove(Gender);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el genero.", ex);
            }
        }

        public async Task<ResponseEntity<GenderEntity>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                var totalRecords = await _context.gender.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var genders = await _context.gender
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var resp = new ResponseEntity<GenderEntity>("Generos Obtenidos", genders);

                resp.totalPages = totalPages;
                resp.totalRecords = totalRecords;

                return resp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de generos con paginación.", ex);
            }
        }

        public async Task<GenderEntity?> GetById(int id)
        {
            try
            {
                return await _context.gender.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el genero por ID.", ex);
            }
        }

        public async Task<GenderEntity?> GetByName(string name)
        {
            try
            {
                return await _context.gender
                    .FirstOrDefaultAsync(c => c.name == name);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el genero por nombre.", ex);
            }
        }


        public async Task<bool> Update(GenderEntity entity)
        {
            try
            {
                var existingEntity = await _context.gender.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    throw new EntityNotFoundException("El genero que se intenta actualizar no existe.");
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