using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using SGI_JMC.Data;
using SGI_JMC.Models;

namespace SGI_JMC.Controllers
{
    [Authorize]
    public class DeclaracaoController : BaseController
    {
        private readonly Context _context;
        //private readonly ApplicationDbContext _contexto;

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

        public FileResult gerarDeclaracao(Declaracao declaracao)
        {
            // Declaracao declaracao = new Declaracao();
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                page.Size = PdfSharpCore.PageSize.A4;
                page.TrimMargins.Right = 50;
                page.TrimMargins.Left = 50;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;

                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
                var textFomatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteOrganizacao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteDesricao = new PdfSharpCore.Drawing.XFont("Calibri", 12);
                var fonteTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 17, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Calibri", 10); 
                var fonteRodape = new PdfSharpCore.Drawing.XFont("Calibri", 7);

                var brasao = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\BrasaoEstado.png";
                var escudo = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\Escudo.jpg";

                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);


                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 115, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 130, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 145, page.Width, page.Height));
                textFomatter.DrawString("SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 160, page.Width, page.Height));
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 230, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = declaracao.turma.ToString();                
                string dataNascString = declaracao.Birth_date.ToString("dd/MM/yyyy");

                if (declaracao.numeroDoNis == null)
                {
                    declaracao.numeroDoNis = "Não encontrado em nossos registros!";
                }
                if (declaracao.Father_name != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracao.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracao.Mother_name.ToUpper() + " e " + declaracao.Father_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracao.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + declaracao.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e possui frequência regular até esta data.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 350, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracao.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracao.Mother_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracao.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + declaracao.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e possui frequência regular até esta data.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 350, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + declaracao.numeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 550, page.Width, page.Height));
                textFomatter.DrawString("CÓD. SIGA: " + declaracao.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 560, page.Width, page.Height));
                textFomatter.DrawString("Observação: Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 650, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 660, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString();
                textFomatter.DrawString("Declaração emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("Esta declaração foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 820, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + declaracao.Name + ".pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }
    }
}
