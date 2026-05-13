using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Services;

namespace PlanEstrategico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login de usuario
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiErrorResponse { Mensaje = "Datos inválidos" });

            try
            {
                var result = await _authService.LoginAsync(request);
                if (result == null)
                    return Unauthorized(new ApiErrorResponse { Mensaje = "Email o contraseña incorrectos" });

                return Ok(new ApiResponse<LoginResponse>
                {
                    Exito = true,
                    Mensaje = "Login exitoso",
                    Datos = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Registrar nuevo usuario
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistroRequest request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request);
                return CreatedAtAction(nameof(Register), new { id = result?.IdUsuario }, new ApiResponse<UsuarioDto>
                {
                    Exito = true,
                    Mensaje = "Usuario registrado exitosamente",
                    Datos = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtener usuario por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                if (usuario == null)
                    return NotFound(new ApiErrorResponse { Mensaje = "Usuario no encontrado" });

                return Ok(new ApiResponse<UsuarioDto>
                {
                    Exito = true,
                    Datos = usuario
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Obtener todos los usuarios (solo admin)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllUsuariosAsync();
                return Ok(new ApiResponse<IEnumerable<UsuarioDto>>
                {
                    Exito = true,
                    Datos = usuarios
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Obtener permisos del usuario actual
        /// </summary>
        [HttpGet("permisos")]
        public async Task<IActionResult> GetPermisos()
        {
            try
            {
                var idUsuario = int.Parse(User.FindFirst("sub")?.Value ?? "0");
                if (idUsuario == 0)
                    return Unauthorized();

                var permisos = await _usuarioService.GetPermisosUsuarioAsync(idUsuario);
                return Ok(new ApiResponse<IEnumerable<object>>
                {
                    Exito = true,
                    Datos = permisos.Select(p => new { p.IdPermiso, p.Nombre })
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }
    }
}
