using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class AdvertenciaController : Controller
    {
        private readonly Context _context;


        public AdvertenciaController(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "administrador")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advertencia.ToListAsync());
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Father_name,Mother_name,Birth_date,anoSerie,turma,dataDeEmissao,descricaoDoFato,turno")] Models.Advertencia advertencia)
        {
            if (ModelState.IsValid)
            {
                advertencia.dataDeEmissao = DateTime.Now;
                _context.Add(advertencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(advertencia);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarAdvertencia(Models.Advertencia advertencia)
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
                var fonteTituloGigante = new PdfSharpCore.Drawing.XFont("Calibri", 24, PdfSharpCore.Drawing.XFontStyle.Bold);

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

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("ADVERTÊNCIA", fonteTituloGigante, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 400, page.Width, 120, 10, 10);


                //melhorar isso aqui
                string turmaString = advertencia.turma.ToString();
                string dataNascString = advertencia.Birth_date.ToString("dd/MM/yyyy");

                if (advertencia.Father_name != null)
                {
                    if ((advertencia.anoSerie.Equals("F3")) || (advertencia.anoSerie.Equals("F4")))
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Venho através desta notificação informar que o aluno(a) " + advertencia.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + advertencia.Mother_name.ToUpper() + " e " + advertencia.Father_name.ToUpper() + ", " +
                            " matriculado no(a) " + advertencia.anoSerie.ToUpper() + ", turma \"" + advertencia.turma.ToUpper() + "\" no turno " + advertencia.turno + ", está sendo advertido por violar as normas do Regimento desta Unidade de Ensino. Informo ainda " +
                            "que é necessária a presença do responsável legal pelo aluno para dialogar com a equipe diretiva sobre a vida escolar do referido discente.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                        textFomatter.DrawString("Descrição do fato:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 400, page.Width, page.Height));
                        textFomatter.DrawString(advertencia.descricaoDoFato, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(10, 420, 575, page.Height));
                    }
                    else
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Venho através desta notificação informar que o aluno(a) " + advertencia.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + advertencia.Mother_name.ToUpper() + " e " + advertencia.Father_name.ToUpper() + ", " +
                            " matriculado no(a) " + advertencia.anoSerie.ToUpper() + "º ano, turma \"" + advertencia.turma.ToUpper() + "\" no turno " + advertencia.turno + ", está sendo advertido por violar as normas do Regimento desta Unidade de Ensino. Informo ainda " +
                            "que é necessária a presença do responsável legal pelo aluno para dialogar com a equipe diretiva sobre a vida escolar do referido discente.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                        textFomatter.DrawString("Descrição do fato:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 400, page.Width, page.Height));
                        textFomatter.DrawString(advertencia.descricaoDoFato, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(10, 420, 575, page.Height));
                    }
                }
                else
                {
                    if ((advertencia.anoSerie.Equals("F3")) || (advertencia.anoSerie.Equals("F4")))
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Venho através desta notificação informar que o aluno(a) " + advertencia.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + advertencia.Mother_name.ToUpper() + ", " +
                            " matriculado no(a) " + advertencia.anoSerie.ToUpper() + ", turma \"" + advertencia.turma.ToUpper() + "\" no turno " + advertencia.turno + ", está sendo advertido por violar as normas do Regimento desta Unidade de Ensino. Informo ainda " +
                            "que é necessária a presença do responsável legal pelo aluno para dialogar com a equipe diretiva sobre a vida escolar do referido discente.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                        textFomatter.DrawString("Descrição do fato:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 400, page.Width, page.Height));
                        textFomatter.DrawString(advertencia.descricaoDoFato, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(10, 420, 575, page.Height));
                    }
                    else
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Venho através desta notificação informar que o aluno(a) " + advertencia.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + advertencia.Mother_name.ToUpper() + ", " +
                            " matriculado no(a) " + advertencia.anoSerie.ToUpper() + "º ano, turma \"" + advertencia.turma.ToUpper() + "\" no turno " + advertencia.turno + ", está sendo advertido por violar as normas do Regimento desta Unidade de Ensino. Informo ainda " +
                            "que é necessária a presença do responsável legal pelo aluno para dialogar com a equipe diretiva sobre a vida escolar do referido discente.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                        textFomatter.DrawString("Descrição do fato:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(10, 400, page.Width, page.Height));
                        textFomatter.DrawString(advertencia.descricaoDoFato, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(10, 420, 575, page.Height));
                    }
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 555, page.Width, page.Height));
                textFomatter.DrawString("Responsável pelo aluno", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 565, page.Width, page.Height));
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 610, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Testemunhas:", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 650, page.Width, page.Height));
                textFomatter.DrawString("______________________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 675, page.Width, page.Height));
                textFomatter.DrawString("______________________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 700, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Advertência emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta advertência foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Advertência " + advertencia.Name + ".pdf";
                    //Salvando no banco
                    advertencia.dataDeEmissao = DateTime.Now;
                    _context.Add(advertencia);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

    }
}
