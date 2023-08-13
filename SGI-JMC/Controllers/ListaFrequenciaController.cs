using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    public class ListaFrequenciaController : Controller
    {
        private readonly Context _context;
        static int tamanho = 0;
        List<AlunoAtual> alunos;

        public ListaFrequenciaController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Lista = new[]
            {
                new SelectListItem { Value = "Janeiro", Text = "Janeiro" },
                new SelectListItem { Value = "Fevereiro", Text = "Fevereiro" },
                new SelectListItem { Value = "Março", Text = "Março" },
                new SelectListItem { Value = "Abril", Text = "Abril" },
                new SelectListItem { Value = "Maio", Text = "Maio" },
                new SelectListItem { Value = "Junho", Text = "Junho" },
                new SelectListItem { Value = "Julho", Text = "Julho" },
                new SelectListItem { Value = "Agosto", Text = "Agosto" },
                new SelectListItem { Value = "Setembro", Text = "Setembro" },
                new SelectListItem { Value = "Outubro", Text = "Outubro" },
                new SelectListItem { Value = "Novembro", Text = "Novembro" },
                new SelectListItem { Value = "Dezembro", Text = "Dezembro" },
            };
            var Turmas = new[]
            {
                new SelectListItem { Value = "2º Ano U", Text = "2º Ano U" },
                new SelectListItem { Value = "3º Ano U", Text = "3º Ano U" },
                new SelectListItem { Value = "4º Ano U", Text = "4º Ano U" },
                new SelectListItem { Value = "5º Ano U", Text = "5º Ano U" },
                new SelectListItem { Value = "6º Ano A", Text = "6º Ano A" },
                new SelectListItem { Value = "6º Ano B", Text = "6º Ano B" },
                new SelectListItem { Value = "7º Ano U", Text = "7º Ano U" },
                new SelectListItem { Value = "8º Ano A", Text = "8º Ano A" },
                new SelectListItem { Value = "8º Ano B", Text = "8º Ano B" },
                new SelectListItem { Value = "9º Ano A", Text = "9º Ano A" },
                new SelectListItem { Value = "9º Ano B", Text = "9º Ano B" },
                new SelectListItem { Value = "Fase 03", Text = "Fase 03" },
                new SelectListItem { Value = "Fase 04 A", Text = "Fase 04 A" },
                new SelectListItem { Value = "Fase 04 B", Text = "Fase 04 B" },
            };
            ViewBag.Turmas = new SelectList(Turmas, "Value", "Text");
            ViewBag.Meses = new SelectList(Lista, "Value", "Text");
            //ViewBag.SextoAnoA = AlunosSextoAnoA();
            return View();
        }

        //public async Task<IActionResult> AlunosSextoAnoA()
        //{
        //    var alunos = await _context.AlunoAtual.Where(p => p.CorrecaoDeFluxo.Equals("Sim") && p.FaseProSic == 3 && p.Transferido == false).OrderBy(q => q.Nome).ToListAsync();

        //    foreach (var item in alunos)
        //    {
        //        alunos.Add(item);
        //        tamanho++;
        //    }
        //    return View(alunos);
        //}

        public async Task<FileResult> DownloadListaFrequencia(Models.ListaFrequencia listaFrequencia)
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                page.Size = PdfSharpCore.PageSize.A4;
                page.Orientation = PdfSharpCore.PageOrientation.Landscape;

                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
                var textFomatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteOrganizacao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteDesricao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 17, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Calibri", 9);
                var fonteRodape = new PdfSharpCore.Drawing.XFont("Calibri", 7);
                var fonteDesricaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, XFontStyle.Bold);
                var fonteTituloGigante = new PdfSharpCore.Drawing.XFont("Calibri", 20, PdfSharpCore.Drawing.XFontStyle.Bold);

                var brasao = Path.GetFullPath("wwwroot/Imagens/BrasaoEstado.png");
                var escudo = Path.GetFullPath("wwwroot/Imagens/Escudo.png");
                var logo = Path.GetFullPath("wwwroot/Imagens/SGI.jpg");
                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);
                XImage imgLogo = XImage.FromFile(logo);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                graphics.DrawImage(imgBrasao, 30, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 200, 130, 450, 450);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                graphics.DrawImage(imgLogo, 700, 60, 120, 50);

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 90, page.Width, page.Height));
                textFomatter.DrawString("______________________________________________________________________________________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(30, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("Frequência Mensal", fonteTituloGigante, corFonte, new PdfSharpCore.Drawing.XRect(30, 120, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("Ano: " + listaFrequencia.Ano + "          Turma: " + listaFrequencia.Turma + "          Turno: " + listaFrequencia.Turno + "          Mês: " + listaFrequencia.Mes, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(30, 150, page.Width, page.Height));

                int inicio = 300;
                int consta = 190;

                for (int i = 0; i < 32; i++)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    if (i < 31)
                    {
                        textFomatter.DrawString((i + 1).ToString(), fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(inicio + 32, 179, page.Width, page.Height));
                    }
                    graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 30, 180, inicio, 10, 0, 0);
                    inicio = inicio + 15;
                    consta = consta + 15;
                }
                switch (listaFrequencia.Turma)
                {
                    case "2º Ano U":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var segundoU = await _context.AlunoAtual.Where(p => p.anoSerie == 2 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
                        foreach (var item in segundoU)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "3º Ano U":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var terceiroU = await _context.AlunoAtual.Where(p => p.anoSerie == 3 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
                        foreach (var item in terceiroU)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "4º Ano U":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var quartoU = await _context.AlunoAtual.Where(p => p.anoSerie == 4 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
                        foreach (var item in quartoU)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "5º Ano U":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var quintoU = await _context.AlunoAtual.Where(p => p.anoSerie == 5 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
                        foreach (var item in quintoU)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "6º Ano A":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var sextoA = await _context.AlunoAtual.Where(p => p.anoSerie == 6 && p.turma.Equals('A') && p.Transferido == false).ToListAsync();
                        foreach (var item in sextoA)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "6º Ano B":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var sextoB = await _context.AlunoAtual.Where(p => p.anoSerie == 6 && p.turma.Equals('B') && p.Transferido == false).ToListAsync();
                        foreach (var item in sextoB)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "7º Ano U":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var setimoU = await _context.AlunoAtual.Where(p => p.anoSerie == 7 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
                        foreach (var item in setimoU)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "8º Ano A":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var oitavoA = await _context.AlunoAtual.Where(p => p.anoSerie == 8 && p.turma.Equals('A') && p.Transferido == false).ToListAsync();
                        foreach (var item in oitavoA)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "8º Ano B":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var oitavoB = await _context.AlunoAtual.Where(p => p.anoSerie == 8 && p.turma.Equals('B') && p.Transferido == false).ToListAsync();
                        foreach (var item in oitavoB)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "9º Ano A":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var nonoA = await _context.AlunoAtual.Where(p => p.anoSerie == 9 && p.turma.Equals('A') && p.Transferido == false).ToListAsync();
                        foreach (var item in nonoA)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "9º Ano B":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var nonoB = await _context.AlunoAtual.Where(p => p.anoSerie == 9 && p.turma.Equals('B') && p.Transferido == false).ToListAsync();
                        foreach (var item in nonoB)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "Fase 03":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var fase03 = await _context.AlunoAtual.Where(p => p.FaseProSic == 3 && p.Transferido == false).OrderBy(q => q.Nome).ToListAsync();
                        foreach (var item in fase03)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "Fase 04 A":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var fase04A = await _context.AlunoAtual.Where(p => p.FaseProSic == 4 && p.turma.Equals('A') && p.Transferido == false).OrderBy(q => q.Nome).ToListAsync();
                        foreach (var item in fase04A)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    case "Fase 04 B":
                        tamanho = 0;
                        alunos = new List<AlunoAtual>();
                        var fase04B = await _context.AlunoAtual.Where(p => p.FaseProSic == 4 && p.turma.Equals('B') && p.Transferido == false).OrderBy(q => q.Nome).ToListAsync();
                        foreach (var item in fase04B)
                        {
                            alunos.Add(item);
                            tamanho++;
                        }
                        break;
                    default:
                        break;
                }
                int x = 190;
                inicio = 300;
                for (int j = 0; j < tamanho; j++)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 30, x, inicio, 10, 0, 0);
                        inicio = inicio + 15;
                    }
                    textFomatter.DrawString((j + 1) + " - " + alunos[j].Nome, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(40, x - 1, page.Width, page.Height));

                    inicio = 300;
                    x = x + 10;

                }

                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("Lista de frequência emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 570, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("______________________________________________________________________________________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(30, 545, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(30, 560, page.Width, page.Height));
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(705, 560, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(-30, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(-30, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista de frequência " + listaFrequencia.Turma + ".pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }
    }
}
