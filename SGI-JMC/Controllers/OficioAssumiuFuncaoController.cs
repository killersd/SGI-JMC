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
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using SGI_JMC.ViewModels;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class OficioAssumiuFuncaoController : Controller
    {
        private readonly Context _context;

        public OficioAssumiuFuncaoController(Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateOficio()
        {
            return View();
        }

        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateOficio([Bind("Id,Name, NumeroOficio, Assunto, destinatario, DataAssumiuFuncao, CPF, vinculo, CargaHoraria,disciplina, DataEmissao")] OficioAssumiuFuncao oficioAssumiuFuncao)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        oficioAssumiuFuncao.DataEmissao = DateTime.Now;
        //        _context.Add(oficioAssumiuFuncao);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(oficioAssumiuFuncao);
        //}

        [Authorize]
        public FileResult gerarOficio(OficioAssumiuFuncao oficioAssumiuFuncao)
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
                var textFomatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteOrganizacao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteDesricao = new PdfSharpCore.Drawing.XFont("Calibri", 14);
                var fonteTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 17, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteRodape = new PdfSharpCore.Drawing.XFont("Calibri", 7);
                var fonteDesricaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 14, XFontStyle.Bold);
                
                var brasao = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\BrasaoEstado.png";
                var escudo = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\Escudo.jpg";

                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);
                oficioAssumiuFuncao.DataEmissao = DateTime.Now;

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
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Ofício nº 00" + oficioAssumiuFuncao.NumeroOficio + "/2022", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString("Assunto: " + oficioAssumiuFuncao.Assunto + ".", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 165, page.Width, page.Height));
                textFomatter.DrawString("Simão Dias - Se -  " + oficioAssumiuFuncao.DataEmissao.ToShortDateString(), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));
                //Corpo do ofício
                textFomatter.DrawString("Senhora Diretora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 280, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("Comunicamos a Vossa Senhoria que " + oficioAssumiuFuncao.Name + ", CPF " + oficioAssumiuFuncao.CPF + ", vínculo " + oficioAssumiuFuncao.vinculo + ", ocupante do Cargo de Professor de Educação Básica, assumiu suas funções em regência de classe no dia " + oficioAssumiuFuncao.DataAssumiuFuncao.ToShortDateString() + " com carga horária de " + oficioAssumiuFuncao.CargaHoraria + " horas semanais, na disciplina ," + oficioAssumiuFuncao.disciplina + " conforme horário anexo, atuando no  Ensino Fundamental FRC 13 (FRC: Fonte de Recursos do FUNDEB).", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 330, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 500, page.Width, page.Height));
                textFomatter.DrawString("Queilanc Borges Batista de Souza", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 510, page.Width, page.Height));
                textFomatter.DrawString("Diretora - Port. 7469/2019", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 525, page.Width, page.Height));
                //Destinatário
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Illma Senhora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                textFomatter.DrawString(oficioAssumiuFuncao.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                textFomatter.DrawString("MD. Diretora Regional DRE'2,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                textFomatter.DrawString("Lagarto - Se", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));
                //Rodapé
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString();
                textFomatter.DrawString("Ofício emitido em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Este ofício foi gerado através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Ofício " + oficioAssumiuFuncao.Name + ".pdf";
                    //Salvando no banco
                    oficioAssumiuFuncao.DataEmissao = DateTime.Now;
                    _context.Add(oficioAssumiuFuncao);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }
    }
}
