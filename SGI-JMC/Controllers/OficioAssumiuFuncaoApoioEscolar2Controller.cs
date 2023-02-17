using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.IO;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class OficioAssumiuFuncaoApoioEscolar2Controller : Controller
    {
        private readonly Context _context;

        public OficioAssumiuFuncaoApoioEscolar2Controller(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateOficioAssumiuFuncaoApoioEscolar2()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarOficioApoioEscolar2(OficioAssumiuFuncaoApoioEscolar2 oficioAssumiuFuncaoApoioEscolar2)
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
                oficioAssumiuFuncaoApoioEscolar2.DataEmissao = DateTime.Now;

                //Usuário
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                //Cabeçalho
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);
                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //Início              
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Ofício nº 00" + oficioAssumiuFuncaoApoioEscolar2.NumeroOficio + "/2022", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString("Assunto: " + oficioAssumiuFuncaoApoioEscolar2.Assunto + ".", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 165, page.Width, page.Height));
                textFomatter.DrawString("Simão Dias - Se -  " + oficioAssumiuFuncaoApoioEscolar2.DataEmissao.ToString("dd/MM/yyyy"), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));
                //Corpo do ofício
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("Comunicamos a Vossa Senhoria que " + oficioAssumiuFuncaoApoioEscolar2.Name + ", CPF " + oficioAssumiuFuncaoApoioEscolar2.CPF + ", vínculo " + oficioAssumiuFuncaoApoioEscolar2.vinculo + "(a), ocupante do Cargo de Apoio Escolar II, assumiu suas funções no dia " + oficioAssumiuFuncaoApoioEscolar2.DataAssumiuFuncao.ToString("dd/MM/yyyy") + " com carga horária de " + oficioAssumiuFuncaoApoioEscolar2.CargaHorariaMensal + " horas mensais,  conforme horário anexo, atuando no  Ensino Fundamental FRC "+oficioAssumiuFuncaoApoioEscolar2.FRC+" (FRC: Fonte de Recursos do FUNDEB).", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 330, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 500, page.Width, page.Height));
                textFomatter.DrawString("Vera Cristina Carvalho Oliveira", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 510, page.Width, page.Height));
                textFomatter.DrawString("Diretora - Port. 0314/2023", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 525, page.Width, page.Height));
                if (oficioAssumiuFuncaoApoioEscolar2.destinatario.Equals("Daniela Silva"))
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString("Senhora Diretora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 280, page.Width, page.Height));
                    //Destinatário
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString("Illma Senhora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                    textFomatter.DrawString(oficioAssumiuFuncaoApoioEscolar2.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                    textFomatter.DrawString(oficioAssumiuFuncaoApoioEscolar2.CargoDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                    textFomatter.DrawString(oficioAssumiuFuncaoApoioEscolar2.CidadeDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));
                                    }
                if (oficioAssumiuFuncaoApoioEscolar2.destinatario.Equals("Kleber do Carmo"))
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString("Senhor Diretor,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 280, page.Width, page.Height));
                    //Destinatário
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString("Illmº Senhor,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                    textFomatter.DrawString(oficioAssumiuFuncaoApoioEscolar2.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                    textFomatter.DrawString(oficioAssumiuFuncaoApoioEscolar2.CargoDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                    textFomatter.DrawString(oficioAssumiuFuncaoApoioEscolar2.CidadeDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));

                }
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
                    var nomeArquivo = "Ofício " + oficioAssumiuFuncaoApoioEscolar2.Name + ".pdf";
                    //Salvando no banco
                    oficioAssumiuFuncaoApoioEscolar2.DataEmissao = DateTime.Now;
                    _context.Add(oficioAssumiuFuncaoApoioEscolar2);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }
    }
}
