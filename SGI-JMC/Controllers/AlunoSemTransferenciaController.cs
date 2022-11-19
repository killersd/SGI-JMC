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
    [Authorize(Roles = "usuario, administrador")]
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

        [Authorize(Roles = "usuario, administrador")]
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

        [Authorize(Roles = "administrador")]
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
                    this.MostrarMensagem($"Pendência removida com sucesso!");
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

        [Authorize(Roles = "administrador")]
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
                    this.MostrarMensagem($"Pendência adicionada com sucesso!");
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

        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> DeletarPendencia(int? id, AlunoSemTransferencia student)
        {
            if (id != null)
            {
                _contexto.Remove(student);
                await _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Registro removido com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound();
        }



        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id != null)
            {
                AlunoSemTransferencia student = _contexto.AlunoSemTransferencia.Find(id);
                return View(student);
            }
            else
                return View();
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public async Task<IActionResult> Update(int? id, AlunoSemTransferencia student)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(student);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(student);
                }

            }
            else

                return NotFound();
        }

    }
}
