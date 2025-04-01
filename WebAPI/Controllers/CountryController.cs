using System.Net;
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Country;
using Application.Service.Country.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> CreateUser([FromBody] CountryInputDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Datos del pais no son válidos" });

            try
            {
                var response = await _countryService.Create(dto);

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
                var response = await _countryService.GetAll(dto);

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
        public async Task<IActionResult> Update([FromBody] CountryUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { success = false, message = "Datos del pais no son válidos" });
            }

            try
            {
                var response = await _countryService.Update(dto);

                return Ok(new { success = true, message = "El pais se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] CountryDeleteDto dto)
        {
            if (dto.Id <= 0)
                return BadRequest(new { success = false, message = "El identificador del país no es válido." });

            try
            {
                var result = await _countryService.Delete(dto.Id);
                return Ok(new { success = result, message = result == true ? "El país se ha eliminado correctamente." : "No se ha eliminado" });
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