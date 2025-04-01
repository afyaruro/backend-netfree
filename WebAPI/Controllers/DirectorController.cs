
using System.Net;
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Country;
using Application.Service.Director;
using Application.Service.Director.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/director")]
    public class DirectorController : ControllerBase
    {
        private readonly CountryService _countryService;
        private readonly DirectorService _directorService;


        public DirectorController(CountryService countryService, DirectorService directorService)
        {
            _countryService = countryService;
            _directorService = directorService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> CreateUser([FromBody] DirectorInputDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Datos del Director no son válidos" });

            try
            {
                var response = await _directorService.Create(dto, _countryService);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                return CreatedAtAction(nameof(CreateUser), new { success = true, message = response.message, data = response.entity });
            }

            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (EntityExistException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PaginationDto dto)
        {
            try
            {
                var response = await _directorService.GetAll(_countryService, dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                return Ok(new { success = true, message = response.message, totalRegistros = response.totalRecords, totalPages = response.totalPages, data = response.listEntity });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }

            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DirectorUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { success = false, message = "Datos del Director no son válidos" });
            }

            try
            {
                var response = await _directorService.Update(dto, _countryService);

                return Ok(new { success = true, message = "El Director se ha actualizado exitosamente" });
            }
            catch (EntityNotUpdateException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

            catch (EntityNotFoundException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

            catch (BadRequestException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DirectorDeleteDto dto)
        {
            if (dto.Id <= 0)
                return BadRequest(new { success = false, message = "El identificador del Director no es válido." });

            try
            {
                var result = await _directorService.Delete(dto.Id);
                return Ok(new { success = result, message = result == true ? "El Director se ha eliminado correctamente." : "No se ha eliminado" });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (EntityNotDelete ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }
    }
}