using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Services;

namespace PlanEstrategico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ObjetivosController : ControllerBase
    {
        private readonly IObjetivoEstrategicoService _objetivoService;

        public ObjetivosController(IObjetivoEstrategicoService objetivoService)
        {
            _objetivoService = objetivoService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var objetivos = await _objetivoService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<ObjetivoEstrategicoDto>>
                {
                    Exito = true,
                    Datos = objetivos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var objetivo = await _objetivoService.GetByIdAsync(id);
                if (objetivo == null)
                    return NotFound();

                return Ok(new ApiResponse<ObjetivoEstrategicoDto>
                {
                    Exito = true,
                    Datos = objetivo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] ObjetivoEstrategicoCreateDto dto)
        {
            try
            {
                var objetivo = await _objetivoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = objetivo.IdObjetivo }, new ApiResponse<ObjetivoEstrategicoDto>
                {
                    Exito = true,
                    Mensaje = "Objetivo creado exitosamente",
                    Datos = objetivo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] ObjetivoEstrategicoCreateDto dto)
        {
            try
            {
                await _objetivoService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Objetivo actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _objetivoService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Objetivo eliminado exitosamente"
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
    public class FODAController : ControllerBase
    {
        private readonly IAnalisisFODAService _fodaService;

        public FODAController(IAnalisisFODAService fodaService)
        {
            _fodaService = fodaService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var foda = await _fodaService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<AnalisisFODADto>>
                {
                    Exito = true,
                    Datos = foda
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpGet("empresa/{empresaId}/tipo/{tipo}")]
        public async Task<IActionResult> GetByTipo(int empresaId, string tipo)
        {
            try
            {
                var foda = await _fodaService.GetByTipoAsync(empresaId, tipo);
                return Ok(new ApiResponse<IEnumerable<AnalisisFODADto>>
                {
                    Exito = true,
                    Datos = foda
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] AnalisisFODACreateDto dto)
        {
            try
            {
                var foda = await _fodaService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = foda.IdEmpresa }, new ApiResponse<AnalisisFODADto>
                {
                    Exito = true,
                    Mensaje = "Análisis FODA creado exitosamente",
                    Datos = foda
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] AnalisisFODACreateDto dto)
        {
            try
            {
                await _fodaService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Análisis FODA actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _fodaService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Análisis FODA eliminado exitosamente"
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
    public class BCGController : ControllerBase
    {
        private readonly IMatrizBCGService _bcgService;

        public BCGController(IMatrizBCGService bcgService)
        {
            _bcgService = bcgService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var bcg = await _bcgService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<MatrizBCGDto>>
                {
                    Exito = true,
                    Datos = bcg
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] MatrizBCGCreateDto dto)
        {
            try
            {
                var bcg = await _bcgService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = bcg.IdEmpresa }, new ApiResponse<MatrizBCGDto>
                {
                    Exito = true,
                    Mensaje = "Producto BCG creado exitosamente",
                    Datos = bcg
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] MatrizBCGCreateDto dto)
        {
            try
            {
                await _bcgService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Producto BCG actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bcgService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Producto BCG eliminado exitosamente"
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
    public class PorterController : ControllerBase
    {
        private readonly IAnalisisPorterService _porterService;

        public PorterController(IAnalisisPorterService porterService)
        {
            _porterService = porterService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var porter = await _porterService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<AnalisisPorterDto>>
                {
                    Exito = true,
                    Datos = porter
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] AnalisisPorterCreateDto dto)
        {
            try
            {
                var porter = await _porterService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = porter.IdEmpresa }, new ApiResponse<AnalisisPorterDto>
                {
                    Exito = true,
                    Mensaje = "Análisis Porter creado exitosamente",
                    Datos = porter
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] AnalisisPorterCreateDto dto)
        {
            try
            {
                await _porterService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Análisis Porter actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _porterService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Análisis Porter eliminado exitosamente"
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
    public class PESTController : ControllerBase
    {
        private readonly IAnalisisPESTService _pestService;

        public PESTController(IAnalisisPESTService pestService)
        {
            _pestService = pestService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var pest = await _pestService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<AnalisisPESTDto>>
                {
                    Exito = true,
                    Datos = pest
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] AnalisisPESTCreateDto dto)
        {
            try
            {
                var pest = await _pestService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = pest.IdEmpresa }, new ApiResponse<AnalisisPESTDto>
                {
                    Exito = true,
                    Mensaje = "Análisis PEST creado exitosamente",
                    Datos = pest
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] AnalisisPESTCreateDto dto)
        {
            try
            {
                await _pestService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Análisis PEST actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pestService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Análisis PEST eliminado exitosamente"
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
    public class EstrategiasController : ControllerBase
    {
        private readonly IEstrategiaIdentificacionService _estrategiaService;

        public EstrategiasController(IEstrategiaIdentificacionService estrategiaService)
        {
            _estrategiaService = estrategiaService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var estrategias = await _estrategiaService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<EstrategiaIdentificacionDto>>
                {
                    Exito = true,
                    Datos = estrategias
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] EstrategiaIdentificacionCreateDto dto)
        {
            try
            {
                var estrategia = await _estrategiaService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = estrategia.IdEmpresa }, new ApiResponse<EstrategiaIdentificacionDto>
                {
                    Exito = true,
                    Mensaje = "Estrategia creada exitosamente",
                    Datos = estrategia
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] EstrategiaIdentificacionCreateDto dto)
        {
            try
            {
                await _estrategiaService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Estrategia actualizada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _estrategiaService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Estrategia eliminada exitosamente"
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
    public class CAMEController : ControllerBase
    {
        private readonly IMatrizCAMEService _cameService;

        public CAMEController(IMatrizCAMEService cameService)
        {
            _cameService = cameService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var came = await _cameService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<MatrizCAMEDto>>
                {
                    Exito = true,
                    Datos = came
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] MatrizCAMECreateDto dto)
        {
            try
            {
                var came = await _cameService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = came.IdEmpresa }, new ApiResponse<MatrizCAMEDto>
                {
                    Exito = true,
                    Mensaje = "Estrategia CAME creada exitosamente",
                    Datos = came
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] MatrizCAMECreateDto dto)
        {
            try
            {
                await _cameService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Estrategia CAME actualizada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cameService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Estrategia CAME eliminada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }
    }
}
