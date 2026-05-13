namespace PlanEstrategico.API.Models
{
    /// <summary>
    /// Entidad de Usuario para autenticación
    /// </summary>
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int IdRol { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }

        // Navegación
        public virtual Rol? Rol { get; set; }
    }

    /// <summary>
    /// Entidad de Rol
    /// </summary>
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Navegación
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }

    /// <summary>
    /// Entidad de Permiso
    /// </summary>
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Navegación
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }

    /// <summary>
    /// Entidad de RolPermiso (Relación muchos a muchos)
    /// </summary>
    public class RolPermiso
    {
        public int IdRolPermiso { get; set; }
        public int IdRol { get; set; }
        public int IdPermiso { get; set; }

        // Navegación
        public virtual Rol? Rol { get; set; }
        public virtual Permiso? Permiso { get; set; }
    }

    /// <summary>
    /// Entidad de Auditoría
    /// </summary>
    public class Auditoria
    {
        public long IdAuditoria { get; set; }
        public int IdUsuario { get; set; }
        public string Tabla { get; set; } = string.Empty;
        public string Operacion { get; set; } = string.Empty;
        public string? ValorAnterior { get; set; }
        public string? ValorNuevo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? DireccionIP { get; set; }

        // Navegación
        public virtual Usuario? Usuario { get; set; }
    }

    /// <summary>
    /// Entidad de Log
    /// </summary>
    public class Log
    {
        public long IdLog { get; set; }
        public string Nivel { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public string? Excepcion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? Usuario { get; set; }
        public string? Modulo { get; set; }
    }
}
