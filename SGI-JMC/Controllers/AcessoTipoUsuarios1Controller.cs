using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Data;
using SGI_JMC.Models;

namespace SGI_JMC.Controllers
{
    [Authorize]
    public class AcessoTipoUsuarios1Controller : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AcessoTipoUsuarios1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AcessoTipoUsuarios1
        public async Task<IActionResult> Index()
        {

            var temAcesso = await Usuario_Tem_Acesso(2, _context);

            if (!temAcesso)
            {
                return RedirectToAction("Error", "Home");
            }

            var applicationDbContext = _context.AcessoTipoUsuario.Include(a => a.TipoUsuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcessoTipoUsuarios1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario
                .Include(a => a.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }

            return View(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios1/Create
        public IActionResult Create()
        {
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario");
            return View();
        }

        // POST: AcessoTipoUsuarios1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acessoTipoUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            return View(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            return View(acessoTipoUsuario);
        }

        // POST: AcessoTipoUsuarios1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (id != acessoTipoUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acessoTipoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcessoTipoUsuarioExists(acessoTipoUsuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            return View(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario
                .Include(a => a.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }

            return View(acessoTipoUsuario);
        }

        // POST: AcessoTipoUsuarios1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            _context.AcessoTipoUsuario.Remove(acessoTipoUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcessoTipoUsuarioExists(int id)
        {
            return _context.AcessoTipoUsuario.Any(e => e.Id == id);
        }
    }
}
