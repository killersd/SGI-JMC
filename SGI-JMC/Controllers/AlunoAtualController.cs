using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using PdfSharpCore.Drawing;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using SGI_JMC.Services;
using SGI_JMC.ViewModels;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography.Xml;

namespace SGI_JMC.Controllers
{
    [Authorize]
    public class AlunoAtualController : Controller
    {
        private readonly Context _contexto;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AlunoAtualController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService, Context context, IWebHostEnvironment webHostEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._emailService = emailService;
            _contexto = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //public AlunoAtualController(Context context)
        //{
        //    _contexto = context;
        //}
        [Authorize(Roles = "usuario, administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.AlunoAtual.ToListAsync());
        }

        [Authorize(Roles = "usuario, administrador")]
        public async Task<IActionResult> AlunosSextoAnoA()
        {
            var alunos = await _contexto.AlunoAtual.Where(p => p.anoSerie == 6 && p.turma.Equals('A')).ToListAsync();
            return View(alunos);
        }

        [Authorize]
        public async Task<IActionResult> MostrarMatriculas()
        {
            var segundoU = await _contexto.AlunoAtual.Where(p => p.anoSerie == 2 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
            ViewBag.segundoU = segundoU.Count;
            var terceiroU = await _contexto.AlunoAtual.Where(p => p.anoSerie == 3 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
            ViewBag.terceiroU = terceiroU.Count;
            var quartoU = await _contexto.AlunoAtual.Where(p => p.anoSerie == 4 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
            ViewBag.quartoU = quartoU.Count;
            var quintoU = await _contexto.AlunoAtual.Where(p => p.anoSerie == 5 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
            ViewBag.quintoU = quintoU.Count;

            var sextoA = await _contexto.AlunoAtual.Where(p => p.anoSerie == 6 && p.turma.Equals('A') && p.Transferido == false).ToListAsync();
            ViewBag.SextoAnoA = sextoA.Count;
            var sextoB = await _contexto.AlunoAtual.Where(p => p.anoSerie == 6 && p.turma.Equals('B') && p.Transferido == false).ToListAsync();
            ViewBag.SextoAnoB = sextoB.Count;

            var setimoU = await _contexto.AlunoAtual.Where(p => p.anoSerie == 7 && p.turma.Equals('U') && p.Transferido == false).ToListAsync();
            ViewBag.setimoU = setimoU.Count;

            var oitavoA = await _contexto.AlunoAtual.Where(p => p.anoSerie == 8 && p.turma.Equals('A') && p.Transferido == false).ToListAsync();
            ViewBag.oitavoA = oitavoA.Count;
            var oitavoB = await _contexto.AlunoAtual.Where(p => p.anoSerie == 8 && p.turma.Equals('B') && p.Transferido == false).ToListAsync();
            ViewBag.oitavoB = oitavoB.Count;

            var nonoA = await _contexto.AlunoAtual.Where(p => p.anoSerie == 9 && p.turma.Equals('A') && p.Transferido == false).ToListAsync();
            ViewBag.nonoA = nonoA.Count;
            var nonoB = await _contexto.AlunoAtual.Where(p => p.anoSerie == 9 && p.turma.Equals('B') && p.Transferido == false).ToListAsync();
            ViewBag.nonoB = nonoB.Count;

            var fase3 = await _contexto.AlunoAtual.Where(p => p.CorrecaoDeFluxo.Equals("Sim") && p.FaseProSic == 3 && p.Transferido == false).ToListAsync();
            ViewBag.fase3 = fase3.Count;

            var fase4A = await _contexto.AlunoAtual.Where(p => p.FaseProSic == 4 && p.turma.Equals('A') && p.Transferido == false).ToListAsync();
            ViewBag.fase4A = fase4A.Count;
            var fase4B = await _contexto.AlunoAtual.Where(p => p.FaseProSic == 4 && p.turma.Equals('B') && p.Transferido == false).ToListAsync();
            ViewBag.fase4B = fase4B.Count;

            return View();
        }


        public FileResult listaTurma2U(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 2 && p.turma.Equals('U') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 2º ano U", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 2º ano U.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma3U(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 3 && p.turma.Equals('U') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 3º ano U", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 3º ano U.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma4U(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 4 && p.turma.Equals('U') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 4º ano U", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 4º ano U.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma5U(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 5 && p.turma.Equals('U') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 5º ano U", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 5º ano U.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma6A(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 6 && p.turma.Equals('A') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 6º ano A", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 6º ano A.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma6B(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 6 && p.turma.Equals('B') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 6º ano B", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 6º ano B.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma7U(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 7 && p.turma.Equals('U') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 7º ano U", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 7º ano U.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma8A(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 8 && p.turma.Equals('A') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 8º ano A", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 8º ano A.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma8B(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 8 && p.turma.Equals('B') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 8º ano B", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 8º ano B.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma9A(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 9 && p.turma.Equals('A') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 9º ano A", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 9º ano A.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurma9B(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.anoSerie == 9 && p.turma.Equals('B') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - 9º ano B", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome, fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista 9º ano B.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurmaF3(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.FaseProSic == 3 && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - Fase 03", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome +" - Série de Origem: "+item.SerieOrigem+"º Ano", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista Fase 03.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurmaF4A(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.FaseProSic == 4 && p.turma.Equals('A') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - Fase 04 A", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome + " - Série de Origem: " + item.SerieOrigem + "º Ano", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista Fase 04 A.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        public FileResult listaTurmaF4B(AlunoAtual aluno)
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
                var fonteOrganizacaoBold = new PdfSharpCore.Drawing.XFont("Calibri", 10, PdfSharpCore.Drawing.XFontStyle.Bold);

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
                var alunos = _contexto.AlunoAtual.Where(p => p.FaseProSic == 4 && p.turma.Equals('B') && p.Transferido == false).OrderBy(q => q.Nome);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("LISTA DE ALUNOS - Fase 04 B", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 150, page.Width, page.Height));
                textFomatter.DrawString(+alunos.Count() + " alunos nesta turma", fonteOrganizacaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 170, page.Width, page.Height));

                var inicio = 225;
                var passo = 15;
                var cont = 1;

                foreach (var item in alunos)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    textFomatter.DrawString(cont + ". " + item.Nome + " - Série de Origem: " + item.SerieOrigem + "º Ano", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio, 575, page.Height));
                    textFomatter.DrawString("___________________________________________________________________________________________________", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(50, inicio + 1, 575, page.Height));
                    inicio = inicio + passo;
                    cont++;
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
                textFomatter.DrawString("Lista emitida em " + dataString, fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 780, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 750, page.Width, page.Height));
                textFomatter.DrawString("Esta lista foi gerada através do SGI da Escola Estadual João de Mattos Carvalho ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("Contato: eejmc.seed@seduc.se.gov.br ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 810, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Right;
                textFomatter.DrawString("SGI-Sistema de Gerenciamento Interno - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Lista Fase 04 B.pdf";
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }
            }
        }

        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> AlunosTransferidos()
        {
            return View(await _contexto.AlunoAtual.ToListAsync());
        }

        //CRIAR
        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult AdicionarAluno()
        {
            return View();
        }
        [Authorize(Roles = "administrador")]
        [HttpPost]
        public async Task<IActionResult> AdicionarAluno(AlunoAtual student)
        {
            if (ModelState.IsValid)
            {
                student.Nome.ToUpper();
                if (student.Pai != null)
                {
                    student.Pai.ToUpper();

                }

                student.Mae.ToUpper();
                student.Endereco.ToUpper();
                student.Transferido = false;


                string uniqueFileName = UploadImagem(student);
                student.UrlFoto = uniqueFileName;
                _contexto.Attach(student);
                _contexto.Entry(student).State = EntityState.Added;
                _contexto.Add(student);
                await _contexto.SaveChangesAsync();
                this.MostrarMensagem($"Aluno cadastrado com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            else

                return View(student);

        }

        //VISUALISAR
        [Authorize(Roles = "usuario, administrador")]
        public ActionResult VisualizarAluno(int? id)
        {
            if (id != null)
            {
                AlunoAtual allunoAtual = _contexto.AlunoAtual.Find(id);
                if (allunoAtual.Telefone == null)
                {
                    allunoAtual.Telefone = "Não encontrado nos nossos registros";
                }
                if (allunoAtual.NumeroDoNis == null)
                {
                    allunoAtual.NumeroDoNis = "Não encontrado nos nossos registros";
                }
                return View(allunoAtual);
            }
            else
                return NotFound();
        }

        //EDITAR
        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult EditarAluno(int? id)
        {
            if (id != null)
            {
                AlunoAtual student = _contexto.AlunoAtual.Find(id);
                return View(student);
            }
            else
                return View();
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public async Task<IActionResult> EditarAluno(int? id, AlunoAtual student)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadImagem(student);
                    student.UrlFoto = uniqueFileName;
                    _contexto.Attach(student);
                    _contexto.Entry(student).State = EntityState.Added;
                    _contexto.Add(student);
                    _contexto.Update(student);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(student);
                }

            }
            else

                return NotFound();
        }

        //APRAGAR
        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult ExcluirAluno(int? id)
        {
            if (id != null)
            {
                AlunoAtual student = _contexto.AlunoAtual.Find(id);
                return View(student);
            }
            else
                return View();
        }
        [Authorize(Roles = "administrador")]
        [HttpPost]
        public async Task<IActionResult> ExcluirAluno(int? id, AlunoAtual student)
        {
            if (id != null)
            {
                _contexto.Remove(student);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound();
        }

        private string UploadImagem(AlunoAtual alunoAtual)
        {
            string uniqueFileName = null;
            if (alunoAtual.FotoDoAluno != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + alunoAtual.FotoDoAluno.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    alunoAtual.FotoDoAluno.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoAlunoAtual(AlunoAtual alunoAtual)
        {
            int numeroDeclaracaoAlunoAtualGerado;

            Random random = new Random();

            numeroDeclaracaoAlunoAtualGerado = random.Next().GetHashCode();
            alunoAtual.numeroDeclaracao = numeroDeclaracaoAlunoAtualGerado;
            return numeroDeclaracaoAlunoAtualGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoAlunoAtual(AlunoAtual alunoAtual)
        {
            string codigoAutenticacaoAlunoAtual = GerarNumeroDeclaracaoAlunoAtual(alunoAtual).ToString("x");
            alunoAtual.codigoAutenticacao = codigoAutenticacaoAlunoAtual;
            return codigoAutenticacaoAlunoAtual;
        }

        //PREENCHER DECLARAÇÃO DE ALUNO REGULAR

        [Authorize(Roles = "usuario, administrador")]
        public FileResult PreencherDeclaracao(AlunoAtual alunoAtual)
        {

            alunoAtual = _contexto.AlunoAtual.Find(alunoAtual.Id);

            alunoAtual.codigoAutenticacao = GerarCodigoDeAutenticacaoAlunoAtual(alunoAtual);
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
                var logo = Path.GetFullPath("wwwroot/Imagens/SGI.jpg");
                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);
                XImage imgLogo = XImage.FromFile(logo);

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
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
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = alunoAtual.turma.ToString();
                string dataNascString = alunoAtual.DataNascimento.ToString("dd/MM/yyyy");

                if (alunoAtual.NumeroDoNis == null)
                {
                    alunoAtual.NumeroDoNis = "Não encontrado em nossos registros!";
                }
                if (alunoAtual.Pai != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() + " e " + alunoAtual.Pai.ToUpper() + ", " +
                        "no ano letivo de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + alunoAtual.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\",e possui frequência regular até esta data. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() +
                        "no ano letivo de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + alunoAtual.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\",e possui frequência regular até esta data. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + alunoAtual.NumeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Código do aluno: " + alunoAtual.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observação: Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + alunoAtual.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + alunoAtual.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://killersd.bsite.net/Declaracao/VerificarAutenticidade, preencha os dados " +
                    "\"Número do documento\" e \"Código de verificação\" com os códigos acima depois clique no botão \"Verificar autenticidade\" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
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
                    var nomeArquivo = "Declaração " + alunoAtual.Nome + ".pdf";
                    //Salvando no banco
                    alunoAtual.dataDeEmissao = DateTime.Now;
                    _contexto.Update(alunoAtual);
                    _contexto.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        //PREENCHER DECLARAÇÃO DE ALUNO PROSIC

        [Authorize(Roles = "usuario, administrador")]
        public FileResult PreencherDeclaracaoProSic(AlunoAtual alunoAtual)
        {
            alunoAtual = _contexto.AlunoAtual.Find(alunoAtual.Id);
            alunoAtual.codigoAutenticacao = GerarCodigoDeAutenticacaoAlunoAtual(alunoAtual);

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
                var logo = Path.GetFullPath("wwwroot/Imagens/SGI.jpg");
                XImage imgBrasao = XImage.FromFile(brasao);
                XImage imgEscudo = XImage.FromFile(escudo);
                XImage imgLogo = XImage.FromFile(logo);

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
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
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = alunoAtual.turma.ToString();
                string dataNascString = alunoAtual.DataNascimento.ToString("dd/MM/yyyy");

                if (alunoAtual.NumeroDoNis == null)
                {
                    alunoAtual.NumeroDoNis = "Não encontrado em nossos registros!";
                }
                if (alunoAtual.Pai != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() + " e " + alunoAtual.Pai.ToUpper() +
                        ", no ano letivo de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino em turma de Fase " + alunoAtual.FaseProSic + " do Programa Sergipe na Idade Certa, tendo como sua turma de origem o " + alunoAtual.SerieOrigem + "º ano, e possui frequência regular até esta data. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() +
                        ", no ano letivo de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino em turma de Fase " + alunoAtual.FaseProSic + " do Programa Sergipe na Idade Certa, tendo como sua turma de origem o " + alunoAtual.SerieOrigem + "º ano, e possui frequência regular até esta data. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + alunoAtual.NumeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Código do aluno: " + alunoAtual.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observação: Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + alunoAtual.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + alunoAtual.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://killersd.bsite.net/Declaracao/VerificarAutenticidade, preencha os dados " +
                    "\"Número do documento\" e \"Código de verificação\" com os códigos acima depois clique no botão \"Verificar autenticidade\" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
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
                    var nomeArquivo = "Declaração " + alunoAtual.Nome + ".pdf";
                    //Salvando no banco
                    alunoAtual.dataDeEmissao = DateTime.Now;
                    _contexto.Update(alunoAtual);
                    _contexto.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        //PREENCHER DECLARAÇÃO DE TRANSFERÊNCIA ALUNO REGULAR
        [Authorize(Roles = "usuario, administrador")]
        public FileResult DeclaracaoTransferencia(AlunoAtual alunoAtual)
        {
            alunoAtual = _contexto.AlunoAtual.Find(alunoAtual.Id);
            alunoAtual.Transferido = true;
            alunoAtual.codigoAutenticacao = GerarCodigoDeAutenticacaoAlunoAtual(alunoAtual);

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
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = alunoAtual.turma.ToString();
                string dataNascString = alunoAtual.DataNascimento.ToString("dd/MM/yyyy");

                if (alunoAtual.NumeroDoNis == null)
                {
                    alunoAtual.NumeroDoNis = "Não encontrado em nossos registros!";
                }
                if (alunoAtual.Pai != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o(a) aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() + " e " + alunoAtual.Pai.ToUpper() + ", " +
                        "no ano letivo de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + alunoAtual.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e nesta data (" + DateTime.Now.ToString("dd/MM/yyyy") + ") seu responsável legal solicitou transferência do(a) discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() + ", no ano letivo " +
                        " de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + alunoAtual.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e nesta data (" + DateTime.Now.ToString("dd/MM/yyyy") + ") seu responsável legal solicitou transferência do discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + alunoAtual.NumeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Código do aluno: " + alunoAtual.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observações:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 675, page.Width, page.Height));
                textFomatter.DrawString("1. O período para a confecção da transferência é de até 30 dias, caso a documentação do aluno esteja em dias, caso contrário, pode ultrapassar esse prazo. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 695, page.Width, page.Height));
                textFomatter.DrawString("2. Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + alunoAtual.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + alunoAtual.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://killersd.bsite.net/Declaracao/VerificarAutenticidade, preencha os dados " +
                    "\"Número do documento\" e \"Código de verificação\" com os códigos acima depois clique no botão \"Verificar autenticidade\" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
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
                    EnviarConfirmacaoTransferenciaRegular(alunoAtual);
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + alunoAtual.Nome + ".pdf";
                    //Salvando no banco
                    alunoAtual.dataDeEmissao = DateTime.Now;
                    _contexto.Update(alunoAtual);
                    _contexto.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        //PREENCHER DECLARAÇÃO DE TRANSFERÊNCIA ALUNO PROSIC
        [Authorize(Roles = "usuario, administrador")]
        public FileResult DeclaracaoTransferenciaProSic(AlunoAtual alunoAtual)
        {
            alunoAtual = _contexto.AlunoAtual.Find(alunoAtual.Id);
            alunoAtual.Transferido = true;
            alunoAtual.codigoAutenticacao = GerarCodigoDeAutenticacaoAlunoAtual(alunoAtual);

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
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = alunoAtual.turma.ToString();
                string dataNascString = alunoAtual.DataNascimento.ToString("dd/MM/yyyy");

                if (alunoAtual.NumeroDoNis == null)
                {
                    alunoAtual.NumeroDoNis = "Não encontrado em nossos registros!";
                }
                if (alunoAtual.Pai != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o(a) aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() + " e " + alunoAtual.Pai.ToUpper() + ", " +
                        " no ano letivo " +
                        "de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino em turma de Correção de Fluxo, Fase " + alunoAtual.FaseProSic + " do Programa Sergipe na Idade Certa, tendo como turma de origem o " + alunoAtual.SerieOrigem + "º ano, e nesta data (" + DateTime.Now.ToString("dd/MM/yyyy") + ") seu responsável legal solicitou transferência do(a) discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() + ", no ano letivo " +
                        "de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino em turma de Correção de Fluxo, Fase " + alunoAtual.FaseProSic + " do Programa Sergipe na Idade Certa, tendo como turma de origem o " + alunoAtual.SerieOrigem + "º ano, e nesta data (" + DateTime.Now.ToString("dd/MM/yyyy") + ") seu responsável legal solicitou transferência do(a) discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + alunoAtual.NumeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Código do aluno: " + alunoAtual.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observações:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 675, page.Width, page.Height));
                textFomatter.DrawString("1. O período para a confecção da transferência é de até 30 dias, caso a documentação do aluno esteja em dias, caso contrário, pode ultrapassar esse prazo. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 695, page.Width, page.Height));
                textFomatter.DrawString("2. Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + alunoAtual.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + alunoAtual.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://killersd.bsite.net/Declaracao/VerificarAutenticidade, preencha os dados " +
                    "\"Número do documento\" e \"Código de verificação\" com os códigos acima depois clique no botão \"Verificar autenticidade\" ", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 635, page.Width, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                string dataString = DateTime.Now.ToString("dd/MM/yyyy");
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
                    EnviarConfirmacaoTransferenciaProSic(alunoAtual);

                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + alunoAtual.Nome + ".pdf";
                    //Salvando no banco
                    alunoAtual.dataDeEmissao = DateTime.Now;
                    _contexto.Update(alunoAtual);
                    _contexto.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> ReverterTransferencia(AlunoAtual alunoAtual, int Id)
        {
            alunoAtual = await _contexto.AlunoAtual.FindAsync(Id);
            alunoAtual.Transferido = false;
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EnviarConfirmacaoTransferenciaProSic(AlunoAtual aluno)
        {
            var usuario = User.Identity.Name;
            var mensagem = new StringBuilder();
            mensagem.Append($"<p>Olá!</p>");
            mensagem.Append("<p> O(a) Usuário(a)<b> " + usuario + "</b> acaba de emitir uma declaração de transferência para" +
                " o(a) aluno(a) cujos dados estão discriminados abaixo:</p>");
            mensagem.Append("<p><b>Ano letivo:</b> " + aluno.anoLetivo + "</p>");
            mensagem.Append("<p><b>Código:</b> " + aluno.codigoSeed + "</p>");
            mensagem.Append("<p><b>Nome:</b> " + aluno.Nome + "</p>");
            mensagem.Append("<p><b>Mãe:</b> " + aluno.Mae + "</p>");
            mensagem.Append("<p><b>Pai:</b> " + aluno.Pai + "</p>");
            mensagem.Append("<p><b>Data de Nascimento: </b>" + aluno.DataNascimento + "</p>");
            mensagem.Append("<p><b>Endereço:</b> " + aluno.Endereco + "</p>");
            mensagem.Append("<p><b>Número do NIS:</b> " + aluno.NumeroDoNis + "</p>");
            mensagem.Append("<p><b>Fase do ProSic:</b> " + aluno.FaseProSic + "</p>");
            mensagem.Append("<p><b>Série de origem:</b> " + aluno.SerieOrigem + "º ano do Ensino Fundamental</p>");
            mensagem.Append("<p></p>");
            mensagem.Append("<p>Favor verificar para o caso da declaração ter sido emitida por engano pelo usuário mencionado anteriormente!</p>");
            mensagem.Append("<p></p>");
            mensagem.Append("<p></p>");
            mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte do SGI-JMC</p>");
            _emailService.SendEmailAsync("alex_underline@hotmail.com",
               "Transferência de aluno", "", mensagem.ToString()).Wait();
            return View(nameof(Index));
        }

        public IActionResult EnviarConfirmacaoTransferenciaRegular(AlunoAtual aluno)
        {
            var usuario = User.Identity.Name;
            var mensagem = new StringBuilder();
            mensagem.Append($"<p>Olá!</p>");
            mensagem.Append("<p> O(a) Usuário(a)<b> " + usuario + "</b> acaba de emitir uma declaração de transferência para" +
                " o(a) aluno(a) cujos dados estão discriminados abaixo:</p>");
            mensagem.Append("<p><b>Ano letivo:</b> " + aluno.anoLetivo + "</p>");
            mensagem.Append("<p><b>Código:</b> " + aluno.codigoSeed + "</p>");
            mensagem.Append("<p><b>Nome:</b> " + aluno.Nome + "</p>");
            mensagem.Append("<p><b>Mãe:</b> " + aluno.Mae + "</p>");
            mensagem.Append("<p><b>Pai:</b> " + aluno.Pai + "</p>");
            mensagem.Append("<p><b>Data de Nascimento: </b>" + aluno.DataNascimento + "</p>");
            mensagem.Append("<p><b>Endereço:</b> " + aluno.Endereco + "</p>");
            mensagem.Append("<p><b>Número do NIS:</b> " + aluno.NumeroDoNis + "</p>");
            mensagem.Append("<p><b>Ano/Série:</b> " + aluno.anoSerie + "º ano do Ensino Fundamental</p>");
            mensagem.Append("<p><b>Turma:</b> " + aluno.turma + "</p>");
            mensagem.Append("<p></p>");
            mensagem.Append("<p>Favor verificar para o caso da declaração ter sido emitida por engano pelo usuário mencionado anteriormente!</p>");
            mensagem.Append("<p></p>");
            mensagem.Append("<p></p>");
            mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte do SGI-JMC</p>");
            _emailService.SendEmailAsync("alex_underline@hotmail.com",
                "Transferência de aluno", "", mensagem.ToString()).Wait();
            return View(nameof(Index));

        }
    }
}
