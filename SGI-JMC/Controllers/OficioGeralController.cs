using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class OficioGeralController : Controller 
    {
        private readonly Context _context;

        public OficioGeralController(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "administrador")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.OficioGeral.ToListAsync());
        }

        [Authorize(Roles = "administrador")]
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                OficioGeral oficio = _context.OficioGeral.Find(id);
                return View(oficio);
            }
            else
                return NotFound();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateOficioGeral()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarOficio(OficioGeral oficioGeral)
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

                var brasao = Path.GetFullPath("wwwroot/Imagens/BrasaoEstado.png");
                var escudo = Path.GetFullPath("wwwroot/Imagens/Escudo.png");
                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);
                oficioGeral.DataEmissao = DateTime.Now;

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
                textFomatter.DrawString("Ofício nº 00" + oficioGeral.NumeroOficio + "/2022", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString("Assunto: " + oficioGeral.Assunto + ".", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 165, page.Width, page.Height));
                textFomatter.DrawString("Simão Dias - Se -  " + oficioGeral.DataEmissao.ToString("dd/MM/yyyy"), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));
                //Corpo do ofício
                textFomatter.DrawString("Senhor(a) "+oficioGeral.destinatario+",", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 235, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString(oficioGeral.CorpoDoOficio, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 275, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 625, page.Width, page.Height));
                if (oficioGeral.Remetente.Equals("Queila"))
                {
                    textFomatter.DrawString("Queilanc Borges Batista de Souza", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));
                    textFomatter.DrawString("Diretora - Port. 7469/2019", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 650, page.Width, page.Height));
                }
                else {
                    textFomatter.DrawString("Alex de Oliveira Souza", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));
                    textFomatter.DrawString("Secretário - Port. 7083/2019", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 650, page.Width, page.Height));
                }
                //Destinatário
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Illmº Senhor(a),", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                textFomatter.DrawString(oficioGeral.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                textFomatter.DrawString(oficioGeral.CargoDoDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                textFomatter.DrawString(oficioGeral.Cidade, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));
                //Rodapé
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
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
                    var nomeArquivo = "Ofício nº" + oficioGeral.NumeroOficio + ".pdf";
                    //Salvando no banco
                    oficioGeral.DataEmissao = DateTime.Now;
                    _context.Add(oficioGeral);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }
    }
}
