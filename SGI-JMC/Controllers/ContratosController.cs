using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using SGI_JMC.ViewModels;
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
        public IActionResult AdicionarGet()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(Contratos contratado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Add(contratado);
                    await _contexto.SaveChangesAsync();
                    this.MostrarMensagem($"Servidor cadastrado com sucesso!");
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception)
                {

                    TempData["mensagem"] = MensagemViewModel.Serializar("Erro ao cadastrar servidor!", TipoMensagem.Erro);
                    return RedirectToAction(nameof(AdicionarGet));
                }
            }
            else
            {
                TempData["mensagem"] = MensagemViewModel.Serializar("Erro ao cadastrar servidor!", TipoMensagem.Erro);
                return RedirectToAction(nameof(AdicionarGet));
            }
        }


        public IActionResult EditarContratado(int? id)
        {
            if (id != null)
            {
                Contratos contratado = _contexto.Contratos.Find(id);
                ViewBag.servidor = contratado;
                return View(contratado);
            }
            else
                return View();
        }

        public async Task<IActionResult> RenovarContrato(int? id, Contratos contratado)
        {
            if (id != null)
            {
                contratado = _contexto.Contratos.Find(id);
                contratado.renovado = true;
                await _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Renovação confirmada!");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["mensagem"] = MensagemViewModel.Serializar("Erro ao informar renovação!", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> DesfazerRenovacao(int? id, Contratos contratado)
        {
            if (id != null)
            {
                contratado = _contexto.Contratos.Find(id);
                contratado.renovado = false;
                await _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Renovação removida!");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["mensagem"] = MensagemViewModel.Serializar("Erro ao remover renovação!", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditarContratado(int? id, Contratos contratado)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(contratado);
                    await _contexto.SaveChangesAsync();
                    this.MostrarMensagem($"Servidor editado com sucesso!");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["mensagem"] = MensagemViewModel.Serializar("Erro ao editar servidor!", TipoMensagem.Erro);
                    return View(contratado);
                }
            }
            else
                return NotFound();
        }

        //APAGAR
        [HttpGet]
        public IActionResult ExcluirServidorContratado(int? id)
        {
            if (id != null)
            {
                Contratos contratado = _contexto.Contratos.Find(id);
                return View(contratado);
            }
            else
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirServidorContratado(int? id, Contratos contratado)
        {
            if (id != null)
            {
                _contexto.Remove(contratado);
                await _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Servidor contratado excluido com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound();
        }
    }
}
