using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Models;
using PlanEstrategico.API.Data;
using Microsoft.EntityFrameworkCore;

namespace PlanEstrategico.API.Services
{
    // ====================== MISIÓN ======================
    public interface IMisionService
    {
        Task<MisionDto?> GetByEmpresaAsync(int empresaId);
        Task<MisionDto> CreateAsync(MisionCreateDto dto);
        Task UpdateAsync(int id, MisionCreateDto dto);
    }

    public class MisionService : IMisionService
    {
        private readonly PlanEstrategicoDbContext _context;

        public MisionService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<MisionDto?> GetByEmpresaAsync(int empresaId)
        {
            var mision = await _context.Misiones
                .FirstOrDefaultAsync(m => m.IdEmpresa == empresaId);

            return mision == null ? null : MapToDto(mision);
        }

        public async Task<MisionDto> CreateAsync(MisionCreateDto dto)
        {
            var mision = new Mision
            {
                IdEmpresa = dto.IdEmpresa,
                Descripcion = dto.Descripcion,
                Responsable = dto.Responsable
            };

            _context.Misiones.Add(mision);
            await _context.SaveChangesAsync();
            return MapToDto(mision);
        }

        public async Task UpdateAsync(int id, MisionCreateDto dto)
        {
            var mision = await _context.Misiones.FindAsync(id);
            if (mision == null) throw new InvalidOperationException("Misión no encontrada");

            mision.Descripcion = dto.Descripcion;
            mision.Responsable = dto.Responsable;
            mision.FechaActualizacion = DateTime.UtcNow;

            _context.Misiones.Update(mision);
            await _context.SaveChangesAsync();
        }

        private static MisionDto MapToDto(Mision m) => new()
        {
            IdMision = m.IdMision,
            IdEmpresa = m.IdEmpresa,
            Descripcion = m.Descripcion,
            Responsable = m.Responsable,
            FechaActualizacion = m.FechaActualizacion
        };
    }

    // ====================== VISIÓN ======================
    public interface IVisionService
    {
        Task<VisionDto?> GetByEmpresaAsync(int empresaId);
        Task<VisionDto> CreateAsync(VisionCreateDto dto);
        Task UpdateAsync(int id, VisionCreateDto dto);
    }

    public class VisionService : IVisionService
    {
        private readonly PlanEstrategicoDbContext _context;

        public VisionService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<VisionDto?> GetByEmpresaAsync(int empresaId)
        {
            var vision = await _context.Visiones
                .FirstOrDefaultAsync(v => v.IdEmpresa == empresaId);

            return vision == null ? null : MapToDto(vision);
        }

        public async Task<VisionDto> CreateAsync(VisionCreateDto dto)
        {
            var vision = new Vision
            {
                IdEmpresa = dto.IdEmpresa,
                Descripcion = dto.Descripcion,
                Responsable = dto.Responsable
            };

            _context.Visiones.Add(vision);
            await _context.SaveChangesAsync();
            return MapToDto(vision);
        }

        public async Task UpdateAsync(int id, VisionCreateDto dto)
        {
            var vision = await _context.Visiones.FindAsync(id);
            if (vision == null) throw new InvalidOperationException("Visión no encontrada");

            vision.Descripcion = dto.Descripcion;
            vision.Responsable = dto.Responsable;
            vision.Fecha = DateTime.UtcNow;

            _context.Visiones.Update(vision);
            await _context.SaveChangesAsync();
        }

        private static VisionDto MapToDto(Vision v) => new()
        {
            IdVision = v.IdVision,
            IdEmpresa = v.IdEmpresa,
            Descripcion = v.Descripcion,
            Responsable = v.Responsable,
            Fecha = v.Fecha
        };
    }

