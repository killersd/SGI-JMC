using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.IO;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class HorarioApoioEscolar2Controller : Controller
    {
        private readonly Context _context;
        public HorarioApoioEscolar2Controller(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateHorarioApoio2()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        private bool VerificarTurnoTarde(HorarioApoioEscolar2 hp)
        {
            if (hp.ts01 == null && hp.ts02 == null && hp.ts03 == null && hp.ts04 == null && hp.ts05 == null
               && hp.tt01 == null && hp.tt02 == null && hp.tt03 == null && hp.tt04 == null && hp.tt05 == null
               && hp.tq01 == null && hp.tq02 == null && hp.tq03 == null && hp.tq04 == null && hp.tq05 == null
               && hp.tqu01 == null && hp.tqu02 == null && hp.tqu03 == null && hp.tqu04 == null && hp.tqu05 == null
               && hp.tse01 == null && hp.tse02 == null && hp.tse03 == null && hp.tse04 == null && hp.tse05 == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Authorize(Roles = "usuario, administrador")]
        private bool VerificarTurnoManha(HorarioApoioEscolar2 hp)
        {
            if (hp.s01 == null && hp.s02 == null && hp.s03 == null && hp.s04 == null && hp.s05 == null
               && hp.t01 == null && hp.t02 == null && hp.t03 == null && hp.t04 == null && hp.t05 == null
               && hp.q01 == null && hp.q02 == null && hp.q03 == null && hp.q04 == null && hp.q05 == null
               && hp.qu01 == null && hp.qu02 == null && hp.qu03 == null && hp.qu04 == null && hp.qu05 == null
               && hp.se01 == null && hp.se02 == null && hp.se03 == null && hp.se04 == null && hp.se05 == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarHorarioApoio2(HorarioApoioEscolar2 horarioApoioEscolar2)
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

                var brasao = Path.GetFullPath("wwwroot/Imagens/BrasaoEstado.png");
                var escudo = Path.GetFullPath("wwwroot/Imagens/Escudo.png");
                var logo = Path.GetFullPath("wwwroot/Imagens/SGI.jpg");
                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);
                XImage imgLogo = XImage.FromFile(logo);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                graphics.DrawImage(imgLogo, 480, 60, 120, 50);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;

                //Usuário
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                //Cabeçalho
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //Início
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("HORÁRIO DE PROFESSOR(A)", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 234, page.Width, 100, 10, 10);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Dados do servidor(a)", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 235, page.Width, page.Height));
                textFomatter.DrawString("Nome: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 250, page.Width, page.Height));
                textFomatter.DrawString(horarioApoioEscolar2.Nome, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(50, 250, page.Width, page.Height));
                textFomatter.DrawString("CPF: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 265, page.Width, page.Height));
                textFomatter.DrawString(horarioApoioEscolar2.CPF, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(35, 265, page.Width, page.Height));
                textFomatter.DrawString("Cargo: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 280, page.Width, page.Height));
                textFomatter.DrawString(horarioApoioEscolar2.Cargo, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(50, 280, page.Width, page.Height));
                textFomatter.DrawString("Carga horária mensal: ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(5, 295, page.Width, page.Height));
                textFomatter.DrawString(horarioApoioEscolar2.CargaHorariaMensal.ToString(), fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(140, 295, page.Width, page.Height));
                //Tabela de horários
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 370, page.Width, 130, 10, 10);
                textFomatter.DrawString("Turno Matutino", fonteSubTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 370, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 375, page.Width, page.Height));
                //Título das colunas manhã
                var detalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

                detalhes.DrawString("Segunda-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(40, 390, page.Width, page.Height));
                detalhes.DrawString("Terça-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(150, 390, page.Width, page.Height));
                detalhes.DrawString("Quarta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(250, 390, page.Width, page.Height));
                detalhes.DrawString("Quinta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(370, 390, page.Width, page.Height));
                detalhes.DrawString("Sexta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(500, 390, page.Width, page.Height));
                //Horários manhã
                detalhes.DrawString("1º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 410, page.Width, page.Height));
                detalhes.DrawString("2º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 425, page.Width, page.Height));
                detalhes.DrawString("3º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 440, page.Width, page.Height));
                detalhes.DrawString("4º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 455, page.Width, page.Height));
                detalhes.DrawString("5º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 470, page.Width, page.Height));
                //Título das colunas tarde
                detalhes.DrawString("Segunda-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(40, 530, page.Width, page.Height));
                detalhes.DrawString("Terça-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(150, 530, page.Width, page.Height));
                detalhes.DrawString("Quarta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(250, 530, page.Width, page.Height));
                detalhes.DrawString("Quinta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(370, 530, page.Width, page.Height));
                detalhes.DrawString("Sexta-Feira", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(500, 530, page.Width, page.Height));
                //Horários tarde
                detalhes.DrawString("1º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 550, page.Width, page.Height));
                detalhes.DrawString("2º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 565, page.Width, page.Height));
                detalhes.DrawString("3º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 580, page.Width, page.Height));
                detalhes.DrawString("4º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 595, page.Width, page.Height));
                detalhes.DrawString("5º ", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(15, 610, page.Width, page.Height));

                if (VerificarTurnoTarde(horarioApoioEscolar2))
                {
                    detalhes.DrawString("Professor(a) não ministra aulas neste turno", fonteDesricao, corFontelerta, new PdfSharpCore.Drawing.XRect(200, 580, page.Width, page.Height));
                }

                if (VerificarTurnoManha(horarioApoioEscolar2))
                {
                    detalhes.DrawString("Professor(a) não ministra aulas neste turno", fonteDesricao, corFontelerta, new PdfSharpCore.Drawing.XRect(200, 440, page.Width, page.Height));
                }

                //Horario da segunda
                var qtdHoras = 0;
                if (horarioApoioEscolar2.s01 == null)
                {
                    horarioApoioEscolar2.s01 = "**********"; ;
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.s01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 410, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.s02 == null)
                {
                    horarioApoioEscolar2.s02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.s02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 425, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.s03 == null)
                {
                    horarioApoioEscolar2.s03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.s03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 440, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.s04 == null)
                {
                    horarioApoioEscolar2.s04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.s04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 455, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.s05 == null)
                {
                    horarioApoioEscolar2.s05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.s05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 470, page.Width, page.Height));
                }

                //Horario terça
                if (horarioApoioEscolar2.t01 == null)
                {
                    horarioApoioEscolar2.t01 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.t01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 410, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.t02 == null)
                {
                    horarioApoioEscolar2.t02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.t02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 425, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.t03 == null)
                {
                    horarioApoioEscolar2.t03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.t03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 440, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.t04 == null)
                {
                    horarioApoioEscolar2.t04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.t04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 455, page.Width, page.Height));
                }
                if (horarioApoioEscolar2.t05 == null)
                {
                    horarioApoioEscolar2.t05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.t05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 470, page.Width, page.Height));
                }

                //Horario Quarta
                if (horarioApoioEscolar2.q01 == null)
                {
                    horarioApoioEscolar2.q01 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.q01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 410, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.q02 == null)
                {
                    horarioApoioEscolar2.q02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.q02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 425, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.q03 == null)
                {
                    horarioApoioEscolar2.q03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.q03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 440, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.q04 == null)
                {
                    horarioApoioEscolar2.q04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.q04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 455, page.Width, page.Height));
                }
                if (horarioApoioEscolar2.q05 == null)
                {
                    horarioApoioEscolar2.q05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.q05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 470, page.Width, page.Height));
                }

                //Horario quinta
                if (horarioApoioEscolar2.qu01 == null)
                {
                    horarioApoioEscolar2.qu01 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.qu01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 410, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.qu02 == null)
                {
                    horarioApoioEscolar2.qu02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.qu02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 425, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.qu03 == null)
                {
                    horarioApoioEscolar2.qu03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.qu03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 440, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.qu04 == null)
                {
                    horarioApoioEscolar2.qu04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.qu04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 455, page.Width, page.Height));
                }
                if (horarioApoioEscolar2.qu05 == null)
                {
                    horarioApoioEscolar2.qu05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.qu05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 470, page.Width, page.Height));
                }

                //Horario sexta
                if (horarioApoioEscolar2.se01 == null)
                {
                    horarioApoioEscolar2.se01 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.se01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 410, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.se02 == null)
                {
                    horarioApoioEscolar2.se02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.se02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 425, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.se03 == null)
                {
                    horarioApoioEscolar2.se03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.se03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 440, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.se04 == null)
                {
                    horarioApoioEscolar2.se04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.se04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 455, page.Width, page.Height));
                }
                if (horarioApoioEscolar2.se05 == null)
                {
                    horarioApoioEscolar2.se05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.se05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 470, page.Width, page.Height));
                }

                //segunda vespertino
                if (horarioApoioEscolar2.ts01 == null)
                {
                    horarioApoioEscolar2.ts01 = "**********"; ;
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.ts01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 550, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.ts02 == null)
                {
                    horarioApoioEscolar2.ts02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.ts02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 565, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.ts03 == null)
                {
                    horarioApoioEscolar2.ts03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.ts03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 580, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.ts04 == null)
                {
                    horarioApoioEscolar2.ts04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.ts04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 595, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.ts05 == null)
                {
                    horarioApoioEscolar2.ts05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.ts05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(40, 610, page.Width, page.Height));
                }
                //terça vespertino
                if (horarioApoioEscolar2.tt01 == null)
                {
                    horarioApoioEscolar2.tt01 = "**********"; ;
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tt01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 550, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tt02 == null)
                {
                    horarioApoioEscolar2.tt02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tt02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 565, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tt03 == null)
                {
                    horarioApoioEscolar2.tt03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tt03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 580, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tt04 == null)
                {
                    horarioApoioEscolar2.tt04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tt04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 595, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tt05 == null)
                {
                    horarioApoioEscolar2.tt05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tt05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(150, 610, page.Width, page.Height));
                }
                //quarta vespertino
                if (horarioApoioEscolar2.tq01 == null)
                {
                    horarioApoioEscolar2.tq01 = "**********"; ;
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tq01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 550, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tq02 == null)
                {
                    horarioApoioEscolar2.tq02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tq02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 565, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tq03 == null)
                {
                    horarioApoioEscolar2.tq03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tq03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 580, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tq04 == null)
                {
                    horarioApoioEscolar2.tq04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tq04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 595, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tq05 == null)
                {
                    horarioApoioEscolar2.tq05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tq05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(250, 610, page.Width, page.Height));
                }
                //quinta vespertino
                if (horarioApoioEscolar2.tqu01 == null)
                {
                    horarioApoioEscolar2.tqu01 = "**********"; ;
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tqu01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 550, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tqu02 == null)
                {
                    horarioApoioEscolar2.tqu02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tqu02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 565, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tqu03 == null)
                {
                    horarioApoioEscolar2.tqu03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tqu03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 580, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tqu04 == null)
                {
                    horarioApoioEscolar2.tqu04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tqu04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 595, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tqu05 == null)
                {
                    horarioApoioEscolar2.tqu05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tqu05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 610, page.Width, page.Height));
                }
                //sexta vespertino
                if (horarioApoioEscolar2.tse01 == null)
                {
                    horarioApoioEscolar2.tse01 = "**********"; ;
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tse01, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 550, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tse02 == null)
                {
                    horarioApoioEscolar2.tse02 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tse02, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 565, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tse03 == null)
                {
                    horarioApoioEscolar2.tse03 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tse03, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 580, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tse04 == null)
                {
                    horarioApoioEscolar2.tse04 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tse04, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(500, 595, page.Width, page.Height));
                }

                if (horarioApoioEscolar2.tse05 == null)
                {
                    horarioApoioEscolar2.tse05 = "**********";
                }
                else
                {
                    qtdHoras++;
                    detalhes.DrawString(horarioApoioEscolar2.tse05, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(370, 610, page.Width, page.Height));
                }


                //if (qtdHoras < horarioApoioEscolar2.CargaHorariaMensal)
                //{
                //    detalhes.DrawString("Total de aulas na semana: " + qtdHoras + " aulas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 645, page.Width, page.Height));
                //    textFomatter.DrawString("Total de aulas inferior à carga horária semanal! ", fonteDetalhesDescricao, corFontelerta, new PdfSharpCore.Drawing.XRect(200, 645, page.Width, page.Height));
                //}
                //else if (qtdHoras > horarioApoioEscolar2.CargaHorariaMensal)
                //{
                //    detalhes.DrawString("Total de aulas na semana: " + qtdHoras + " aulas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 645, page.Width, page.Height));
                //    textFomatter.DrawString("Total de aulas SUPERIOR à carga horária semanal! ", fonteDetalhesDescricao, corFontelerta, new PdfSharpCore.Drawing.XRect(200, 645, page.Width, page.Height));
                //}
                {
                    detalhes.DrawString("Total de aulas na semana: " + qtdHoras + " aulas.", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(15, 645, page.Width, page.Height));
                }

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 510, page.Width, 130, 10, 10);
                textFomatter.DrawString("Turno Vespertino", fonteSubTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 510, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 515, page.Width, page.Height));



                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 670, page.Width, page.Height));
                textFomatter.DrawString("Servidor(a)", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 680, page.Width, page.Height));
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 720, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));



                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
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
                    var nomeArquivo = "Horário " + horarioApoioEscolar2.Nome + ".pdf";
                    //Salvando no banco
                    _context.Add(horarioApoioEscolar2);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }
    }
}
