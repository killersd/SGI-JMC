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
    public class DeclaracaoSabadoLetivoController : Controller
    {
        private readonly Context _context;

        public DeclaracaoSabadoLetivoController(Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CPF,dataDeEmissao,anoLetivo,numeroDeclaracaoSabado,codigoAutenticacaoSabado,dataSabado")] DeclaracaoSabadoLetivo declaracaoSabadoLetivo)
        {
            if (ModelState.IsValid)
            {
                declaracaoSabadoLetivo.dataDeEmissao = DateTime.Now;
                _context.Add(declaracaoSabadoLetivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracaoSabadoLetivo);
        }

        [Authorize]
        public FileResult gerarDeclaracaoSabadoLetivo(DeclaracaoSabadoLetivo declaracaoSabadoLetivo)
        {
            declaracaoSabadoLetivo.numeroDeclaracaoSabado = GerarNumeroDeclaracaoSabadoLetivo(declaracaoSabadoLetivo);
            declaracaoSabadoLetivo.codigoAutenticacaoSabado = GerarCodigoDeAutenticacaoSabadoLetivo(declaracaoSabadoLetivo);
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


                var brasao = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\BrasaoEstado.png";
                var escudo = @"C:\Users\Alex e Grace\source\repos\SGI-JMC\SGI-JMC\wwwroot\Imagens\Escudo.jpg";

                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO DE COMPARECIMENTO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("Declaro para os devidos fins que de acordo com o calendário letivo" + declaracaoSabadoLetivo.anoLetivo + ", aprovado por orgão competente, hoje, (" + declaracaoSabadoLetivo.dataSabado.ToShortDateString() + "), foi sábado letivo nesta Unidade de Ensino. Informo ainda, que neste dia o(a) servidor(a) " + declaracaoSabadoLetivo.Name + ", CPF nº " + declaracaoSabadoLetivo.CPF + ", Professor(a) de Educação Básica, ministrou aulas nesta Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Atenciosamente, ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 370, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 445, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 460, page.Width, page.Height));
                textFomatter.DrawString("Número do documento: " + declaracaoSabadoLetivo.numeroDeclaracaoSabado, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoSabadoLetivo.codigoAutenticacaoSabado, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://localhost:44363/Declaracao/VerificarAutenticidade, preencha os dados " +
                    "\"Número do documento\" e \"Código de verificação\" com os códigos acima depois clique no botão \"Verificar autenticidade\" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString();
                textFomatter.DrawString("Declaração emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta declaração foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + declaracaoSabadoLetivo.Name + ".pdf";
                    //Salvando no banco
                    declaracaoSabadoLetivo.dataDeEmissao = DateTime.Now;
                    _context.Add(declaracaoSabadoLetivo);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize]
        public int GerarNumeroDeclaracaoSabadoLetivo(DeclaracaoSabadoLetivo declaracaoSabadoLetivo)
        {
            int numeroDeclaracaoSabadoLetivoGerado;

            Random random = new Random();

            numeroDeclaracaoSabadoLetivoGerado = random.Next().GetHashCode();
            declaracaoSabadoLetivo.numeroDeclaracaoSabado = numeroDeclaracaoSabadoLetivoGerado;
            return numeroDeclaracaoSabadoLetivoGerado;
        }

        [Authorize]
        public string GerarCodigoDeAutenticacaoSabadoLetivo(DeclaracaoSabadoLetivo declaracaoSabadoLetivo)
        {
            string codigoAutenticacaoSabadoLetivo = GerarNumeroDeclaracaoSabadoLetivo(declaracaoSabadoLetivo).ToString("x");
            declaracaoSabadoLetivo.codigoAutenticacaoSabado = codigoAutenticacaoSabadoLetivo;
            return codigoAutenticacaoSabadoLetivo;
        }

    }
}
