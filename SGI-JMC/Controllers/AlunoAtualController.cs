using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace SGI_JMC.Controllers
{
    public class AlunoAtualController : Controller
    {
        private readonly Context _contexto;

        public AlunoAtualController(Context context)
        {
            _contexto = context;
        }
        [Authorize(Roles = "usuario, administrador")]
        public async Task<IActionResult> Index()
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
                student.Pai.ToUpper();
                student.Mae.ToUpper();
                student.Endereco.ToUpper();
                student.Transferido = false;

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
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + alunoAtual.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + alunoAtual.Mae.ToUpper() + " e " + alunoAtual.Pai.ToUpper()  +
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
                        "de " + alunoAtual.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino em turma de Correção de Fluxo, Fase " + alunoAtual.FaseProSic + " do Programa Sergipe na Idade Certa, tendo como turma de origem o " + alunoAtual.SerieOrigem+ "º ano, e nesta data (" + DateTime.Now.ToString("dd/MM/yyyy") + ") seu responsável legal solicitou transferência do(a) discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
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


    }
}
