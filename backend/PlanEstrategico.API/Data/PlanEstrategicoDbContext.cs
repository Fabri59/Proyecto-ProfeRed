using Microsoft.EntityFrameworkCore;
using PlanEstrategico.API.Models;

namespace PlanEstrategico.API.Data
{
    /// <summary>
    /// DbContext para el Sistema de Plan Estratégico de TI
    /// </summary>
    public class PlanEstrategicoDbContext : DbContext
    {
        public PlanEstrategicoDbContext(DbContextOptions<PlanEstrategicoDbContext> options)
            : base(options)
        {
        }

        // ============== SEGURIDAD ==============
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Log> Logs { get; set; }

        // ============== MÓDULOS ESTRATÉGICOS ==============
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Mision> Misiones { get; set; }
        public DbSet<Vision> Visiones { get; set; }
        public DbSet<Valor> Valores { get; set; }
        public DbSet<ObjetivoEstrategico> ObjetivosEstrategicos { get; set; }
        public DbSet<AnalisisFODA> AnalisisFODAs { get; set; }
        public DbSet<CadenaValor> CadenasValor { get; set; }
        public DbSet<AutoCadenaValor> AutoCadenasValor { get; set; }
        public DbSet<MatrizBCG> MatricesBCG { get; set; }
        public DbSet<AutoBCG> AutoBCGs { get; set; }
        public DbSet<AnalisisPorter> AnalisisPorters { get; set; }
        public DbSet<AutoPorter> AutoPorters { get; set; }
        public DbSet<AnalisisPEST> AnalisisPESTs { get; set; }
        public DbSet<EstrategiaIdentificacion> EstrategiasIdentificacion { get; set; }
        public DbSet<MatrizCAME> MatrizesCAME { get; set; }
        public DbSet<ResumenEjecutivo> ResumenesEjecutivos { get; set; }
        public DbSet<ReporteFinal> ReportesFinal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============== CONFIGURACIÓN DE ENTIDADES DE SEGURIDAD ==============
            
