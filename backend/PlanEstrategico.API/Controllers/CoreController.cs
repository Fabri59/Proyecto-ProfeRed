using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PlanEstrategico.API.DTOs;
using PlanEstrategico.API.Services;

namespace PlanEstrategico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var empresa = await _empresaService.GetEmpresaByIdAsync(id);
                if (empresa == null)
                    return NotFound(new ApiErrorResponse { Mensaje = "Empresa no encontrada" });

                return Ok(new ApiResponse<EmpresaDto>
                {
                    Exito = true,
                    Datos = empresa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var empresas = await _empresaService.GetAllEmpresasAsync();
                return Ok(new ApiResponse<IEnumerable<EmpresaDto>>
                {
                    Exito = true,
                    Datos = empresas
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] EmpresaCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiErrorResponse { Mensaje = "Datos inválidos" });

            try
            {
                var empresa = await _empresaService.CreateEmpresaAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = empresa.IdEmpresa }, new ApiResponse<EmpresaDto>
                {
                    Exito = true,
                    Mensaje = "Empresa creada exitosamente",
                    Datos = empresa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpresaCreateDto dto)
        {
            try
            {
                await _empresaService.UpdateEmpresaAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Empresa actualizada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _empresaService.DeleteEmpresaAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Empresa eliminada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var count = await _empresaService.GetCountAsync();
                return Ok(new ApiResponse<int>
                {
                    Exito = true,
                    Datos = count
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
    public class MisionesController : ControllerBase
    {
        private readonly IMisionService _misionService;

        public MisionesController(IMisionService misionService)
        {
            _misionService = misionService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var mision = await _misionService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<MisionDto?>
                {
                    Exito = true,
                    Datos = mision
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] MisionCreateDto dto)
        {
            try
            {
                var mision = await _misionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = mision.IdEmpresa }, new ApiResponse<MisionDto>
                {
                    Exito = true,
                    Mensaje = "Misión creada exitosamente",
                    Datos = mision
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] MisionCreateDto dto)
        {
            try
            {
                await _misionService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Misión actualizada exitosamente"
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
    public class VisionesController : ControllerBase
    {
        private readonly IVisionService _visionService;

        public VisionesController(IVisionService visionService)
        {
            _visionService = visionService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var vision = await _visionService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<VisionDto?>
                {
                    Exito = true,
                    Datos = vision
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] VisionCreateDto dto)
        {
            try
            {
                var vision = await _visionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = vision.IdEmpresa }, new ApiResponse<VisionDto>
                {
                    Exito = true,
                    Mensaje = "Visión creada exitosamente",
                    Datos = vision
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] VisionCreateDto dto)
        {
            try
            {
                await _visionService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Visión actualizada exitosamente"
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
    public class ValoresController : ControllerBase
    {
        private readonly IValorService _valorService;

        public ValoresController(IValorService valorService)
        {
            _valorService = valorService;
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var valores = await _valorService.GetByEmpresaAsync(empresaId);
                return Ok(new ApiResponse<IEnumerable<ValorDto>>
                {
                    Exito = true,
                    Datos = valores
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Create([FromBody] ValorCreateDto dto)
        {
            try
            {
                var valor = await _valorService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByEmpresa), new { empresaId = valor.IdEmpresa }, new ApiResponse<ValorDto>
                {
                    Exito = true,
                    Mensaje = "Valor creado exitosamente",
                    Datos = valor
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Analista")]
        public async Task<IActionResult> Update(int id, [FromBody] ValorCreateDto dto)
        {
            try
            {
                await _valorService.UpdateAsync(id, dto);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Valor actualizado exitosamente"
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
                await _valorService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Exito = true,
                    Mensaje = "Valor eliminado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse { Mensaje = ex.Message });
            }
        }
    }
}