    // ====================== VALORES ======================
    public interface IValorService
    {
        Task<IEnumerable<ValorDto>> GetByEmpresaAsync(int empresaId);
        Task<ValorDto?> GetByIdAsync(int id);
        Task<ValorDto> CreateAsync(ValorCreateDto dto);
        Task UpdateAsync(int id, ValorCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class ValorService : IValorService
    {
        private readonly PlanEstrategicoDbContext _context;

        public ValorService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<ValorDto>> GetByEmpresaAsync(int empresaId)
        {
            var valores = await _context.Valores
                .Where(v => v.IdEmpresa == empresaId)
                .ToListAsync();

            return valores.Select(MapToDto);
        }

        public async Task<ValorDto?> GetByIdAsync(int id)
        {
            var valor = await _context.Valores.FindAsync(id);
            return valor == null ? null : MapToDto(valor);
        }

        public async Task<ValorDto> CreateAsync(ValorCreateDto dto)
        {
            var valor = new Valor
            {
                IdEmpresa = dto.IdEmpresa,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            _context.Valores.Add(valor);
            await _context.SaveChangesAsync();
            return MapToDto(valor);
        }

        public async Task UpdateAsync(int id, ValorCreateDto dto)
        {
            var valor = await _context.Valores.FindAsync(id);
            if (valor == null) throw new InvalidOperationException("Valor no encontrado");

            valor.Nombre = dto.Nombre;
            valor.Descripcion = dto.Descripcion;

            _context.Valores.Update(valor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var valor = await _context.Valores.FindAsync(id);
            if (valor == null) throw new InvalidOperationException("Valor no encontrado");

            _context.Valores.Remove(valor);
            await _context.SaveChangesAsync();
        }

        private static ValorDto MapToDto(Valor v) => new()
        {
            IdValor = v.IdValor,
            IdEmpresa = v.IdEmpresa,
            Nombre = v.Nombre,
            Descripcion = v.Descripcion,
            FechaRegistro = v.FechaRegistro
        };
    }

    // ====================== OBJETIVOS ESTRATÉGICOS ======================
    public interface IObjetivoEstrategicoService
    {
        Task<IEnumerable<ObjetivoEstrategicoDto>> GetByEmpresaAsync(int empresaId);
        Task<ObjetivoEstrategicoDto?> GetByIdAsync(int id);
        Task<ObjetivoEstrategicoDto> CreateAsync(ObjetivoEstrategicoCreateDto dto);
        Task UpdateAsync(int id, ObjetivoEstrategicoCreateDto dto);
        Task DeleteAsync(int id);
        Task<int> GetCountByEmpresaAsync(int empresaId);
    }

    public class ObjetivoEstrategicoService : IObjetivoEstrategicoService
    {
        private readonly PlanEstrategicoDbContext _context;

        public ObjetivoEstrategicoService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<ObjetivoEstrategicoDto>> GetByEmpresaAsync(int empresaId)
        {
            var objetivos = await _context.ObjetivosEstrategicos
                .Where(o => o.IdEmpresa == empresaId)
                .OrderByDescending(o => o.FechaRegistro)
                .ToListAsync();

            return objetivos.Select(MapToDto);
        }

        public async Task<ObjetivoEstrategicoDto?> GetByIdAsync(int id)
        {
            var objetivo = await _context.ObjetivosEstrategicos.FindAsync(id);
            return objetivo == null ? null : MapToDto(objetivo);
        }

        public async Task<ObjetivoEstrategicoDto> CreateAsync(ObjetivoEstrategicoCreateDto dto)
        {
            var objetivo = new ObjetivoEstrategico
            {
                IdEmpresa = dto.IdEmpresa,
                Objetivo = dto.Objetivo,
                UEN = dto.UEN,
                Indicador = dto.Indicador,
                Meta = dto.Meta,
                Responsable = dto.Responsable,
                Estado = dto.Estado
            };

            _context.ObjetivosEstrategicos.Add(objetivo);
            await _context.SaveChangesAsync();
            return MapToDto(objetivo);
        }

        public async Task UpdateAsync(int id, ObjetivoEstrategicoCreateDto dto)
        {
            var objetivo = await _context.ObjetivosEstrategicos.FindAsync(id);
            if (objetivo == null) throw new InvalidOperationException("Objetivo no encontrado");

            objetivo.Objetivo = dto.Objetivo;
            objetivo.UEN = dto.UEN;
            objetivo.Indicador = dto.Indicador;
            objetivo.Meta = dto.Meta;
            objetivo.Responsable = dto.Responsable;
            objetivo.Estado = dto.Estado;
            objetivo.FechaModificacion = DateTime.UtcNow;

            _context.ObjetivosEstrategicos.Update(objetivo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var objetivo = await _context.ObjetivosEstrategicos.FindAsync(id);
            if (objetivo == null) throw new InvalidOperationException("Objetivo no encontrado");

            _context.ObjetivosEstrategicos.Remove(objetivo);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountByEmpresaAsync(int empresaId)
        {
            return await _context.ObjetivosEstrategicos
                .Where(o => o.IdEmpresa == empresaId)
                .CountAsync();
        }

        private static ObjetivoEstrategicoDto MapToDto(ObjetivoEstrategico o) => new()
        {
            IdObjetivo = o.IdObjetivo,
            IdEmpresa = o.IdEmpresa,
            Objetivo = o.Objetivo,
            UEN = o.UEN,
            Indicador = o.Indicador,
            Meta = o.Meta,
            Responsable = o.Responsable,
            Estado = o.Estado,
            FechaRegistro = o.FechaRegistro
        };
    }
}
