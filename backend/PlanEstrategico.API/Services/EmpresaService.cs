using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Models;
using PlanEstrategico.API.Data;
using Microsoft.EntityFrameworkCore;

namespace PlanEstrategico.API.Services
{
    /// <summary>
    /// Servicio para empresa
    /// </summary>
    public interface IEmpresaService
    {
        Task<EmpresaDto?> GetEmpresaByIdAsync(int id);
        Task<IEnumerable<EmpresaDto>> GetAllEmpresasAsync();
        Task<EmpresaDto> CreateEmpresaAsync(EmpresaCreateDto dto);
        Task UpdateEmpresaAsync(int id, EmpresaCreateDto dto);
        Task DeleteEmpresaAsync(int id);
        Task<int> GetCountAsync();
    }

    public class EmpresaService : IEmpresaService
    {
        private readonly PlanEstrategicoDbContext _context;

        public EmpresaService(PlanEstrategicoDbContext context)
        {
            _context = context;
        }

        public async Task<EmpresaDto?> GetEmpresaByIdAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) return null;

            return MapToDto(empresa);
        }

        public async Task<IEnumerable<EmpresaDto>> GetAllEmpresasAsync()
        {
            var empresas = await _context.Empresas
                .Where(e => e.Activo)
                .ToListAsync();

            return empresas.Select(MapToDto);
        }

        public async Task<EmpresaDto> CreateEmpresaAsync(EmpresaCreateDto dto)
        {
            var empresa = new Empresa
            {
                Nombre = dto.Nombre,
                RUC = dto.RUC,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Rubro = dto.Rubro,
                Descripcion = dto.Descripcion,
                Responsable = dto.Responsable
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return MapToDto(empresa);
        }

        public async Task UpdateEmpresaAsync(int id, EmpresaCreateDto dto)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) throw new InvalidOperationException("Empresa no encontrada");

            empresa.Nombre = dto.Nombre;
            empresa.RUC = dto.RUC;
            empresa.Direccion = dto.Direccion;
            empresa.Telefono = dto.Telefono;
            empresa.Correo = dto.Correo;
            empresa.Rubro = dto.Rubro;
            empresa.Descripcion = dto.Descripcion;
            empresa.Responsable = dto.Responsable;
            empresa.FechaModificacion = DateTime.UtcNow;

            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmpresaAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) throw new InvalidOperationException("Empresa no encontrada");

            empresa.Activo = false;
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Empresas.Where(e => e.Activo).CountAsync();
        }

        private static EmpresaDto MapToDto(Empresa empresa)
        {
            return new EmpresaDto
            {
                IdEmpresa = empresa.IdEmpresa,
                Nombre = empresa.Nombre,
                RUC = empresa.RUC,
                Direccion = empresa.Direccion,
                Telefono = empresa.Telefono,
                Correo = empresa.Correo,
                Rubro = empresa.Rubro,
                Descripcion = empresa.Descripcion,
                Responsable = empresa.Responsable,
                FechaRegistro = empresa.FechaRegistro,
                FechaModificacion = empresa.FechaModificacion,
                Activo = empresa.Activo
            };
        }
    }

    /// <summary>
    /// Servicio para análisis FODA
    /// </summary>
    public interface IAnalisisFODAService
    {
        Task<AnalisisFODADto?> GetByIdAsync(int id);
        Task<IEnumerable<AnalisisFODADto>> GetByEmpresaAsync(int empresaId);
        Task<IEnumerable<AnalisisFODADto>> GetByTipoAsync(int empresaId, string tipo);
        Task<AnalisisFODADto> CreateAsync(AnalisisFODACreateDto dto);
        Task UpdateAsync(int id, AnalisisFODACreateDto dto);
        Task DeleteAsync(int id);
    }

    public class AnalisisFODAService : IAnalisisFODAService
    {
        private readonly PlanEstrategicoDbContext _context;

        public AnalisisFODAService(PlanEstrategicoDbContext context)
        {
            _context = context;
        }

        public async Task<AnalisisFODADto?> GetByIdAsync(int id)
        {
            var analisis = await _context.AnalisisFODAs.FindAsync(id);
            return analisis == null ? null : MapToDto(analisis);
        }

        public async Task<IEnumerable<AnalisisFODADto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.AnalisisFODAs
                .Where(f => f.IdEmpresa == empresaId)
                .OrderBy(f => f.Tipo)
                .ThenByDescending(f => f.Ponderacion)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<IEnumerable<AnalisisFODADto>> GetByTipoAsync(int empresaId, string tipo)
        {
            var items = await _context.AnalisisFODAs
                .Where(f => f.IdEmpresa == empresaId && f.Tipo == tipo)
                .OrderByDescending(f => f.Ponderacion)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<AnalisisFODADto> CreateAsync(AnalisisFODACreateDto dto)
        {
            var analisis = new AnalisisFODA
            {
                IdEmpresa = dto.IdEmpresa,
                Tipo = dto.Tipo,
                Descripcion = dto.Descripcion,
                Ponderacion = dto.Ponderacion,
                Estado = dto.Estado
            };

            _context.AnalisisFODAs.Add(analisis);
            await _context.SaveChangesAsync();

            return MapToDto(analisis);
        }

        public async Task UpdateAsync(int id, AnalisisFODACreateDto dto)
        {
            var analisis = await _context.AnalisisFODAs.FindAsync(id);
            if (analisis == null) throw new InvalidOperationException("Análisis no encontrado");

            analisis.Tipo = dto.Tipo;
            analisis.Descripcion = dto.Descripcion;
            analisis.Ponderacion = dto.Ponderacion;
            analisis.Estado = dto.Estado;

            _context.AnalisisFODAs.Update(analisis);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var analisis = await _context.AnalisisFODAs.FindAsync(id);
            if (analisis == null) throw new InvalidOperationException("Análisis no encontrado");

            _context.AnalisisFODAs.Remove(analisis);
            await _context.SaveChangesAsync();
        }

        private static AnalisisFODADto MapToDto(AnalisisFODA analisis)
        {
            return new AnalisisFODADto
            {
                IdAnalisis = analisis.IdAnalisis,
                IdEmpresa = analisis.IdEmpresa,
                Tipo = analisis.Tipo,
                Descripcion = analisis.Descripcion,
                Ponderacion = analisis.Ponderacion,
                Estado = analisis.Estado,
                FechaRegistro = analisis.FechaRegistro
            };
        }
    }
}
