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
    public class DeclaracaoDistanciaInteriorizacaoController : Controller
    {

        private readonly Context _context;

        public DeclaracaoDistanciaInteriorizacaoController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateDeclaracaoDistanciaInteriorizacao()
        {
            return View();
        }
        //[Authorize(Roles = "usuario, administrador")]
        //[HttpPost]
        //public async Task<IActionResult> CreateDeclaracaoDistanciaInteriorizacao(DeclaracaoDistanciaInteriorizacao declaracaoDistanciaInteriorizacao)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        declaracaoDistanciaInteriorizacao.DataDeEmissao = DateTime.Now;
        //        _context.Add(declaracaoDistanciaInteriorizacao);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(declaracaoDistanciaInteriorizacao);
        //}

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracaoDistanciaInteriorizacao(DeclaracaoDistanciaInteriorizacao declaracaoDistanciaInteriorizacao)
        {
            declaracaoDistanciaInteriorizacao.NumeroDeclaracaoDistanciaInteriorizacao = GerarNumeroDeclaracaoDistanciaInteriorizacao(declaracaoDistanciaInteriorizacao);
            declaracaoDistanciaInteriorizacao.CodigoAutenticacaoDeclaracaoInteriorizacao = GerarCodigoDeAutenticacaoDistanciaInteriorizacao(declaracaoDistanciaInteriorizacao);
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

                var brasao = Path.GetFullPath("wwwroot/Imagens/BrasaoEstado.png");
                var escudo = Path.GetFullPath("wwwroot/Imagens/Escudo.png");
                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                declaracaoDistanciaInteriorizacao.DataDeEmissao = DateTime.Now;
                textFomatter.DrawString("Declaro para os devidos fins que o(a) servidor(a) " + declaracaoDistanciaInteriorizacao.Nome + ", CPF " + declaracaoDistanciaInteriorizacao.CPF + ", " +
                    "RG nº  " + declaracaoDistanciaInteriorizacao.RG + " "+ declaracaoDistanciaInteriorizacao.OrgaoExpedidor + ", exerce a função de " +
                    "PROFESSOR(A) DE EDUCAÇÃO BÁSICA nesta Unidade de Ensino, desde o dia "+ declaracaoDistanciaInteriorizacao.InicioExercicio.ToString("dd/MM/yyyy") + " com " +
                    "carga horária semanal de " + declaracaoDistanciaInteriorizacao.CargaHorariaSemanal + " horas, conforme horário em anexo. Outrossim, informamos que a distância entre a cidade de "+ declaracaoDistanciaInteriorizacao.Origem+" - "+declaracaoDistanciaInteriorizacao.Destino+"/"+declaracaoDistanciaInteriorizacao.Destino+" - "+declaracaoDistanciaInteriorizacao.Origem+" é de "+declaracaoDistanciaInteriorizacao.Distancia+" quilômetros.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Atenciosamente, ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 370, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 445, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 460, page.Width, page.Height));
                textFomatter.DrawString("Número do documento: " + declaracaoDistanciaInteriorizacao.NumeroDeclaracaoDistanciaInteriorizacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoDistanciaInteriorizacao.CodigoAutenticacaoDeclaracaoInteriorizacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                //textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://sgi-eejmc.azurewebsites.net/Declaracao/VerificarAutenticidade, preencha os dados " +
                //    "\"Número do documento\" e \"Código de verificação\" com os códigos acima depois clique no botão \"Verificar autenticidade\" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
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
                    var nomeArquivo = "Declaração de distância" + declaracaoDistanciaInteriorizacao.Nome + ".pdf";
                    //Salvando no banco
                    //declaracaoDistanciaInteriorizacao.DataDeEmissao = DateTime.Now;
                    //_context.Add(declaracaoDistanciaInteriorizacao);
                    //_context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoDistanciaInteriorizacao(DeclaracaoDistanciaInteriorizacao declaracaoDistanciaInteriorizacao)
        {
            int numeroDeclaracaoDistanciaInteriorizacaoGerado;

            Random random = new Random();
            numeroDeclaracaoDistanciaInteriorizacaoGerado = random.Next().GetHashCode();
            declaracaoDistanciaInteriorizacao.NumeroDeclaracaoDistanciaInteriorizacao = numeroDeclaracaoDistanciaInteriorizacaoGerado;
            return numeroDeclaracaoDistanciaInteriorizacaoGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoDistanciaInteriorizacao(DeclaracaoDistanciaInteriorizacao declaracaoDistanciaInteriorizacao)
        {
            string codigoAutenticacaoServidor = GerarNumeroDeclaracaoDistanciaInteriorizacao(declaracaoDistanciaInteriorizacao).ToString("x");
            declaracaoDistanciaInteriorizacao.CodigoAutenticacaoDeclaracaoInteriorizacao = codigoAutenticacaoServidor;
            return codigoAutenticacaoServidor;
        }


    }
}
