using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    public class HorarioProfessorController : Controller
    {

        private readonly Context _context;

        public HorarioProfessorController(Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateHorarioProfessor()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateHorario(HorarioProfessor hp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateHorarioProfessor));
            }
            return View(hp);
        }

        [Authorize]
        public FileResult gerarHorario(HorarioProfessor horarioProfessor)
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                page.Size = PdfSharpCore.PageSize.A4;
                page.TrimMargins.Right = 50;
                page.TrimMargins.Left = 50;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;

                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
                var corFontelerta = PdfSharpCore.Drawing.XBrushes.Red;
                var textFomatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteOrganizacao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteDesricao = new PdfSharpCore.Drawing.XFont("Calibri", 14);
                var fonteTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 17, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteSubTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 15, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteRodape = new PdfSharpCore.Drawing.XFont("Calibri", 7);
                var fonteDesricaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 14, XFontStyle.Bold);

                var brasao = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\BrasaoEstado.png";
                var escudo = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\Escudo.jpg";

                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);

                //Usuário
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                //Cabeçalho
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);
                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //Início
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("HORÁRIO DE PROFESSOR(A)", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 234, page.Width, 100, 10, 10);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Dados do servidor(a)", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 235, page.Width, page.Height));
                textFomatter.DrawString("Nome: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 250, page.Width, page.Height));
                textFomatter.DrawString(horarioProfessor.Nome, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(50, 250, page.Width, page.Height));
                textFomatter.DrawString("CPF: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 265, page.Width, page.Height));
                textFomatter.DrawString(horarioProfessor.CPF, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(35, 265, page.Width, page.Height));
                textFomatter.DrawString("Cargo: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 280, page.Width, page.Height));
                textFomatter.DrawString(horarioProfessor.Cargo, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(50, 280, page.Width, page.Height));
                textFomatter.DrawString("Disciplina: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 295, page.Width, page.Height));
                textFomatter.DrawString(horarioProfessor.Disciplina, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(70, 295, page.Width, page.Height));
                textFomatter.DrawString("Carga horária semanal: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 310, page.Width, page.Height));
                textFomatter.DrawString(horarioProfessor.CargaHorariaSemanal + " horas/aula", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(145, 310, page.Width, page.Height));
                //Tabela de horários
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 370, page.Width, 130, 10, 10);
                textFomatter.DrawString("Turno Matutino", fonteSubTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 370, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 375, page.Width, page.Height));
                //Título das colunas manhã
                var detalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                detalhes.DrawString("Segunda-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(40, 390, page.Width, page.Height));
                detalhes.DrawString("Terça-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(150, 390, page.Width, page.Height));
                detalhes.DrawString("Quarta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(250, 390, page.Width, page.Height));
                detalhes.DrawString("Quinta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(370, 390, page.Width, page.Height));
                detalhes.DrawString("Sexta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(500, 390, page.Width, page.Height));
                //Horários manhã
                detalhes.DrawString("1º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 410, page.Width, page.Height));
                detalhes.DrawString("2º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 425, page.Width, page.Height));
                detalhes.DrawString("3º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 440, page.Width, page.Height));
                detalhes.DrawString("4º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 455, page.Width, page.Height));
                detalhes.DrawString("5º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 470, page.Width, page.Height));
                //Título das colunas tarde
                detalhes.DrawString("Segunda-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(40, 530, page.Width, page.Height));
                detalhes.DrawString("Terça-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(150, 530, page.Width, page.Height));
                detalhes.DrawString("Quarta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(250, 530, page.Width, page.Height));
                detalhes.DrawString("Quinta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(370, 530, page.Width, page.Height));
                detalhes.DrawString("Sexta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(500, 530, page.Width, page.Height));
                //Horários tarde
                detalhes.DrawString("1º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 550, page.Width, page.Height));
                detalhes.DrawString("2º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 565, page.Width, page.Height));
                detalhes.DrawString("3º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 580, page.Width, page.Height));
                detalhes.DrawString("4º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 595, page.Width, page.Height));
                detalhes.DrawString("5º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 610, page.Width, page.Height));
                //Horario da segunda
                var qtdHoras = 0;
                if (horarioProfessor.s01 == null)
                {
                    horarioProfessor.s01 = "sem aulas";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.s01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 410, page.Width, page.Height));
                }

                if (horarioProfessor.s02 == null)
                {
                    horarioProfessor.s02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.s02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 425, page.Width, page.Height));
                }

                if (horarioProfessor.s03 == null)
                {
                    horarioProfessor.s03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.s03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 440, page.Width, page.Height));
                }

                if (horarioProfessor.s04 == null)
                {
                    horarioProfessor.s04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.s02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 455, page.Width, page.Height));
                }

                if (horarioProfessor.s05 == null)
                {
                    horarioProfessor.s05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.s05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 470, page.Width, page.Height));
                }

                //Horario terça
                if (horarioProfessor.t01 == null)
                {
                    horarioProfessor.t01 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.t01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 410, page.Width, page.Height));
                }

                if (horarioProfessor.t02 == null)
                {
                    horarioProfessor.t02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.t02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 425, page.Width, page.Height));
                }

                if (horarioProfessor.t03 == null)
                {
                    horarioProfessor.t03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.t03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 440, page.Width, page.Height));
                }

                if (horarioProfessor.t04 == null)
                {
                    horarioProfessor.t04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.t04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 455, page.Width, page.Height));
                }
                if (horarioProfessor.t05 == null)
                {
                    horarioProfessor.t05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioProfessor.t05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 470, page.Width, page.Height));
                }


                if (qtdHoras < horarioProfessor.CargaHorariaSemanal)
                {
                    detalhes.DrawString("Total de aulas na semana: " + qtdHoras + " aulas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 485, page.Width, page.Height));
                    textFomatter.DrawString("Total de aulas inferior à carga horária semanal! ", fonteDetalhesDescricao, corFontelerta, new PdfSharpCore.Drawing.XRect(200, 485, page.Width, page.Height));
                }
                else
                {
                    detalhes.DrawString("Total de aulas na semana: " + qtdHoras + " aulas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 485, page.Width, page.Height));
                }

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 510, page.Width, 130, 10, 10);
                textFomatter.DrawString("Turno Vespertino", fonteSubTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 510, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 515, page.Width, page.Height));



                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                textFomatter.DrawString("Professor(a)", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 680, page.Width, page.Height));
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 720, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));



                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString();
                textFomatter.DrawString("Horário emitido em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Este horário foi emitido através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));



                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Horário " + horarioProfessor.Nome + ".pdf";
                    //Salvando no banco
                    _context.Add(horarioProfessor);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

    }
}
