using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Red.Interfaces;
using Proyecto_Red.Models;
using Proyecto_Red.ViewModels;

namespace Proyecto_Red.Controllers
{
    [Authorize(Roles = "Administrador,Analista")]
    public class PlanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var model = new PlanOverviewViewModel
            {
                PresentacionCount = (await _unitOfWork.Repository<PresentacionPlan>().GetAllAsync()).Count(),
                MisionCount = (await _unitOfWork.Repository<Mision>().GetAllAsync()).Count(),
                VisionCount = (await _unitOfWork.Repository<Vision>().GetAllAsync()).Count(),
                ValoresCount = (await _unitOfWork.Repository<ValorInstitucional>().GetAllAsync()).Count(),
                ObjetivosCount = (await _unitOfWork.Repository<ObjetivoEstrategico>().GetAllAsync()).Count(),
                UENCount = (await _unitOfWork.Repository<UEN>().GetAllAsync()).Count(),
                AnalisisInternoCount = (await _unitOfWork.Repository<AnalisisInterno>().GetAllAsync()).Count(),
                AnalisisExternoCount = (await _unitOfWork.Repository<AnalisisExterno>().GetAllAsync()).Count(),
                CadenaValorCount = (await _unitOfWork.Repository<CadenaValor>().GetAllAsync()).Count(),
                BCGCount = (await _unitOfWork.Repository<MatrizBCG>().GetAllAsync()).Count(),
                PorterCount = (await _unitOfWork.Repository<AnalisisPorter>().GetAllAsync()).Count(),
                PESTCount = (await _unitOfWork.Repository<AnalisisPEST>().GetAllAsync()).Count(),
                CAMECount = (await _unitOfWork.Repository<MatrizCAME>().GetAllAsync()).Count()
            };

