using System.Text;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using PlanEstrategico.API.Data;
using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Models;

namespace PlanEstrategico.API.Services
{
    public interface ICadenaValorService
    {
        Task<IEnumerable<CadenaValorDto>> GetByEmpresaAsync(int empresaId);
        Task<CadenaValorDto> CreateAsync(CadenaValorCreateDto dto);
        Task UpdateAsync(int id, CadenaValorCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class CadenaValorService : ICadenaValorService
    {
        private readonly PlanEstrategicoDbContext _context;

        public CadenaValorService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<CadenaValorDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.CadenasValor.Where(x => x.IdEmpresa == empresaId).ToListAsync();
            return items.Select(MapToDto);
        }

        public async Task<CadenaValorDto> CreateAsync(CadenaValorCreateDto dto)
        {
            var item = new CadenaValor
            {
                IdEmpresa = dto.IdEmpresa,
                TipoActividad = dto.TipoActividad,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Responsable = dto.Responsable
            };
            _context.CadenasValor.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, CadenaValorCreateDto dto)
        {
            var item = await _context.CadenasValor.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Cadena de valor no encontrada");

            item.TipoActividad = dto.TipoActividad;
            item.Nombre = dto.Nombre;
            item.Descripcion = dto.Descripcion;
            item.Responsable = dto.Responsable;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.CadenasValor.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Cadena de valor no encontrada");
            _context.CadenasValor.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static CadenaValorDto MapToDto(CadenaValor x) => new()
        {
            IdCadenaValor = x.IdCadenaValor,
            IdEmpresa = x.IdEmpresa,
            TipoActividad = x.TipoActividad,
            Nombre = x.Nombre,
            Descripcion = x.Descripcion,
            Responsable = x.Responsable,
            FechaRegistro = x.FechaRegistro
        };
    }

    public interface IAutoCadenaValorService
    {
        Task<IEnumerable<AutoCadenaValorDto>> GetByEmpresaAsync(int empresaId);
        Task<AutoCadenaValorDto> CreateAsync(AutoCadenaValorCreateDto dto);
        Task UpdateAsync(int id, AutoCadenaValorCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class AutoCadenaValorService : IAutoCadenaValorService
    {
        private readonly PlanEstrategicoDbContext _context;

        public AutoCadenaValorService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<AutoCadenaValorDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.AutoCadenasValor.Where(x => x.IdEmpresa == empresaId).ToListAsync();
            return items.Select(MapToDto);
        }

        public async Task<AutoCadenaValorDto> CreateAsync(AutoCadenaValorCreateDto dto)
        {
            var item = new AutoCadenaValor
            {
                IdEmpresa = dto.IdEmpresa,
                Area = dto.Area,
                Puntaje = dto.Puntaje,
                Observacion = dto.Observacion
            };

            _context.AutoCadenasValor.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, AutoCadenaValorCreateDto dto)
        {
            var item = await _context.AutoCadenasValor.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Auto diagnóstico de cadena no encontrado");

            item.Area = dto.Area;
            item.Puntaje = dto.Puntaje;
            item.Observacion = dto.Observacion;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.AutoCadenasValor.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Auto diagnóstico de cadena no encontrado");
            _context.AutoCadenasValor.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static AutoCadenaValorDto MapToDto(AutoCadenaValor x) => new()
        {
            IdAutoCadena = x.IdAutoCadena,
            IdEmpresa = x.IdEmpresa,
            Area = x.Area,
            Puntaje = x.Puntaje,
            Observacion = x.Observacion,
            FechaRegistro = x.FechaRegistro
        };
    }

    public interface IAutoBCGService
    {
        Task<IEnumerable<AutoBCGDto>> GetByEmpresaAsync(int empresaId);
        Task<AutoBCGDto> CreateAsync(AutoBCGCreateDto dto);
        Task UpdateAsync(int id, AutoBCGCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class AutoBCGService : IAutoBCGService
    {
        private readonly PlanEstrategicoDbContext _context;

        public AutoBCGService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<AutoBCGDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.AutoBCGs.Where(x => x.IdEmpresa == empresaId).ToListAsync();
            return items.Select(MapToDto);
        }

        public async Task<AutoBCGDto> CreateAsync(AutoBCGCreateDto dto)
        {
            var item = new AutoBCG
            {
                IdEmpresa = dto.IdEmpresa,
                Evaluacion = dto.Evaluacion,
                Puntaje = dto.Puntaje,
                Comentario = dto.Comentario
            };

            _context.AutoBCGs.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, AutoBCGCreateDto dto)
        {
            var item = await _context.AutoBCGs.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Auto BCG no encontrado");

            item.Evaluacion = dto.Evaluacion;
            item.Puntaje = dto.Puntaje;
            item.Comentario = dto.Comentario;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.AutoBCGs.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Auto BCG no encontrado");
            _context.AutoBCGs.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static AutoBCGDto MapToDto(AutoBCG x) => new()
        {
            IdAutoBCG = x.IdAutoBCG,
            IdEmpresa = x.IdEmpresa,
            Evaluacion = x.Evaluacion,
            Puntaje = x.Puntaje,
            Comentario = x.Comentario,
            FechaRegistro = x.FechaRegistro
        };
    }

    public interface IAutoPorterService
    {
        Task<IEnumerable<AutoPorterDto>> GetByEmpresaAsync(int empresaId);
        Task<AutoPorterDto> CreateAsync(AutoPorterCreateDto dto);
        Task UpdateAsync(int id, AutoPorterCreateDto dto);
        Task DeleteAsync(int id);
    }

    public class AutoPorterService : IAutoPorterService
    {
        private readonly PlanEstrategicoDbContext _context;

        public AutoPorterService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<IEnumerable<AutoPorterDto>> GetByEmpresaAsync(int empresaId)
        {
            var items = await _context.AutoPorters.Where(x => x.IdEmpresa == empresaId).ToListAsync();
            return items.Select(MapToDto);
        }

        public async Task<AutoPorterDto> CreateAsync(AutoPorterCreateDto dto)
        {
            var item = new AutoPorter
            {
                IdEmpresa = dto.IdEmpresa,
                Evaluacion = dto.Evaluacion,
                Resultado = dto.Resultado,
                Observacion = dto.Observacion
            };

            _context.AutoPorters.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task UpdateAsync(int id, AutoPorterCreateDto dto)
        {
            var item = await _context.AutoPorters.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Auto Porter no encontrado");

            item.Evaluacion = dto.Evaluacion;
            item.Resultado = dto.Resultado;
            item.Observacion = dto.Observacion;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.AutoPorters.FindAsync(id);
            if (item == null) throw new InvalidOperationException("Auto Porter no encontrado");
            _context.AutoPorters.Remove(item);
            await _context.SaveChangesAsync();
        }

        private static AutoPorterDto MapToDto(AutoPorter x) => new()
        {
            IdAutoPorter = x.IdAutoPorter,
            IdEmpresa = x.IdEmpresa,
            Evaluacion = x.Evaluacion,
            Resultado = x.Resultado,
            Observacion = x.Observacion,
            FechaRegistro = x.FechaRegistro
        };
    }

    public interface IResumenEjecutivoService
    {
        Task<ResumenEjecutivoDto?> GetByEmpresaAsync(int empresaId);
        Task<ResumenEjecutivoDto> GenerarAsync(int empresaId);
    }

    public class ResumenEjecutivoService : IResumenEjecutivoService
    {
        private readonly PlanEstrategicoDbContext _context;

        public ResumenEjecutivoService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<ResumenEjecutivoDto?> GetByEmpresaAsync(int empresaId)
        {
            var item = await _context.ResumenesEjecutivos
                .Where(x => x.IdEmpresa == empresaId && x.Activo)
                .OrderByDescending(x => x.FechaGeneracion)
                .FirstOrDefaultAsync();

            if (item == null) return null;

            return new ResumenEjecutivoDto
            {
                IdResumen = item.IdResumen,
                IdEmpresa = item.IdEmpresa,
                Contenido = item.Contenido,
                FechaGeneracion = item.FechaGeneracion
            };
        }

        public async Task<ResumenEjecutivoDto> GenerarAsync(int empresaId)
        {
            var empresa = await _context.Empresas.FindAsync(empresaId)
                ?? throw new InvalidOperationException("Empresa no encontrada");

            var objetivos = await _context.ObjetivosEstrategicos.CountAsync(x => x.IdEmpresa == empresaId);
            var estrategias = await _context.EstrategiasIdentificacion.CountAsync(x => x.IdEmpresa == empresaId);
            var foda = await _context.AnalisisFODAs.Where(x => x.IdEmpresa == empresaId).ToListAsync();

            var resumen = new StringBuilder();
            resumen.AppendLine($"Empresa: {empresa.Nombre}");
            resumen.AppendLine($"Objetivos registrados: {objetivos}");
            resumen.AppendLine($"Estrategias registradas: {estrategias}");
            resumen.AppendLine($"FODA total: {foda.Count}");
            resumen.AppendLine("Conclusión: El plan estratégico se encuentra en proceso de consolidación con base en la información registrada.");

            var item = new ResumenEjecutivo
            {
                IdEmpresa = empresaId,
                Contenido = resumen.ToString(),
                FechaGeneracion = DateTime.UtcNow,
                Activo = true
            };

            _context.ResumenesEjecutivos.Add(item);
            await _context.SaveChangesAsync();

            return new ResumenEjecutivoDto
            {
                IdResumen = item.IdResumen,
                IdEmpresa = item.IdEmpresa,
                Contenido = item.Contenido,
                FechaGeneracion = item.FechaGeneracion
            };
        }
    }

    public interface IReporteFinalService
    {
        Task<byte[]> GenerarPdfAsync(int empresaId);
        Task<byte[]> GenerarExcelAsync(int empresaId);
        Task<ReporteFinalDto> RegistrarAsync(int empresaId, string formato, byte[] archivo);
    }

    public class ReporteFinalService : IReporteFinalService
    {
        private readonly PlanEstrategicoDbContext _context;
        private readonly IResumenEjecutivoService _resumenService;

        public ReporteFinalService(PlanEstrategicoDbContext context, IResumenEjecutivoService resumenService)
        {
            _context = context;
            _resumenService = resumenService;
        }

        public async Task<byte[]> GenerarPdfAsync(int empresaId)
        {
            var empresa = await _context.Empresas.FindAsync(empresaId)
                ?? throw new InvalidOperationException("Empresa no encontrada");

            var resumen = await _resumenService.GenerarAsync(empresaId);
            using var ms = new MemoryStream();
            using (var doc = new Document(PageSize.A4))
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();
                doc.Add(new Paragraph("Reporte Final - Plan Estratégico de TI") { SpacingAfter = 10f });
                doc.Add(new Paragraph($"Empresa: {empresa.Nombre}"));
                doc.Add(new Paragraph($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm}"));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(resumen.Contenido));
                doc.Close();
            }
            return ms.ToArray();
        }

        public async Task<byte[]> GenerarExcelAsync(int empresaId)
        {
            var empresa = await _context.Empresas.FindAsync(empresaId)
                ?? throw new InvalidOperationException("Empresa no encontrada");

            var objetivos = await _context.ObjetivosEstrategicos.Where(x => x.IdEmpresa == empresaId).ToListAsync();
            var foda = await _context.AnalisisFODAs.Where(x => x.IdEmpresa == empresaId).ToListAsync();

            using var workbook = new XLWorkbook();
            var resumenSheet = workbook.Worksheets.Add("Resumen");
            resumenSheet.Cell(1, 1).Value = "Empresa";
            resumenSheet.Cell(1, 2).Value = empresa.Nombre;
            resumenSheet.Cell(2, 1).Value = "Fecha";
            resumenSheet.Cell(2, 2).Value = DateTime.Now;

            var objetivosSheet = workbook.Worksheets.Add("Objetivos");
            objetivosSheet.Cell(1, 1).Value = "Objetivo";
            objetivosSheet.Cell(1, 2).Value = "UEN";
            objetivosSheet.Cell(1, 3).Value = "Indicador";
            objetivosSheet.Cell(1, 4).Value = "Meta";

            for (var i = 0; i < objetivos.Count; i++)
            {
                objetivosSheet.Cell(i + 2, 1).Value = objetivos[i].Objetivo;
                objetivosSheet.Cell(i + 2, 2).Value = objetivos[i].UEN;
                objetivosSheet.Cell(i + 2, 3).Value = objetivos[i].Indicador;
                objetivosSheet.Cell(i + 2, 4).Value = objetivos[i].Meta;
            }

            var fodaSheet = workbook.Worksheets.Add("FODA");
            fodaSheet.Cell(1, 1).Value = "Tipo";
            fodaSheet.Cell(1, 2).Value = "Descripcion";
            fodaSheet.Cell(1, 3).Value = "Ponderacion";

            for (var i = 0; i < foda.Count; i++)
            {
                fodaSheet.Cell(i + 2, 1).Value = foda[i].Tipo;
                fodaSheet.Cell(i + 2, 2).Value = foda[i].Descripcion;
                fodaSheet.Cell(i + 2, 3).Value = (double)foda[i].Ponderacion;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<ReporteFinalDto> RegistrarAsync(int empresaId, string formato, byte[] archivo)
        {
            var item = new ReporteFinal
            {
                IdEmpresa = empresaId,
                Contenido = "Reporte generado automáticamente",
                Formato = formato,
                FechaGeneracion = DateTime.UtcNow,
                Archivo = archivo,
                NombreArchivo = $"reporte_plan_{empresaId}_{DateTime.UtcNow:yyyyMMddHHmmss}.{formato.ToLower()}"
            };

            _context.ReportesFinal.Add(item);
            await _context.SaveChangesAsync();

            return new ReporteFinalDto
            {
                IdReporte = item.IdReporte,
                IdEmpresa = item.IdEmpresa,
                Contenido = item.Contenido,
                Formato = item.Formato,
                FechaGeneracion = item.FechaGeneracion
            };
        }
    }

    public interface IDashboardService
    {
        Task<DashboardDto> GetGeneralAsync();
    }

    public class DashboardService : IDashboardService
    {
        private readonly PlanEstrategicoDbContext _context;

        public DashboardService(PlanEstrategicoDbContext context) => _context = context;

        public async Task<DashboardDto> GetGeneralAsync()
        {
            var totalEmpresas = await _context.Empresas.CountAsync(x => x.Activo);
            var totalEstrategias = await _context.EstrategiasIdentificacion.CountAsync();
            var totalObjetivos = await _context.ObjetivosEstrategicos.CountAsync();

            var conteoFoda = await _context.AnalisisFODAs
                .GroupBy(x => x.Tipo)
                .Select(g => new { Tipo = g.Key, Total = g.Count() })
                .ToListAsync();

            var bcg = await _context.MatricesBCG
                .GroupBy(x => x.Clasificacion)
                .Select(g => new { Tipo = g.Key, Total = g.Count() })
                .ToListAsync();

            var porter = await _context.AnalisisPorters
                .OrderByDescending(x => x.FechaRegistro)
                .Take(5)
                .Select(x => new AnalisisPorterDto
                {
                    IdPorter = x.IdPorter,
                    IdEmpresa = x.IdEmpresa,
                    Fuerza = x.Fuerza,
                    Descripcion = x.Descripcion,
                    Puntaje = x.Puntaje,
                    FechaRegistro = x.FechaRegistro
                }).ToListAsync();

            return new DashboardDto
            {
                TotalEmpresas = totalEmpresas,
                TotalEstrategias = totalEstrategias,
                TotalObjetivos = totalObjetivos,
                ConteoFODA = conteoFoda.ToDictionary(x => x.Tipo, x => x.Total),
                ClasificacionBCG = bcg.ToDictionary(x => x.Tipo, x => x.Total),
                AnalisisPorter = porter,
                AvancePromedio = totalObjetivos == 0 ? 0 : Math.Min(100, (decimal)totalEstrategias / totalObjetivos * 100)
            };
        }
    }
}
