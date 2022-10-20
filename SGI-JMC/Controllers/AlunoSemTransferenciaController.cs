using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using System;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    public class AlunoSemTransferenciaController : Controller
    {
        private readonly Context _contexto;

        public AlunoSemTransferenciaController(Context context)
        {
            _contexto = context;
        }

        [Authorize(Roles = "usuario, administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.AlunoSemTransferencia.ToListAsync());
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlunoSemTransferencia alunoSemTransferencia)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(alunoSemTransferencia);
                await _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Aluno cadastrado com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            else

                return View(alunoSemTransferencia);
        }

        public async Task<IActionResult> RemoverPendencia(int id)
        {
            AlunoSemTransferencia alunoSemTransferencia;
            if (id != 0)
            {
                alunoSemTransferencia = _contexto.AlunoSemTransferencia.Find(id);
                if (ModelState.IsValid)
                {
                    alunoSemTransferencia.TemPendencia = 0;
                    _contexto.Update(alunoSemTransferencia);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(alunoSemTransferencia);
                }

            }
            else
                return NotFound();

        }
        public async Task<IActionResult> AdicionarPendencia(int id)
        {
            AlunoSemTransferencia alunoSemTransferencia;
            if (id != 0)
            {
                alunoSemTransferencia = _contexto.AlunoSemTransferencia.Find(id);
                if (ModelState.IsValid)
                {
                    alunoSemTransferencia.TemPendencia = 1;
                    _contexto.Update(alunoSemTransferencia);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(alunoSemTransferencia);
                }

            }
            else
                return NotFound();

        }

    }
}