            return View(model);
        }

        public async Task<IActionResult> Presentacion()
        {
            var list = await _unitOfWork.Repository<PresentacionPlan>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreatePresentacion()
        {
            return View(new PresentacionPlan());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePresentacion(PresentacionPlan model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<PresentacionPlan>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Presentacion));
        }

        public async Task<IActionResult> Mision()
        {
            var list = await _unitOfWork.Repository<Mision>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateMision()
        {
            return View(new Mision());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMision(Mision model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<Mision>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Mision));
        }

        public async Task<IActionResult> Vision()
        {
            var list = await _unitOfWork.Repository<Vision>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateVision()
        {
            return View(new Vision());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVision(Vision model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<Vision>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Vision));
        }

        public async Task<IActionResult> Valores()
        {
            var list = await _unitOfWork.Repository<ValorInstitucional>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateValor()
        {
            return View(new ValorInstitucional());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateValor(ValorInstitucional model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<ValorInstitucional>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Valores));
        }

        public async Task<IActionResult> Estrategia()
        {
            var model = new PlanEstrategiaViewModel
            {
                Misiones = await _unitOfWork.Repository<Mision>().GetAllAsync(),
                Visiones = await _unitOfWork.Repository<Vision>().GetAllAsync(),
                Valores = await _unitOfWork.Repository<ValorInstitucional>().GetAllAsync(),
                Objetivos = await _unitOfWork.Repository<ObjetivoEstrategico>().GetAllAsync(),
                UENes = await _unitOfWork.Repository<UEN>().GetAllAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> Analisis()
        {
            var model = new PlanAnalisisViewModel
            {
                AnalisisInternos = await _unitOfWork.Repository<AnalisisInterno>().GetAllAsync(),
                AnalisisExternos = await _unitOfWork.Repository<AnalisisExterno>().GetAllAsync(),
                CadenasValor = await _unitOfWork.Repository<CadenaValor>().GetAllAsync(),
                MatricesBCG = await _unitOfWork.Repository<MatrizBCG>().GetAllAsync(),
                AnalisisPorters = await _unitOfWork.Repository<AnalisisPorter>().GetAllAsync(),
                AnalisisPESTs = await _unitOfWork.Repository<AnalisisPEST>().GetAllAsync(),
                MatricesCAME = await _unitOfWork.Repository<MatrizCAME>().GetAllAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> Objetivos()
        {
            var list = await _unitOfWork.Repository<ObjetivoEstrategico>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateObjetivo()
        {
            return View(new ObjetivoEstrategico());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateObjetivo(ObjetivoEstrategico model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<ObjetivoEstrategico>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Objetivos));
        }

        public async Task<IActionResult> UEN()
        {
            var list = await _unitOfWork.Repository<UEN>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateUEN()
        {
            return View(new UEN());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUEN(UEN model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<UEN>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(UEN));
        }

        public async Task<IActionResult> AnalisisInterno()
        {
            var list = await _unitOfWork.Repository<AnalisisInterno>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateAnalisisInterno()
        {
            return View(new AnalisisInterno());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnalisisInterno(AnalisisInterno model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<AnalisisInterno>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(AnalisisInterno));
        }

        public async Task<IActionResult> AnalisisExterno()
        {
            var list = await _unitOfWork.Repository<AnalisisExterno>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateAnalisisExterno()
        {
            return View(new AnalisisExterno());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnalisisExterno(AnalisisExterno model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<AnalisisExterno>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(AnalisisExterno));
        }

        public async Task<IActionResult> CadenaValor()
        {
            var list = await _unitOfWork.Repository<CadenaValor>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateCadenaValor()
        {
            return View(new CadenaValor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCadenaValor(CadenaValor model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<CadenaValor>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(CadenaValor));
        }

        public async Task<IActionResult> MatrizBCG()
        {
            var list = await _unitOfWork.Repository<MatrizBCG>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateMatrizBCG()
        {
            return View(new MatrizBCG());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMatrizBCG(MatrizBCG model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<MatrizBCG>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(MatrizBCG));
        }

        public async Task<IActionResult> AnalisisPorter()
        {
            var list = await _unitOfWork.Repository<AnalisisPorter>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateAnalisisPorter()
        {
            return View(new AnalisisPorter());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnalisisPorter(AnalisisPorter model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<AnalisisPorter>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(AnalisisPorter));
        }

        public async Task<IActionResult> AnalisisPEST()
        {
            var list = await _unitOfWork.Repository<AnalisisPEST>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateAnalisisPEST()
        {
            return View(new AnalisisPEST());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnalisisPEST(AnalisisPEST model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<AnalisisPEST>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(AnalisisPEST));
        }

        public async Task<IActionResult> CAME()
        {
            var list = await _unitOfWork.Repository<MatrizCAME>().GetAllAsync();
            return View(list);
        }

        public IActionResult CreateCAME()
        {
            return View(new MatrizCAME());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCAME(MatrizCAME model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _unitOfWork.Repository<MatrizCAME>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(CAME));
        }

        public async Task<IActionResult> Resumen()
        {
            var model = new PlanResumenViewModel
            {
                Presentacion = (await _unitOfWork.Repository<PresentacionPlan>().GetAllAsync()).FirstOrDefault(),
                Mision = (await _unitOfWork.Repository<Mision>().GetAllAsync()).FirstOrDefault(),
                Vision = (await _unitOfWork.Repository<Vision>().GetAllAsync()).FirstOrDefault(),
                Valores = await _unitOfWork.Repository<ValorInstitucional>().GetAllAsync(),
                Objetivos = await _unitOfWork.Repository<ObjetivoEstrategico>().GetAllAsync(),
                UENes = await _unitOfWork.Repository<UEN>().GetAllAsync(),
                AnalisisInterno = (await _unitOfWork.Repository<AnalisisInterno>().GetAllAsync()).FirstOrDefault(),
                AnalisisExterno = (await _unitOfWork.Repository<AnalisisExterno>().GetAllAsync()).FirstOrDefault(),
                CadenaValor = (await _unitOfWork.Repository<CadenaValor>().GetAllAsync()).FirstOrDefault(),
                MatricesBCG = await _unitOfWork.Repository<MatrizBCG>().GetAllAsync(),
                AnalisisPorter = (await _unitOfWork.Repository<AnalisisPorter>().GetAllAsync()).FirstOrDefault(),
                AnalisisPEST = (await _unitOfWork.Repository<AnalisisPEST>().GetAllAsync()).FirstOrDefault(),
                MatrizCAME = (await _unitOfWork.Repository<MatrizCAME>().GetAllAsync()).FirstOrDefault()
            };

            return View(model);
        }
    }
}
