using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class AlunoController : Controller
    {
        private readonly Context _contexto;

        public AlunoController(Context context)
        {
            _contexto = context;
        }

        [Authorize(Roles = "usuario, administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Alunos.ToListAsync());
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(aluno student)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(student);
                await _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Aluno cadastrado com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            else

                return View(student);

        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult UpdateStudent(int? id)
        {
            if (id != null)
            {
                aluno student = _contexto.Alunos.Find(id);
                return View(student);
            }
            else
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(int? id, aluno student)
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
        
        [Authorize(Roles ="administrador")]
        [HttpGet]
        public IActionResult DeleteStudent(int? id)
        {
            if (id != null)
            {
                aluno student = _contexto.Alunos.Find(id);
                return View(student);
            }
            else
                return View();
        }
        
        [Authorize(Roles = "administrador")]
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int? id, aluno student)
        {
            if (id != null)
            {
                _contexto.Remove(student);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound();
        }
    }
}