            // Usuarios
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario).IsUnique();
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nombre).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(u => u.NombreUsuario).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

            // Roles
            modelBuilder.Entity<Rol>()
                .HasKey(r => r.IdRol);
            modelBuilder.Entity<Rol>()
                .HasIndex(r => r.Nombre).IsUnique();

            // Permisos
            modelBuilder.Entity<Permiso>()
                .HasKey(p => p.IdPermiso);
            modelBuilder.Entity<Permiso>()
                .HasIndex(p => p.Nombre).IsUnique();

            // RolPermisos
            modelBuilder.Entity<RolPermiso>()
                .HasKey(rp => rp.IdRolPermiso);
            modelBuilder.Entity<RolPermiso>()
                .HasIndex(rp => new { rp.IdRol, rp.IdPermiso }).IsUnique();
            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPermisos)
                .HasForeignKey(rp => rp.IdRol);
            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(rp => rp.IdPermiso);

            // Auditoría
            modelBuilder.Entity<Auditoria>()
                .HasKey(a => a.IdAuditoria);
            modelBuilder.Entity<Auditoria>()
                .HasIndex(a => a.IdUsuario);
            modelBuilder.Entity<Auditoria>()
                .HasIndex(a => a.Tabla);
            modelBuilder.Entity<Auditoria>()
                .HasIndex(a => a.Fecha);
            modelBuilder.Entity<Auditoria>()
                .HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.IdUsuario);

            // Logs
            modelBuilder.Entity<Log>()
                .HasKey(l => l.IdLog);
            modelBuilder.Entity<Log>()
                .HasIndex(l => l.Fecha);

            // ============== CONFIGURACIÓN DE ENTIDADES DE EMPRESA ==============
            
            modelBuilder.Entity<Empresa>()
                .HasKey(e => e.IdEmpresa);
            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.RUC).IsUnique();
            modelBuilder.Entity<Empresa>()
                .Property(e => e.Nombre).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<Empresa>()
                .Property(e => e.RUC).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Empresa>()
                .Property(e => e.Descripcion).HasMaxLength(4000);

            // Relación uno a muchos con otras entidades
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Misiones)
                .WithOne(m => m.Empresa)
                .HasForeignKey(m => m.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Visiones)
                .WithOne(v => v.Empresa)
                .HasForeignKey(v => v.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Valores)
                .WithOne(v => v.Empresa)
                .HasForeignKey(v => v.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.ObjetivosEstrategicos)
                .WithOne(o => o.Empresa)
                .HasForeignKey(o => o.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.AnalisisFODAs)
                .WithOne(f => f.Empresa)
                .HasForeignKey(f => f.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE MISIÓN ==============
            modelBuilder.Entity<Mision>()
                .HasKey(m => m.IdMision);
            modelBuilder.Entity<Mision>()
                .Property(m => m.Descripcion).HasMaxLength(4000).IsRequired();

            // ============== CONFIGURACIÓN DE VISIÓN ==============
            modelBuilder.Entity<Vision>()
                .HasKey(v => v.IdVision);
            modelBuilder.Entity<Vision>()
                .Property(v => v.Descripcion).HasMaxLength(4000).IsRequired();

            // ============== CONFIGURACIÓN DE VALORES ==============
            modelBuilder.Entity<Valor>()
                .HasKey(v => v.IdValor);
            modelBuilder.Entity<Valor>()
                .Property(v => v.Nombre).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Valor>()
                .Property(v => v.Descripcion).HasMaxLength(4000).IsRequired();

            // ============== CONFIGURACIÓN DE OBJETIVOS ==============
            modelBuilder.Entity<ObjetivoEstrategico>()
                .HasKey(o => o.IdObjetivo);
            modelBuilder.Entity<ObjetivoEstrategico>()
                .Property(o => o.Objetivo).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<ObjetivoEstrategico>()
                .HasIndex(o => o.IdEmpresa);
            modelBuilder.Entity<ObjetivoEstrategico>()
                .HasOne(o => o.Empresa)
                .WithMany(e => e.ObjetivosEstrategicos)
                .HasForeignKey(o => o.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE FODA ==============
            modelBuilder.Entity<AnalisisFODA>()
                .HasKey(f => f.IdAnalisis);
            modelBuilder.Entity<AnalisisFODA>()
                .Property(f => f.Descripcion).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<AnalisisFODA>()
                .HasIndex(f => f.Tipo);
            modelBuilder.Entity<AnalisisFODA>()
                .HasIndex(f => f.IdEmpresa);
            modelBuilder.Entity<AnalisisFODA>()
                .HasOne(f => f.Empresa)
                .WithMany(e => e.AnalisisFODAs)
                .HasForeignKey(f => f.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE CADENA DE VALOR ==============
            modelBuilder.Entity<CadenaValor>()
                .HasKey(c => c.IdCadenaValor);
            modelBuilder.Entity<CadenaValor>()
                .HasIndex(c => c.IdEmpresa);
            modelBuilder.Entity<CadenaValor>()
                .HasOne(c => c.Empresa)
                .WithMany(e => e.CadenasValor)
                .HasForeignKey(c => c.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE AUTO CADENA VALOR ==============
            modelBuilder.Entity<AutoCadenaValor>()
                .HasKey(a => a.IdAutoCadena);
            modelBuilder.Entity<AutoCadenaValor>()
                .HasIndex(a => a.IdEmpresa);
            modelBuilder.Entity<AutoCadenaValor>()
                .HasOne(a => a.Empresa)
                .WithMany(e => e.AutoCadenasValor)
                .HasForeignKey(a => a.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE MATRIZ BCG ==============
            modelBuilder.Entity<MatrizBCG>()
                .HasKey(b => b.IdBCG);
            modelBuilder.Entity<MatrizBCG>()
                .HasIndex(b => b.IdEmpresa);
            modelBuilder.Entity<MatrizBCG>()
                .HasOne(b => b.Empresa)
                .WithMany(e => e.MatricesBCG)
                .HasForeignKey(b => b.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE AUTO BCG ==============
            modelBuilder.Entity<AutoBCG>()
                .HasKey(a => a.IdAutoBCG);
            modelBuilder.Entity<AutoBCG>()
                .HasIndex(a => a.IdEmpresa);
            modelBuilder.Entity<AutoBCG>()
                .HasOne(a => a.Empresa)
                .WithMany(e => e.AutoBCGs)
                .HasForeignKey(a => a.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE PORTER ==============
            modelBuilder.Entity<AnalisisPorter>()
                .HasKey(p => p.IdPorter);
            modelBuilder.Entity<AnalisisPorter>()
                .HasIndex(p => p.IdEmpresa);
            modelBuilder.Entity<AnalisisPorter>()
                .HasOne(p => p.Empresa)
                .WithMany(e => e.AnalisisPorters)
                .HasForeignKey(p => p.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE AUTO PORTER ==============
            modelBuilder.Entity<AutoPorter>()
                .HasKey(a => a.IdAutoPorter);
            modelBuilder.Entity<AutoPorter>()
                .HasIndex(a => a.IdEmpresa);
            modelBuilder.Entity<AutoPorter>()
                .HasOne(a => a.Empresa)
                .WithMany(e => e.AutoPorters)
                .HasForeignKey(a => a.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE PEST ==============
            modelBuilder.Entity<AnalisisPEST>()
                .HasKey(p => p.IdPEST);
            modelBuilder.Entity<AnalisisPEST>()
                .HasIndex(p => p.IdEmpresa);
            modelBuilder.Entity<AnalisisPEST>()
                .HasOne(p => p.Empresa)
                .WithMany(e => e.AnalisisPESTs)
                .HasForeignKey(p => p.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE ESTRATEGIA IDENTIFICACIÓN ==============
            modelBuilder.Entity<EstrategiaIdentificacion>()
                .HasKey(e => e.IdEstrategia);
            modelBuilder.Entity<EstrategiaIdentificacion>()
                .HasIndex(e => e.IdEmpresa);
            modelBuilder.Entity<EstrategiaIdentificacion>()
                .HasOne(e => e.Empresa)
                .WithMany(emp => emp.EstrategiasIdentificacion)
                .HasForeignKey(e => e.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE MATRIZ CAME ==============
            modelBuilder.Entity<MatrizCAME>()
                .HasKey(c => c.IdCAME);
            modelBuilder.Entity<MatrizCAME>()
                .HasIndex(c => c.IdEmpresa);
            modelBuilder.Entity<MatrizCAME>()
                .HasOne(c => c.Empresa)
                .WithMany(e => e.MatrizesCAME)
                .HasForeignKey(c => c.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE RESUMEN EJECUTIVO ==============
            modelBuilder.Entity<ResumenEjecutivo>()
                .HasKey(r => r.IdResumen);
            modelBuilder.Entity<ResumenEjecutivo>()
                .HasIndex(r => r.IdEmpresa);
            modelBuilder.Entity<ResumenEjecutivo>()
                .HasOne(r => r.Empresa)
                .WithMany(e => e.ResumenesEjecutivos)
                .HasForeignKey(r => r.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);

            // ============== CONFIGURACIÓN DE REPORTE FINAL ==============
            modelBuilder.Entity<ReporteFinal>()
                .HasKey(r => r.IdReporte);
            modelBuilder.Entity<ReporteFinal>()
                .HasIndex(r => r.IdEmpresa);
            modelBuilder.Entity<ReporteFinal>()
                .HasOne(r => r.Empresa)
                .WithMany(e => e.ReportesFinal)
                .HasForeignKey(r => r.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
