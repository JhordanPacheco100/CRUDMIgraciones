using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migraciones.Models;
using System.Diagnostics;

namespace Migraciones.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var empleados = await context.Empleados.ToListAsync();
            return View(empleados);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Crear(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                context.Empleados.Add(empleado);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(empleado);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var empleado = await context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound();

            return View(empleado);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Empleado empleado)
        {
            if (id != empleado.Id) return NotFound();

            if (ModelState.IsValid)
            {
                context.Update(empleado);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await context.Empleados
                .FirstOrDefaultAsync(e => e.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        [HttpPost, ActionName("EliminarConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var empleado = await context.Empleados.FindAsync(id);

            if (empleado != null)
            {
                context.Empleados.Remove(empleado);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
