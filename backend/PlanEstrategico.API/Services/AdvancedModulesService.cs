using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Models;
using PlanEstrategico.API.Data;
using Microsoft.EntityFrameworkCore;

namespace PlanEstrategico.API.Services
{
    // ====================== MATRIZ BCG ======================
    public interface IMatrizBCGService
    {
        Task<IEnumerable<MatrizBCGDto>> GetByEmpresaAsync(int empresaId);
        Task<MatrizBCGDto?> GetByIdAsync(int id);
        Task<MatrizBCGDto> CreateAsync(MatrizBCGCreateDto dto);
        Task UpdateAsync(int id, MatrizBCGCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class MatrizBCGService : IMatrizBCGService
    {
        private readonly PlanEstrategicoDbContext _context;

        public MatrizBCGService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<MatrizBCGDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.MatricesBCG
                .Where(b => b.IdEmpresa == empresaId)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<MatrizBCGDto?> GetByIdAsync(int id)
        {
            var item = await _context.MatricesBCG.FindAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<MatrizBCGDto> CreateAsync(MatrizBCGCreateDto dto)
        {
            var item = new MatrizBCG
            {
                IdEmpresa = dto.IdEmpresa,
                Producto = dto.Producto,
                Participacion = dto.Participacion,
                Crecimiento = dto.Crecimiento,
                Clasificacion = CalcularClasificacion(dto.Participacion, dto.Crecimiento)
            };

            _context.MatricesBCG.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, MatrizBCGCreateDto dto)
        {
            var item = await _context.MatricesBCG.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Producto BCG no encontrado");

            item.Producto = dto.Producto;
            item.Participacion = dto.Participacion;
            item.Crecimiento = dto.Crecimiento;
            item.Clasificacion = CalcularClasificacion(dto.Participacion, dto.Crecimiento);

            _context.MatricesBCG.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.MatricesBCG.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Producto BCG no encontrado");

            _context.MatricesBCG.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static string CalcularClasificacion(decimal participacion, decimal crecimiento)
        {
            if (crecimiento > 10 && participacion > 50)
                return "Estrella";
            if (crecimiento <= 10 && participacion > 50)
                return "Vaca";
            if (crecimiento > 10 && participacion <= 50)
                return "Interrogante";
            return "Perro";
        }

        private static MatrizBCGDto MapToDto(MatrizBCG b) => new()
        {
            IdBCG = b.IdBCG,
            IdEmpresa = b.IdEmpresa,
            Producto = b.Producto,
            Participacion = b.Participacion,
            Crecimiento = b.Crecimiento,
            Clasificacion = b.Clasificacion,
            FechaRegistro = b.FechaRegistro
        };
    }

    // ====================== ANÁLISIS PORTER ======================
    public interface IAnalisisPorterService
    {
        Task<IEnumerable<AnalisisPorterDto>> GetByEmpresaAsync(int empresaId);
        Task<AnalisisPorterDto?> GetByIdAsync(int id);
        Task<AnalisisPorterDto> CreateAsync(AnalisisPorterCreateDto dto);
        Task UpdateAsync(int id, AnalisisPorterCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class AnalisisPorterService : IAnalisisPorterService
    {
        private readonly PlanEstrategicoDbContext _context;

        public AnalisisPorterService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<AnalisisPorterDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.AnalisisPorters
                .Where(p => p.IdEmpresa == empresaId)
                .OrderBy(p => p.Fuerza)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<AnalisisPorterDto?> GetByIdAsync(int id)
        {
            var item = await _context.AnalisisPorters.FindAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<AnalisisPorterDto> CreateAsync(AnalisisPorterCreateDto dto)
        {
            var item = new AnalisisPorter
            {
                IdEmpresa = dto.IdEmpresa,
                Fuerza = dto.Fuerza,
                Descripcion = dto.Descripcion,
                Puntaje = dto.Puntaje
            };

            _context.AnalisisPorters.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, AnalisisPorterCreateDto dto)
        {
            var item = await _context.AnalisisPorters.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Análisis Porter no encontrado");

            item.Fuerza = dto.Fuerza;
            item.Descripcion = dto.Descripcion;
            item.Puntaje = dto.Puntaje;

            _context.AnalisisPorters.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.AnalisisPorters.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Análisis Porter no encontrado");

            _context.AnalisisPorters.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static AnalisisPorterDto MapToDto(AnalisisPorter p) => new()
        {
            IdPorter = p.IdPorter,
            IdEmpresa = p.IdEmpresa,
            Fuerza = p.Fuerza,
            Descripcion = p.Descripcion,
            Puntaje = p.Puntaje,
            FechaRegistro = p.FechaRegistro
        };
    }

    // ====================== ANÁLISIS PEST ======================
    public interface IAnalisisPESTService
    {
        Task<IEnumerable<AnalisisPESTDto>> GetByEmpresaAsync(int empresaId);
        Task<IEnumerable<AnalisisPESTDto>> GetByTipoAsync(int empresaId, string tipo);
        Task<AnalisisPESTDto?> GetByIdAsync(int id);
        Task<AnalisisPESTDto> CreateAsync(AnalisisPESTCreateDto dto);
        Task UpdateAsync(int id, AnalisisPESTCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class AnalisisPESTService : IAnalisisPESTService
    {
        private readonly PlanEstrategicoDbContext _context;

        public AnalisisPESTService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<AnalisisPESTDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.AnalisisPESTs
                .Where(p => p.IdEmpresa == empresaId)
                .OrderBy(p => p.Tipo)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<IEnumerable<AnalisisPESTDto>> GetByTipoAsync(int empresaId, string tipo)
        {
            var items = await _context.AnalisisPESTs
                .Where(p => p.IdEmpresa == empresaId && p.Tipo == tipo)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<AnalisisPESTDto?> GetByIdAsync(int id)
        {
            var item = await _context.AnalisisPESTs.FindAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<AnalisisPESTDto> CreateAsync(AnalisisPESTCreateDto dto)
        {
            var item = new AnalisisPEST
            {
                IdEmpresa = dto.IdEmpresa,
                Tipo = dto.Tipo,
                Descripcion = dto.Descripcion,
                Impacto = dto.Impacto
            };

            _context.AnalisisPESTs.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, AnalisisPESTCreateDto dto)
        {
            var item = await _context.AnalisisPESTs.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Análisis PEST no encontrado");

            item.Tipo = dto.Tipo;
            item.Descripcion = dto.Descripcion;
            item.Impacto = dto.Impacto;

            _context.AnalisisPESTs.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.AnalisisPESTs.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Análisis PEST no encontrado");

            _context.AnalisisPESTs.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static AnalisisPESTDto MapToDto(AnalisisPEST p) => new()
        {
            IdPEST = p.IdPEST,
            IdEmpresa = p.IdEmpresa,
            Tipo = p.Tipo,
            Descripcion = p.Descripcion,
            Impacto = p.Impacto,
            FechaRegistro = p.FechaRegistro
        };
    }

    // ====================== ESTRATEGIAS IDENTIFICACIÓN ======================
    public interface IEstrategiaIdentificacionService
    {
        Task<IEnumerable<EstrategiaIdentificacionDto>> GetByEmpresaAsync(int empresaId);
        Task<EstrategiaIdentificacionDto?> GetByIdAsync(int id);
        Task<EstrategiaIdentificacionDto> CreateAsync(EstrategiaIdentificacionCreateDto dto);
        Task UpdateAsync(int id, EstrategiaIdentificacionCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class EstrategiaIdentificacionService : IEstrategiaIdentificacionService
    {
        private readonly PlanEstrategicoDbContext _context;

        public EstrategiaIdentificacionService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<EstrategiaIdentificacionDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.EstrategiasIdentificacion
                .Where(e => e.IdEmpresa == empresaId)
                .OrderByDescending(e => e.FechaRegistro)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<EstrategiaIdentificacionDto?> GetByIdAsync(int id)
        {
            var item = await _context.EstrategiasIdentificacion.FindAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<EstrategiaIdentificacionDto> CreateAsync(EstrategiaIdentificacionCreateDto dto)
        {
            var item = new EstrategiaIdentificacion
            {
                IdEmpresa = dto.IdEmpresa,
                Nombre = dto.Nombre,
                Tipo = dto.Tipo,
                Prioridad = dto.Prioridad,
                Responsable = dto.Responsable
            };

            _context.EstrategiasIdentificacion.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, EstrategiaIdentificacionCreateDto dto)
        {
            var item = await _context.EstrategiasIdentificacion.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Estrategia no encontrada");

            item.Nombre = dto.Nombre;
            item.Tipo = dto.Tipo;
            item.Prioridad = dto.Prioridad;
            item.Responsable = dto.Responsable;

            _context.EstrategiasIdentificacion.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.EstrategiasIdentificacion.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Estrategia no encontrada");

            _context.EstrategiasIdentificacion.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static EstrategiaIdentificacionDto MapToDto(EstrategiaIdentificacion e) => new()
        {
            IdEstrategia = e.IdEstrategia,
            IdEmpresa = e.IdEmpresa,
            Nombre = e.Nombre,
            Tipo = e.Tipo,
            Prioridad = e.Prioridad,
            Responsable = e.Responsable,
            Estado = e.Estado,
            FechaRegistro = e.FechaRegistro
        };
    }

    // ====================== MATRIZ CAME ======================
    public interface IMatrizCAMEService
    {
        Task<IEnumerable<MatrizCAMEDto>> GetByEmpresaAsync(int empresaId);
        Task<IEnumerable<MatrizCAMEDto>> GetByTipoAsync(int empresaId, string tipo);
        Task<MatrizCAMEDto?> GetByIdAsync(int id);
        Task<MatrizCAMEDto> CreateAsync(MatrizCAMECreateDto dto);
        Task UpdateAsync(int id, MatrizCAMECreateDto dto);
        Task DeleteAsync(int id);
    }

    public class MatrizCAMEService : IMatrizCAMEService
    {
        private readonly PlanEstrategicoDbContext _context;

        public MatrizCAMEService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<MatrizCAMEDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.MatrizesCAME
                .Where(c => c.IdEmpresa == empresaId)
                .OrderBy(c => c.Tipo)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<IEnumerable<MatrizCAMEDto>> GetByTipoAsync(int empresaId, string tipo)
        {
            var items = await _context.MatrizesCAME
                .Where(c => c.IdEmpresa == empresaId && c.Tipo == tipo)
                .ToListAsync();

            return items.Select(MapToDto);
        }

        public async Task<MatrizCAMEDto?> GetByIdAsync(int id)
        {
            var item = await _context.MatrizesCAME.FindAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<MatrizCAMEDto> CreateAsync(MatrizCAMECreateDto dto)
        {
            var item = new MatrizCAME
            {
                IdEmpresa = dto.IdEmpresa,
                Tipo = dto.Tipo,
                Estrategia = dto.Estrategia,
                Responsable = dto.Responsable
            };

            _context.MatrizesCAME.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, MatrizCAMECreateDto dto)
        {
            var item = await _context.MatrizesCAME.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Estrategia CAME no encontrada");

            item.Tipo = dto.Tipo;
            item.Estrategia = dto.Estrategia;
            item.Responsable = dto.Responsable;

            _context.MatrizesCAME.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.MatrizesCAME.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Estrategia CAME no encontrada");

            _context.MatrizesCAME.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static MatrizCAMEDto MapToDto(MatrizCAME c) => new()
        {
            IdCAME = c.IdCAME,
            IdEmpresa = c.IdEmpresa,
            Tipo = c.Tipo,
            Estrategia = c.Estrategia,
            Responsable = c.Responsable,
            Estado = c.Estado,
            FechaRegistro = c.FechaRegistro
        };
    }
}
