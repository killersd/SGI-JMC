using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System;

namespace SGI_JMC.Controllers
{
    public class DeclaracaoResidenciaController : Controller
    {

        private readonly Context _context;

        public DeclaracaoResidenciaController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateDeclaracaoResidencia()
        {
            return View();
        }
        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        public async Task<IActionResult> CreateDeclaracaoResidencia(DeclaracaoResidencia declaracaoResidencia)
        {
            if (ModelState.IsValid)
            {
                declaracaoResidencia.DataDeEmissao = DateTime.Now;
                _context.Add(declaracaoResidencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracaoResidencia);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracaoResidencia(DeclaracaoResidencia declaracaoResidencia)
        {
            declaracaoResidencia.NumeroDeclaracaoResidencia = GerarNumeroDeclaracaoResidencia(declaracaoResidencia);
            declaracaoResidencia.CodigoAutenticacaoDeclaracaoResidencia = GerarCodigoDeAutenticacaoResidencia(declaracaoResidencia);
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
                var fonteDesricaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 14, PdfSharpCore.Drawing.XFontStyle.Bold);



                var brasao = Path.GetFullPath("wwwroot/Imagens/BrasaoEstado.png");
                var escudo = Path.GetFullPath("wwwroot/Imagens/Escudo.png");
                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                declaracaoResidencia.DataDeEmissao = DateTime.Now;
                textFomatter.DrawString("Eu, " + declaracaoResidencia.Nome.ToUpper() + ", CPF " + declaracaoResidencia.CPF + ", " +
                    " declaro para os devidos fins de direito, que não possuo residência no município " +
                    "da Escola Estadual João de Mattos Carvalho, onde desempenho minhas atividades pedagógicas. " +
                    "A presente declaração é a mais lídima expressão da verdade, sob pena, inclusive, de incorrer " +
                    "na prática de crime de falsidade ideológica (art. 299 do código penal), devolução dos valores" +
                    " indevidamente recebidos e a deflagração de procedimento administrativo disciplinar.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("Simão Dias, ______ de ______ de __________, ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 445, page.Width, page.Height));
                textFomatter.DrawString("Assinatura do servidor(a)", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 460, page.Width, page.Height));

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 75, 500, 200, 150, 10, 10);
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 326, 500, 200, 150, 10, 10);

                textFomatter.DrawString("Para uso da Unidade de Ensino", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(76, 505, 200, page.Height));
                textFomatter.DrawString("Dou fé que as informações acima são verdadeiras.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(76, 525, 200, page.Height));
                textFomatter.DrawString("Data ____/____/________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(76, 570, 200, page.Height));
                textFomatter.DrawString("_______________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(76, 590, 200, page.Height));
                textFomatter.DrawString("Diretor(a)", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(76, 610, 200, page.Height));
                textFomatter.DrawString("carimbo", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(76, 625, 200, page.Height));


                textFomatter.DrawString("Para uso da Diretoria Regional", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(327, 505, 200, page.Height));
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 335, 543, 20, 20, 10, 10);
                textFomatter.DrawString("Nada a opor", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(327, 545, 140, page.Height));

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 445, 543, 20, 20, 10, 10);
                textFomatter.DrawString("Discordo", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(425, 545, 140, page.Height));

                textFomatter.DrawString("Data ____/____/________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(327, 570, 200, page.Height));
                textFomatter.DrawString("_______________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(327, 590, 200, page.Height));
                textFomatter.DrawString("Diretor(a)", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(327, 610, 200, page.Height));
                textFomatter.DrawString("carimbo", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(327, 625, 200, page.Height));

                textFomatter.DrawString("Número do documento: " + declaracaoResidencia.NumeroDeclaracaoResidencia, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 680, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoResidencia.CodigoAutenticacaoDeclaracaoResidencia, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 693, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://killersd.bsite.net/Declaracao/VerificarAutenticidade, preencha os dados " +
                    "\"Número do documento\" e \"Código de verificação\" com os códigos acima depois clique no botão \"Verificar autenticidade\" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 710, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                textFomatter.DrawString("Declaração emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta declaração foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração de residência " + declaracaoResidencia.Nome + ".pdf";
                    //Salvando no banco
                    declaracaoResidencia.DataDeEmissao = DateTime.Now;
                    _context.Add(declaracaoResidencia);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoResidencia(DeclaracaoResidencia declaracaoResidencia)
        {
            int numeroDeclaracaoResidenciaGerado;

            Random random = new Random();
            numeroDeclaracaoResidenciaGerado = random.Next().GetHashCode();
            declaracaoResidencia.NumeroDeclaracaoResidencia = numeroDeclaracaoResidenciaGerado;
            return numeroDeclaracaoResidenciaGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoResidencia(DeclaracaoResidencia declaracaoResidencia)
        {
            string codigoAutenticacaoResidencia = GerarNumeroDeclaracaoResidencia(declaracaoResidencia).ToString("x");
            declaracaoResidencia.CodigoAutenticacaoDeclaracaoResidencia = codigoAutenticacaoResidencia;
            return codigoAutenticacaoResidencia;
        }


    }
}
