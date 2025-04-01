using Application.Common.Exceptions;
using Application.Jwt;
using Application.Service.User;
using Application.Service.User.Created.Dto;
using Application.Service.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JWTService _jwtService;


        public UserController(UserService userService, JWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> CreateUser([FromBody] UserInputDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Datos de usuario no válidos" });

            try
            {
                var response = await _userService.Create(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                var token = _jwtService.generateToken(response.entity!.mail);
                return CreatedAtAction(nameof(CreateUser), new { success = true, message = response.message, data = response.entity, token = token });
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { success = false, message = e.Message });
            }
            catch (EntityExistException e)
            {
                return Conflict(new { success = false, message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { success = false, message = $"Error: {e}" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var response = await _userService.Login(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                var token = _jwtService.generateToken(response.entity!.mail);
                return Ok(new { success = true, message = response.message, data = response.entity, token = token });
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { success = false, message = e.Message });
            }
            catch (NotAuthException e)
            {
                return Unauthorized(new { success = false, message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { success = false, message = $"Error: {e}" });
            }
        }
    }
}