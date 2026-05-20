using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Red.Data;
using Proyecto_Red.Models;
using System.Security.Claims;

namespace Proyecto_Red.Controllers
{
    [Authorize]
    public class AutodiagnosticoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AutodiagnosticoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var preguntas = await _context.Preguntas
                .Where(p => p.Estado && p.Modulo == "Autodiagnóstico")
                .OrderBy(p => p.IdPregunta)
                .ToListAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var respuestasExistentes = await _context.Respuestas
                .Where(r => r.IdUsuario == userId)
                .ToDictionaryAsync(r => r.IdPregunta, r => r.ValorSeleccionado);

            ViewBag.RespuestasExistentes = respuestasExistentes;
            return View(preguntas);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarRespuestas(Dictionary<int, int> respuestas)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            // Validar que todas las preguntas tengan respuesta y solo una
            var preguntas = await _context.Preguntas
                .Where(p => p.Estado && p.Modulo == "Autodiagnóstico")
                .ToListAsync();

            if (respuestas.Count != preguntas.Count)
            {
                return Json(new { success = false, message = "Debe responder todas las preguntas." });
            }

            // Eliminar respuestas anteriores
            var respuestasAnteriores = await _context.Respuestas
                .Where(r => r.IdUsuario == userId && preguntas.Select(p => p.IdPregunta).Contains(r.IdPregunta))
                .ToListAsync();
            _context.Respuestas.RemoveRange(respuestasAnteriores);

            // Agregar nuevas respuestas
            foreach (var respuesta in respuestas)
            {
                _context.Respuestas.Add(new Respuesta
                {
                    IdPregunta = respuesta.Key,
                    IdUsuario = userId,
                    ValorSeleccionado = respuesta.Value
                });
            }

            // Calcular resultado
            int puntajeTotal = respuestas.Values.Sum();
            double promedio = (double)puntajeTotal / preguntas.Count;
            string nivel = promedio < 2 ? "Bajo" : promedio < 3 ? "Medio" : "Alto";

            // Guardar o actualizar resultado
            var resultadoExistente = await _context.Resultados
                .FirstOrDefaultAsync(r => r.IdUsuario == userId);
            if (resultadoExistente != null)
            {
                resultadoExistente.PuntajeTotal = puntajeTotal;
                resultadoExistente.Promedio = promedio;
                resultadoExistente.Nivel = nivel;
                resultadoExistente.Fecha = DateTime.Now;
            }
            else
            {
                _context.Resultados.Add(new Resultado
                {
                    IdUsuario = userId,
                    PuntajeTotal = puntajeTotal,
                    Promedio = promedio,
                    Nivel = nivel
                });
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true, puntajeTotal, promedio, nivel });
        }

        public async Task<IActionResult> Resultados()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resultado = await _context.Resultados
                .FirstOrDefaultAsync(r => r.IdUsuario == userId);

            if (resultado == null)
            {
                return RedirectToAction("Index");
            }

            return View(resultado);
        }
    }
}