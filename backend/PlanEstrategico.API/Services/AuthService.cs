using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Models;
using PlanEstrategico.API.Repositories;
using PlanEstrategico.API.Utilities;
using Microsoft.EntityFrameworkCore;
using PlanEstrategico.API.Data;

namespace PlanEstrategico.API.Services
{
    /// <summary>
    /// Servicio de autenticación
    /// </summary>
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<UsuarioDto?> RegisterAsync(RegistroRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly PlanEstrategicoDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(
            PlanEstrategicoDbContext context,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuario == null || !_passwordHasher.Verify(request.Password, usuario.PasswordHash))
            {
                return null;
            }

            if (!usuario.Activo)
            {
                throw new InvalidOperationException("El usuario está inactivo");
            }

            usuario.UltimoAcceso = DateTime.UtcNow;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            var token = _tokenGenerator.GenerateToken(
                usuario.IdUsuario,
                usuario.Email,
                usuario.Nombre,
                usuario.Rol?.Nombre ?? "Usuario");

            return new LoginResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario,
                RolNombre = usuario.Rol?.Nombre ?? "Usuario",
                Token = token
            };
        }

        public async Task<UsuarioDto?> RegisterAsync(RegistroRequest request)
        {
            // Validar que no exista el usuario
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == request.Email || u.NombreUsuario == request.NombreUsuario);

            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("El email o nombre de usuario ya existe");
            }

            // Crear nuevo usuario
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Email = request.Email,
                NombreUsuario = request.NombreUsuario,
                PasswordHash = _passwordHasher.Hash(request.Password),
                IdRol = 3 // Rol de Usuario por defecto
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario,
                IdRol = usuario.IdRol,
                Activo = usuario.Activo,
                FechaCreacion = usuario.FechaCreacion
            };
        }
    }

    /// <summary>
    /// Servicio de usuarios
    /// </summary>
    public interface IUsuarioService
    {
        Task<UsuarioDto?> GetUsuarioByIdAsync(int id);
        Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync();
        Task<IEnumerable<Permiso>> GetPermisosUsuarioAsync(int idUsuario);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly PlanEstrategicoDbContext _context;

        public UsuarioService(PlanEstrategicoDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null) return null;

            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario,
                IdRol = usuario.IdRol,
                RolNombre = usuario.Rol?.Nombre ?? string.Empty,
                Activo = usuario.Activo,
                FechaCreacion = usuario.FechaCreacion
            };
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Email = u.Email,
                NombreUsuario = u.NombreUsuario,
                IdRol = u.IdRol,
                RolNombre = u.Rol?.Nombre ?? string.Empty,
                Activo = u.Activo,
                FechaCreacion = u.FechaCreacion
            });
        }

        public async Task<IEnumerable<Permiso>> GetPermisosUsuarioAsync(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null) return new List<Permiso>();

            var permisos = await _context.Permisos
                .Where(p => p.RolPermisos.Any(rp => rp.IdRol == usuario.IdRol))
                .ToListAsync();

            return permisos;
        }
    }
}
