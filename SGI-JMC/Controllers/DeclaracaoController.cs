using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Models;

namespace SGI_JMC.Controllers
{
    public class DeclaracaoController : Controller
    {
        private readonly Context _context;

        public DeclaracaoController(Context context)
        {
            _context = context;
        }

        // GET: Declaracao
        public async Task<IActionResult> Index()
        {
            return View(await _context.Declaracao.ToListAsync());
        }

        // GET: Declaracao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var declaracao = await _context.Declaracao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (declaracao == null)
            {
                return NotFound();
            }

            return View(declaracao);
        }

        // GET: Declaracao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Declaracao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Father_name,Mother_name,Birth_date,anoLetivo,anoSerie,turma,numeroDoNis,codigoSeed,dataDeEmissao")] Declaracao declaracao)
        {
            if (ModelState.IsValid)
            {
                declaracao.dataDeEmissao = DateTime.Now;
                _context.Add(declaracao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracao);
        }

        // GET: Declaracao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var declaracao = await _context.Declaracao.FindAsync(id);
            if (declaracao == null)
            {
                return NotFound();
            }
            return View(declaracao);
        }

        // POST: Declaracao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Father_name,Mother_name,Birth_date,anoLetivo,anoSerie,turma,numeroDoNis,codigoSeed,dataDeEmissao")] Declaracao declaracao)
        {
            if (id != declaracao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(declaracao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeclaracaoExists(declaracao.Id))
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
            return View(declaracao);
        }

        // GET: Declaracao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var declaracao = await _context.Declaracao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (declaracao == null)
            {
                return NotFound();
            }

            return View(declaracao);
        }

        // POST: Declaracao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var declaracao = await _context.Declaracao.FindAsync(id);
            _context.Declaracao.Remove(declaracao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeclaracaoExists(int id)
        {
            return _context.Declaracao.Any(e => e.Id == id);
        }
    }
}
