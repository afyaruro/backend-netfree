using System.Net;
using Application.Base.Dto;
using Application.Common.Exceptions;
using Application.Service.Gender;
using Application.Service.Gender.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/gender")]
    public class GenderController : ControllerBase
    {
        private readonly GenderService _genderService;

        public GenderController(GenderService genderService)
        {
            _genderService = genderService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> CreateGender([FromBody] GenderInputDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Datos del genero no son válidos" });

            try
            {
                var response = await _genderService.Create(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                return CreatedAtAction(nameof(CreateGender), new { success = true, message = response.message, data = response.entity });
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
                var response = await _genderService.GetAll(dto);

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
        public async Task<IActionResult> Update([FromBody] GenderUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { success = false, message = "Datos del genero no son válidos" });
            }

            try
            {
                var response = await _genderService.Update(dto);

                return Ok(new { success = true, message = "El genero se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] GenderDeleteDto dto)
        {
            if (dto.Id <= 0)
                return BadRequest(new { success = false, message = "El identificador del genero no es válido." });

            try
            {
                var result = await _genderService.Delete(dto.Id);
                return Ok(new { success = result, message = result == true ? "El genero se ha eliminado correctamente." : "No se ha eliminado" });
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