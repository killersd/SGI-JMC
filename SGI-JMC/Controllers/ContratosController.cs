using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "administrador")]
    public class ContratosController : Controller
    {
        private readonly Context _contexto;

        public ContratosController(Context context)
        {
            _contexto = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Contratos.ToListAsync());
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(Contratos contratado)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(contratado);
                _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Servidor cadastrado com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
