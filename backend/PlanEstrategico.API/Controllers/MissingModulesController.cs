using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Services;

namespace PlanEstrategico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CadenaValorController : ControllerBase
    {
        private readonly ICadenaValorService _service;

        public CadenaValorController(ICadenaValorService service) => _service = service;

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            var data = await _service.GetByEmpresaAsync(empresaId);
            return Ok(new ApiResponse<IEnumerable<CadenaValorDto>> { Exito = true, Datos = data });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] CadenaValorCreateDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return Ok(new ApiResponse<CadenaValorDto> { Exito = true, Mensaje = "Cadena de valor creada", Datos = data });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] CadenaValorCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Cadena de valor actualizada" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Cadena de valor eliminada" });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AutoCadenaValorController : ControllerBase
    {
        private readonly IAutoCadenaValorService _service;

        public AutoCadenaValorController(IAutoCadenaValorService service) => _service = service;

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            var data = await _service.GetByEmpresaAsync(empresaId);
            return Ok(new ApiResponse<IEnumerable<AutoCadenaValorDto>> { Exito = true, Datos = data });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] AutoCadenaValorCreateDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return Ok(new ApiResponse<AutoCadenaValorDto> { Exito = true, Mensaje = "Autodiagnóstico cadena creado", Datos = data });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] AutoCadenaValorCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Autodiagnóstico cadena actualizado" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Autodiagnóstico cadena eliminado" });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AutoBCGController : ControllerBase
    {
        private readonly IAutoBCGService _service;

        public AutoBCGController(IAutoBCGService service) => _service = service;

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            var data = await _service.GetByEmpresaAsync(empresaId);
            return Ok(new ApiResponse<IEnumerable<AutoBCGDto>> { Exito = true, Datos = data });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] AutoBCGCreateDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return Ok(new ApiResponse<AutoBCGDto> { Exito = true, Mensaje = "Auto BCG creado", Datos = data });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] AutoBCGCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Auto BCG actualizado" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Auto BCG eliminado" });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AutoPorterController : ControllerBase
    {
        private readonly IAutoPorterService _service;

        public AutoPorterController(IAutoPorterService service) => _service = service;

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            var data = await _service.GetByEmpresaAsync(empresaId);
            return Ok(new ApiResponse<IEnumerable<AutoPorterDto>> { Exito = true, Datos = data });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] AutoPorterCreateDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return Ok(new ApiResponse<AutoPorterDto> { Exito = true, Mensaje = "Auto Porter creado", Datos = data });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] AutoPorterCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Auto Porter actualizado" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<object> { Exito = true, Mensaje = "Auto Porter eliminado" });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResumenController : ControllerBase
    {
        private readonly IResumenEjecutivoService _service;

        public ResumenController(IResumenEjecutivoService service) => _service = service;

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            var data = await _service.GetByEmpresaAsync(empresaId);
            return Ok(new ApiResponse<ResumenEjecutivoDto?> { Exito = true, Datos = data });
        }

        [HttpPost("empresa/{empresaId}/generar")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Generar(int empresaId)
        {
            var data = await _service.GenerarAsync(empresaId);
            return Ok(new ApiResponse<ResumenEjecutivoDto> { Exito = true, Mensaje = "Resumen generado", Datos = data });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteFinalService _service;

        public ReportesController(IReporteFinalService service) => _service = service;

        [HttpGet("empresa/{empresaId}/pdf")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> ExportPdf(int empresaId)
        {
            var file = await _service.GenerarPdfAsync(empresaId);
            await _service.RegistrarAsync(empresaId, "pdf", file);
            return File(file, "application/pdf", $"plan_estrategico_{empresaId}.pdf");
        }

        [HttpGet("empresa/{empresaId}/excel")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> ExportExcel(int empresaId)
        {
            var file = await _service.GenerarExcelAsync(empresaId);
            await _service.RegistrarAsync(empresaId, "xlsx", file);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"plan_estrategico_{empresaId}.xlsx");
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service) => _service = service;

        [HttpGet("general")]
        public async Task<IActionResult> GetGeneral()
        {
            var data = await _service.GetGeneralAsync();
            return Ok(new ApiResponse<DashboardDto> { Exito = true, Datos = data });
        }
    }
}
