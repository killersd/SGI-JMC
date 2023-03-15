using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.IO;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class OficioAssumiuFuncaoServidorController : Controller
    {
        private readonly Context _context;

        public OficioAssumiuFuncaoServidorController(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateOficioServidor()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        public FileResult gerarOficioServidor(OficioAssumiuFuncaoServidor oficioAssumiuFuncaoServidor)
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
                oficioAssumiuFuncaoServidor.DataEmissao = DateTime.Now;

                //Usuário
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
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
                textFomatter.DrawString("Ofício nº 00" + oficioAssumiuFuncaoServidor.NumeroOficio + "/"+ DateTime.Now.Year, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString("Assunto: " + oficioAssumiuFuncaoServidor.Assunto + ".", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 165, page.Width, page.Height));
                textFomatter.DrawString("Simão Dias - Se -  " + oficioAssumiuFuncaoServidor.DataEmissao.ToString("dd/MM/yyyy"), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));
                //Corpo do ofício,

                string ch = null;

                if (oficioAssumiuFuncaoServidor.CargaHoraria > 44)
                {
                    ch = "mensais";
                }
                else
                {
                    ch = "semanais";
                }

                if (oficioAssumiuFuncaoServidor.FRC.Equals(0))
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Comunicamos a Vossa Senhoria que " + oficioAssumiuFuncaoServidor.Name + ", CPF " + oficioAssumiuFuncaoServidor.CPF + ", vínculo " + oficioAssumiuFuncaoServidor.vinculo + ", ocupante do Cargo de " + oficioAssumiuFuncaoServidor.cargo + ", assumiu suas funções no dia " + oficioAssumiuFuncaoServidor.DataAssumiuFuncao.ToString("dd/MM/yyyy") + " com carga horária de " + oficioAssumiuFuncaoServidor.CargaHoraria + " horas " + ch + ", conforme horário anexo.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 330, page.Width, page.Height));
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                    textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 500, page.Width, page.Height));
                    textFomatter.DrawString("Vera Cristina Carvalho Oliveira", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 510, page.Width, page.Height));
                    textFomatter.DrawString("Diretora - Port. 0314/2023", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 525, page.Width, page.Height));
                    if (oficioAssumiuFuncaoServidor.destinatario.Equals("Kleber do Carmo"))
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Senhor Diretor,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 280, page.Width, page.Height));
                        //Destinatário
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Illmo Senhor,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CargoDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CidadeDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));
                    }
                    if (oficioAssumiuFuncaoServidor.destinatario.Equals("Daniela Silva"))
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Senhora Diretora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 280, page.Width, page.Height));
                        //Destinatário
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Illma Senhora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CargoDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CidadeDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));
                    }
                }
                else 
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Comunicamos a Vossa Senhoria que " + oficioAssumiuFuncaoServidor.Name + ", CPF " + oficioAssumiuFuncaoServidor.CPF + ", vínculo " + oficioAssumiuFuncaoServidor.vinculo + ", ocupante do Cargo de " + oficioAssumiuFuncaoServidor.cargo + ", assumiu suas funções no dia " + oficioAssumiuFuncaoServidor.DataAssumiuFuncao.ToString("dd/MM/yyyy") + " com carga horária de " + oficioAssumiuFuncaoServidor.CargaHoraria + " horas " + ch + ", conforme horário anexo.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 330, page.Width, page.Height));
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                    textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 500, page.Width, page.Height));
                    textFomatter.DrawString("Vera Cristina Carvalho Oliveira", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 510, page.Width, page.Height));
                    textFomatter.DrawString("Diretora - Port. 0314/2023", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 525, page.Width, page.Height));
                    if (oficioAssumiuFuncaoServidor.destinatario.Equals("Kleber do Carmo"))
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Senhor Diretor,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 280, page.Width, page.Height));
                        //Destinatário
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Illmo Senhor,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CargoDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CidadeDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));
                    }
                    if (oficioAssumiuFuncaoServidor.destinatario.Equals("Daniela Silva"))
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Senhora Diretora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 280, page.Width, page.Height));
                        //Destinatário
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                        textFomatter.DrawString("Illma Senhora,", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.destinatario, fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 682, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CargoDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 694, page.Width, page.Height));
                        textFomatter.DrawString(oficioAssumiuFuncaoServidor.CidadeDestinatario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 706, page.Width, page.Height));
                    }
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
                    var nomeArquivo = "Ofício " + oficioAssumiuFuncaoServidor.Name + ".pdf";
                    //Salvando no banco
                    oficioAssumiuFuncaoServidor.DataEmissao = DateTime.Now;
                    _context.Add(oficioAssumiuFuncaoServidor);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }
    }
}
