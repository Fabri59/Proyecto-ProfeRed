using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Red.Models;

namespace Proyecto_Red.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<PresentacionPlan> Presentaciones { get; set; }
        public DbSet<Mision> Misiones { get; set; }
        public DbSet<Vision> Visiones { get; set; }
        public DbSet<ValorInstitucional> ValoresInstitucionales { get; set; }
        public DbSet<ObjetivoEstrategico> ObjetivosEstrategicos { get; set; }
        public DbSet<UEN> UENes { get; set; }
        public DbSet<AnalisisInterno> AnalisisInternos { get; set; }
        public DbSet<AnalisisExterno> AnalisisExternos { get; set; }
        public DbSet<MatrizFoda> MatricesFoda { get; set; }
        public DbSet<AnalisisPEST> AnalisisPESTs { get; set; }
        public DbSet<AnalisisPorter> AnalisisPorters { get; set; }
        public DbSet<CadenaValor> CadenasValor { get; set; }
        public DbSet<MatrizBCG> MatricesBCG { get; set; }
        public DbSet<MatrizCAME> MatricesCAME { get; set; }
        public DbSet<Estrategia> Estrategias { get; set; }
        public DbSet<IndicadorKPI> IndicadoresKPI { get; set; }
        public DbSet<PlanAccion> PlanesAccion { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<Cronograma> Cronogramas { get; set; }

        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Resultado> Resultados { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Empresa>()
                .HasIndex(e => e.RUC)
                .IsUnique();
        }
    }
}
