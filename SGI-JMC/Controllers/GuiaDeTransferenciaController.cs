using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System;
using iText.Layout.Element;

namespace SGI_JMC.Controllers
{
    public class GuiaDeTransferenciaController : Controller
    {
        private readonly Context _context;

        public GuiaDeTransferenciaController(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateGuiaTransferencia()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        public async Task<IActionResult> CreateGuiaTransferencia(GuiaDeTransferencia guiaDeTransferencia)
        {
            if (ModelState.IsValid)
            {
                guiaDeTransferencia.DataEmissao = DateTime.Now;
                _context.Add(guiaDeTransferencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guiaDeTransferencia);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarGuiaDeTransferencia(GuiaDeTransferencia guiaDeTransferencia)
        {
            //guiaDeTransferencia.codigoAutenticacaoGuiaTransferencia = GerarCodigoDeAutenticacaoTransferencia(guiaDeTransferencia);
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDesricao = new PdfSharpCore.Drawing.XFont("Calibri", 14);
                var fonteTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 17, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteRodape = new PdfSharpCore.Drawing.XFont("Calibri", 7);
                var fonteDesricaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 14, PdfSharpCore.Drawing.XFontStyle.Bold);


                var brasao = Path.GetFullPath("wwwroot/Imagens/BrasaoEstado.png");
                XImage imgBrasao = XImage.FromFile(brasao);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);


                var escudo = Path.GetFullPath("wwwroot/Imagens/SemTransparencia.png");
                XImage imgEscudo = XImage.FromFile(escudo);
                graphics.DrawImage(imgEscudo, 475, 60, 100, 100);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("GUIA DE TRANSFERÊNCIA", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 120, page.Width, page.Height));
                textFomatter.DrawString("ENSINO FUNDAMENTAL E MÉDIO - (Lei 9.394/96)", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 140, page.Width, page.Height));

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 175, page.Width, 155, 10, 10);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Instituição De Ensino: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 180, page.Width, page.Height));
                textFomatter.DrawString("Escola Estadual João de Mattos Carvalho ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(140, 180, page.Width, page.Height));
                textFomatter.DrawString("CNPJ (MF) Nº: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 195, page.Width, page.Height));
                textFomatter.DrawString("01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(95, 195, page.Width, page.Height));
                textFomatter.DrawString("Endereço: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 210, page.Width, page.Height));
                textFomatter.DrawString("Praça Abel Jacó dos Santos, nº 892", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(75, 210, page.Width, page.Height));

                textFomatter.DrawString("Credenciamento: ", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 225, page.Width, page.Height));
                textFomatter.DrawString("Autorização: ", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(230, 225, page.Width, page.Height));
                textFomatter.DrawString("Res. Nº :166/CEE, de 21/06/2012 ", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(285, 225, page.Width, page.Height));
                textFomatter.DrawString("Reconhecimento: ", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(450, 225, page.Width, page.Height));

                textFomatter.DrawString("Concedemos a pressente Guia de Transferência do(a) aluno(a) ", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(10, 238, page.Width, page.Height));
                textFomatter.DrawString("Alex de Oliveira Souza", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 250, page.Width, page.Height));
                textFomatter.DrawString("Data de Nascimento: 27/09/1987", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(300, 250, page.Width, page.Height));
                textFomatter.DrawString("Nome da mãe: Josefa de Oliveira Souza", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(10, 265, page.Width, page.Height));
                textFomatter.DrawString("Nome do pai: José Domingos Dias de Souza", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(10, 280, page.Width, page.Height));
                textFomatter.DrawString("Matriculado(a) no(a): 9º Ano do Ensino Fundamental", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(10, 295, page.Width, page.Height));
                textFomatter.DrawString("Ano: " + DateTime.Now.Year, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(400, 295, page.Width, page.Height));

                textFomatter.DrawString("Conforme rendimento obtido abaixo e/ou no verso desta Guia ", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(10, 312, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("Transferência por atividade ou disciplina", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 335, page.Width, page.Height));

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 360, page.Width, 205, 10, 10);
                textFomatter.DrawString("Rendimento Escolar", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 362, page.Width, page.Height));

                int inicio = 55;

                for (int i = 0; i < 37; i++)
                {
                    graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 380, inicio, 185, 0, 0);
                    inicio = inicio + 15;
                }

                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 445, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("1ª unidade", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 461, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 460, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("2ª unidade", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 476, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 475, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Média", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 491, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 490, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("3ª unidade", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 506, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 505, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("4ª unidade", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 521, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 520, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Média", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 536, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 535, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Média Final", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 551, page.Width, page.Height));

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 575, 250, 180, 10, 10);
                textFomatter.DrawString("Reservado ao DIES/SEDUC", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(70, 576, page.Width, page.Height));

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 255, 575, 340, 180, 10, 10);
                textFomatter.DrawString("Reservado à Instituição de Ensino", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(360, 576, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Simão Dias - Se, " + DateTime.Now.ToString("dd/MM/yyyy"), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));
                textFomatter.DrawString("_________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("_________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));
                textFomatter.DrawString("Assinatura do(a) secretário(a)", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 775, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("_________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));
                textFomatter.DrawString("Assinatura do(a) diretor(a)", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(-35, 775, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("O aluno concluiu o curso nos termos da Legislação em vigor à época", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 790, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 790, page.Width, page.Height));
                textFomatter.DrawString("Esta guia de transferência foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));


                graphics.Dispose();

                int v1 = 65, v2 = 90;

                for (int i = 0; i < 1; i++)
                {
                    XGraphics gfxRotate = XGraphics.FromPdfPage(page);
                    gfxRotate.RotateAtTransform(-90, new XPoint(v1, 480));
                    gfxRotate.DrawString("Português", fonteDetalhesDescricao, XBrushes.Black, new XPoint(v2, 480));
                    gfxRotate.Dispose();
                    v1 = v1 + 15;
                    v2 = v2 + 15;
                }

                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                //textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                //textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                //textFomatter.DrawString("Número do documento: " + declaracaoGenerica.numeroDeclaracaoGenerica, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                //textFomatter.DrawString("Código de verificação: " + declaracaoGenerica.codigoAutenticacaoDeclaracaoGenerica, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                //string dataString = DateTime.Now.ToString("dd/MM/yyyy");

                //textFomatter.DrawString("Declaração emitida em " + DateTime.Now.ToString("dd/MM/yyyy"), fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));


                var page2 = doc.AddPage();
                page2.Size = PdfSharpCore.PageSize.A4;
                page2.TrimMargins.Right = 50;
                page2.TrimMargins.Left = 50;
                page2.Orientation = PdfSharpCore.PageOrientation.Portrait;


                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Guia de transferência " + " Teste " + ".pdf";
                    //Salvando no banco
                    guiaDeTransferencia.DataEmissao = DateTime.Now;
                    //_context.Add(declaracaoGenerica);
                    //_context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }


        public FileResult gerarGuiaDeTransferenciaFundo(GuiaDeTransferencia guiaDeTransferencia)
        {
            //guiaDeTransferencia.codigoAutenticacaoGuiaTransferencia = GerarCodigoDeAutenticacaoTransferencia(guiaDeTransferencia);
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDesricao = new PdfSharpCore.Drawing.XFont("Calibri", 14);
                var fonteTitulo = new PdfSharpCore.Drawing.XFont("Calibri", 17, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Calibri", 10);
                var fonteRodape = new PdfSharpCore.Drawing.XFont("Calibri", 7);
                var fonteDesricaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 14, PdfSharpCore.Drawing.XFontStyle.Bold);

                //var escudo = Path.GetFullPath("wwwroot/Imagens/Escudo.png");
                //XImage imgEscudo = XImage.FromFile(escudo);
                //graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 50, page.Width, 700, 10, 10);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("HISTÓRICO ESCOLAR", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 50, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 55, page.Width, page.Height));
                textFomatter.DrawString("ENSINO FUNDAMENTAL", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 70, page.Width, page.Height));
                textFomatter.DrawString("Aprroveitamento", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 85, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 90, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("DISCIPLINAS", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(70, 130, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                              
                
                textFomatter.DrawString("NOTAS", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(100, 150, page.Width, page.Height));

                int inicio = 165;

                for (int i = 0; i < 24; i++)
                {
                    textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, inicio, page.Width, page.Height));
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString("", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, inicio + 1, page.Width, page.Height));
                    inicio = inicio + 15;
                }
                                                                                        //1º parametro margem esquerda/2º altura(eixo Y)/3ºLargura/4ºaltura                                             
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 105, 200, 435, 0, 0);

                double largura = 200;

                for (int i = 0; i < 10; i++)
                {
                    graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 165, largura, 375, 0, 0);
                    largura = largura + 43.9;
                }
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 550, page.Width, page.Height));

                //textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 445, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //textFomatter.DrawString("", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 461, page.Width, page.Height));
                //textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 460, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //textFomatter.DrawString(" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 476, page.Width, page.Height));
                //textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 475, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //textFomatter.DrawString("Média", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 491, page.Width, page.Height));
                //textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 490, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //textFomatter.DrawString("3ª unidade", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 506, page.Width, page.Height));
                //textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 505, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //textFomatter.DrawString("4ª unidade", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 521, page.Width, page.Height));
                //textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 520, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //textFomatter.DrawString("Média", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 536, page.Width, page.Height));
                //textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 535, page.Width, page.Height));
                //textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //textFomatter.DrawString("Média Final", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 551, page.Width, page.Height));

                //graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 575, 250, 180, 10, 10);
                //textFomatter.DrawString("Reservado ao DIES/SEDUC", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(70, 576, page.Width, page.Height));

                //graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 255, 575, 340, 180, 10, 10);
                //textFomatter.DrawString("Reservado à Instituição de Ensino", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(360, 576, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Simão Dias - Se, " + DateTime.Now.ToString("dd/MM/yyyy"), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));
                textFomatter.DrawString("_________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("_________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));
                textFomatter.DrawString("Assinatura do(a) secretário(a)", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 775, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("_________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 760, page.Width, page.Height));
                textFomatter.DrawString("Assinatura do(a) diretor(a)", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(-35, 775, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 790, page.Width, page.Height));
                textFomatter.DrawString("Esta guia de transferência foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));


                graphics.Dispose();

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Guia de transferência fundo   " + " Teste " + ".pdf";
                    //Salvando no banco
                    guiaDeTransferencia.DataEmissao = DateTime.Now;
                    //_context.Add(declaracaoGenerica);
                    //_context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }


        //[Authorize(Roles = "usuario, administrador")]
        //public int GerarNumeroDeclaracaoGenerica(DeclaracaoGenerica declaracaoGenerica)
        //{
        //    int numeroDeclaracaoGerado;

        //    Random random = new Random();

        //    numeroDeclaracaoGerado = random.Next().GetHashCode();
        //    declaracaoGenerica.numeroDeclaracaoGenerica = numeroDeclaracaoGerado;
        //    return numeroDeclaracaoGerado;
        //}

        //[Authorize(Roles = "usuario, administrador")]
        //public string GerarCodigoDeAutenticacaoGnerica(DeclaracaoGenerica declaracaoGenerica)
        //{
        //    string codigoAutenticacao = GerarNumeroDeclaracaoGenerica(declaracaoGenerica).ToString("x");
        //    declaracaoGenerica.codigoAutenticacaoDeclaracaoGenerica = codigoAutenticacao;
        //    return codigoAutenticacao;
        //}
    }
}
