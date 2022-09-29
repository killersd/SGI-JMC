using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.IO;

namespace SGI_JMC.Controllers
{
    public class HorarioServidorController : Controller
    {
        private readonly Context _context;

        public HorarioServidorController(Context context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult CreateHorarioServidor()
        {
            return View();
        }

        [Authorize]
        public FileResult gerarHorarioServidor(HorarioServidor horarioServidor)
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
                textFomatter.DrawString("HORÁRIO DE SERVIDOR(A)", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 234, page.Width, 135, 10, 10);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Dados do servidor(a)", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 235, page.Width, page.Height));
                textFomatter.DrawString("Unidade de Lotação: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 250, page.Width, page.Height));
                textFomatter.DrawString(horarioServidor.Unidade, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(130, 250, page.Width, page.Height));
                textFomatter.DrawString("Servidor(a): ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 265, page.Width, page.Height));
                textFomatter.DrawString(horarioServidor.Nome, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 265, page.Width, page.Height));
                textFomatter.DrawString("Cargo: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 280, page.Width, page.Height));
                textFomatter.DrawString(horarioServidor.Cargo.ToString(), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(50, 280, page.Width, page.Height));
                textFomatter.DrawString("Carga horária semanal: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 295, page.Width, page.Height));
                textFomatter.DrawString(horarioServidor.CargaHorariaSemanal.ToString() + " horas", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(145, 295, page.Width, page.Height));
                textFomatter.DrawString("Vínculo: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 310, page.Width, page.Height));
                textFomatter.DrawString(horarioServidor.Vinculo, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(60, 310, page.Width, page.Height));
                textFomatter.DrawString("Turno: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 325, page.Width, page.Height));
                textFomatter.DrawString(horarioServidor.Turno, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(50, 325, page.Width, page.Height));
                textFomatter.DrawString("Horário de trabalho: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 340, page.Width, page.Height));
                textFomatter.DrawString(horarioServidor.TurnoHorario, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(130, 340, page.Width, page.Height));


                //Tabela de horários
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 400, page.Width, 70, 10, 10);
                textFomatter.DrawString("Horário Matutino", fonteSubTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 405, page.Width, page.Height));
                //Título das colunas 
                var detalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                detalhes.DrawString("Domingo", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 420, page.Width, page.Height));
                detalhes.DrawString("Segunda-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(80, 420, page.Width, page.Height));
                detalhes.DrawString("Terça-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(180, 420, page.Width, page.Height));
                detalhes.DrawString("Quarta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(260, 420, page.Width, page.Height));
                detalhes.DrawString("Quinta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(350, 420, page.Width, page.Height));
                detalhes.DrawString("Sexta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(440, 420, page.Width, page.Height));
                detalhes.DrawString("Sábado", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(520, 420, page.Width, page.Height));

                var qtdHoras = 0;
                if (horarioServidor.segunda == null)
                {
                    horarioServidor.segunda = "**********"; ;
                }
                else
                {
                    qtdHoras = qtdHoras + 6; ;
                    detalhes.DrawString(horarioServidor.segunda, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 440, page.Width, page.Height));
                }
                if (horarioServidor.terca == null)
                {
                    horarioServidor.terca = "**********"; ;
                }
                else
                {
                    qtdHoras = qtdHoras + 6; ;
                    detalhes.DrawString(horarioServidor.terca, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(180, 440, page.Width, page.Height));
                }
                if (horarioServidor.quarta == null)
                {
                    horarioServidor.quarta = "**********"; ;
                }
                else
                {
                    qtdHoras = qtdHoras + 6; ;
                    detalhes.DrawString(horarioServidor.quarta, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(260, 440, page.Width, page.Height));
                }
                if (horarioServidor.quinta == null)
                {
                    horarioServidor.quinta = "**********"; ;
                }
                else
                {
                    qtdHoras = qtdHoras + 6; ;
                    detalhes.DrawString(horarioServidor.quinta, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(350, 440, page.Width, page.Height));
                }
                if (horarioServidor.sexta == null)
                {
                    horarioServidor.sexta = "**********"; ;
                }
                else
                {
                    qtdHoras = qtdHoras + 6; ;
                    detalhes.DrawString(horarioServidor.sexta, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(440, 440, page.Width, page.Height));
                }
                if (horarioServidor.sabado == null)
                {
                    horarioServidor.sabado = "**********"; ;
                }
                else
                {
                    qtdHoras = qtdHoras + 6; ;
                    detalhes.DrawString(horarioServidor.sabado, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(520, 440, page.Width, page.Height));
                }
                if (horarioServidor.domingo == null)
                {
                    horarioServidor.domingo = "**********"; ;
                }
                else
                {
                    qtdHoras = qtdHoras + 6; ;
                    detalhes.DrawString(horarioServidor.domingo, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 440, page.Width, page.Height));
                }

                if (qtdHoras < horarioServidor.CargaHorariaSemanal)
                {
                    detalhes.DrawString("Total de horas na semana: " + qtdHoras + " horas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 475, page.Width, page.Height));
                    textFomatter.DrawString("Total de horas inferior à carga horária semanal! ", fonteDetalhesDescricao, corFontelerta, new PdfSharpCore.Drawing.XRect(200, 475, page.Width, page.Height));
                }
                if (qtdHoras > horarioServidor.CargaHorariaSemanal)
                {
                    detalhes.DrawString("Total de horas na semana: " + qtdHoras + " horas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 475, page.Width, page.Height));
                    textFomatter.DrawString("Total de horas SUPERIOR à carga horária semanal! ", fonteDetalhesDescricao, corFontelerta, new PdfSharpCore.Drawing.XRect(200, 475, page.Width, page.Height));
                }
                detalhes.DrawString("Total de horas na semana: " + qtdHoras + " horas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 475, page.Width, page.Height));




                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 540, page.Width, page.Height));
                textFomatter.DrawString("Servidor(a)", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 550, page.Width, page.Height));
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 590, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Observação:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 650, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString(horarioServidor.Observacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));



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
                    var nomeArquivo = "Horário " + horarioServidor.Nome + ".pdf";
                    //Salvando no banco
                    _context.Add(horarioServidor);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

    }
}
