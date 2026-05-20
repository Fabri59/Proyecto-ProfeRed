using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Red.Interfaces;
using Proyecto_Red.Models;

namespace Proyecto_Red.Controllers
{
    [Authorize(Roles = "Administrador,Analista")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        public async Task<IActionResult> Index()
        {
            var empresas = await _empresaService.GetAllAsync();
            return View(empresas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            await _empresaService.CreateAsync(empresa);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa is null) return NotFound();
            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empresa empresa)
        {
            if (id != empresa.Id) return BadRequest();
            if (!ModelState.IsValid) return View(empresa);

            await _empresaService.UpdateAsync(empresa);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            return empresa is null ? NotFound() : View(empresa);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            return empresa is null ? NotFound() : View(empresa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _empresaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
