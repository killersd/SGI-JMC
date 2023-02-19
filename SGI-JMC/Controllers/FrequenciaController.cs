using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System.IO;
using System;

namespace SGI_JMC.Controllers
{
    public class FrequenciaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public float CalcularFrequencia(Frequencia frequencia)
        {
            float pctFaltas;
            pctFaltas = (frequencia.QuantidadeFaltas * 100) / 1000;
            return pctFaltas;
        }


        public FileResult gerarDeclaracao(Frequencia frequencia)
        {
            float porcentagemDeFaltas = 100 - CalcularFrequencia(frequencia);

            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                page.Size = PdfSharpCore.PageSize.A6;
                page.TrimMargins.Right = 50;
                page.TrimMargins.Left = 50;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;

                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
                var textFomatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 17, PdfSharpCore.Drawing.XFontStyle.Bold);
 
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("PRESENÇA DO ALUNO: " + porcentagemDeFaltas + "%", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Frequencia.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }


    }
}
