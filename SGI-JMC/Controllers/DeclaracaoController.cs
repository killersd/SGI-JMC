using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using SGI_JMC.Data;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using SGI_JMC.ViewModels;

namespace SGI_JMC.Controllers
{
    [Authorize(Roles = "usuario, administrador")]
    public class DeclaracaoController : Controller
    {
        private readonly Context _context;

        public DeclaracaoController(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Declaracao.ToListAsync());
        }

        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> IndexTransferenciaRegular()
        {
            return View(await _context.DeclaracaoTransferenciaRegular.ToListAsync());
        }
        


        //Métodos auxiliares
        [Authorize(Roles = "usuario, administrador")]
        private bool DeclaracaoExists(int id)
        {
            return _context.Declaracao.Any(e => e.Id == id);
        }

        [Authorize(Roles = "usuario, administrador")]
        private bool EntidadeExiste(string codAut)
        {
            return (_context.Declaracao.AsNoTracking().Any(u => u.codigoAutenticacao == codAut));
        }

        //Métodos para verificar a autenticidade das declarações
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> VerificarAutenticidade(
       [FromForm] VerificarAutenticidadeViewModel verificar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     if ((_context.Declaracao.Any(u => u.codigoAutenticacao == verificar.CodigoDeVerificacao) &&
                        (_context.Declaracao.Any(u => u.numeroDeclaracao == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoProSic.Any(u => u.codigoAutenticacao == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoProSic.Any(u => u.numeroDeclaracao == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoTransferenciaRegular.Any(u => u.codigoAutenticacao == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoTransferenciaRegular.Any(u => u.numeroDeclaracao == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoTransferenciaProSic.Any(u => u.codigoAutenticacao == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoTransferenciaProSic.Any(u => u.numeroDeclaracao == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoSabadoLetivo.Any(u => u.codigoAutenticacaoSabado == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoSabadoLetivo.Any(u => u.numeroDeclaracaoSabado == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoExServidor.Any(u => u.codigoAutenticacaoExServidor == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoExServidor.Any(u => u.numeroDeclaracaoExServidor == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoServidor.Any(u => u.codigoAutenticacaoServidor == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoServidor.Any(u => u.numeroDeclaracaoServidor == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoConcludentesRegular.Any(u => u.codigoAutenticacaoConcludente == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoConcludentesRegular.Any(u => u.numeroDeclaracaoConcludente == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoConcludentesProSic.Any(u => u.codigoAutenticacaoConcludenteProSic == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoConcludentesProSic.Any(u => u.numeroDeclaracaoConcludenteProSic == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoExAluno.Any(u => u.CodigoAutenticacao == verificar.CodigoDeVerificacao) &&
                        (_context.DeclaracaoExAluno.Any(u => u.NumeroDeclaracao == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoDistanciaInteriorizacao.Any(u => u.CodigoAutenticacaoDeclaracaoInteriorizacao == verificar.CodigoDeVerificacao) &&
                      (_context.DeclaracaoDistanciaInteriorizacao.Any(u => u.NumeroDeclaracaoDistanciaInteriorizacao == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }
                    else if ((_context.DeclaracaoResidencia.Any(u => u.CodigoAutenticacaoDeclaracaoResidencia == verificar.CodigoDeVerificacao) &&
                     (_context.DeclaracaoResidencia.Any(u => u.NumeroDeclaracaoResidencia == verificar.NumeroDeclaracao))))
                    {
                        this.MostrarMensagem("ESTE DOCUMENTO É VERDADEIRO E FOI GERADO PELO SGI-EEJMC.");
                        return View(verificar);
                    }


                    this.MostrarMensagem("ESTE DOCUMENTO É FALSO.", true);
                        return View(verificar);
                    
                }
                else
                {
                    return View(verificar);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("VerificarAutenticidade");
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult VerificarAutenticidade()
        {
            return View();
        }

        //Métodos para declaração de frequência de aluno regular 
        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Father_name,Mother_name,Birth_date,anoLetivo,anoSerie,turma,numeroDoNis,codigoSeed,dataDeEmissao")] Declaracao declaracao)
        {
            if (ModelState.IsValid)
            {
                declaracao.dataDeEmissao = DateTime.Now;
                _context.Add(declaracao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracao);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracao(Declaracao declaracao)
        {
            declaracao.codigoAutenticacao = GerarCodigoDeAutenticacao(declaracao);
            float porcentagemDeFaltas = 100 - CalculaFrequenciaGeral(declaracao);
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
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));

                //textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 115, page.Width, page.Height));
                //textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 130, page.Width, page.Height));
                //textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 145, page.Width, page.Height));
                //textFomatter.DrawString("SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 160, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = declaracao.turma.ToString();
                string dataNascString = declaracao.Birth_date.ToString("dd/MM/yyyy");

                if (declaracao.numeroDoNis == null)
                {
                    declaracao.numeroDoNis = "Não encontrado em nossos registros!";
                }
                if (declaracao.Father_name != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracao.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracao.Mother_name.ToUpper() + " e " + declaracao.Father_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracao.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + declaracao.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e da carga horária anual (833 horas), possui frequência de " + porcentagemDeFaltas + "% nesta data.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracao.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracao.Mother_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracao.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + declaracao.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e da carga horária anual (833 horas), possui frequência de " + porcentagemDeFaltas + "% nesta data.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + declaracao.numeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Matrícula SIAE: " + declaracao.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observação: Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + declaracao.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracao.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://sgi-eejmc.azurewebsites.net/Declaracao/VerificarAutenticidade, preencha os dados " +
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
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + declaracao.Name + ".pdf";
                    //Salvando no banco
                    declaracao.dataDeEmissao = DateTime.Now;
                    _context.Add(declaracao);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracao(Declaracao declaracao)
        {
            int numeroDeclaracaoGerado;

            Random random = new Random();

            numeroDeclaracaoGerado = random.Next().GetHashCode();
            declaracao.numeroDeclaracao = numeroDeclaracaoGerado;
            return numeroDeclaracaoGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacao(Declaracao declaracao)
        {
            string codigoAutenticacao = GerarNumeroDeclaracao(declaracao).ToString("x");
            declaracao.codigoAutenticacao = codigoAutenticacao;
            return codigoAutenticacao;
        }

        [Authorize(Roles = "usuario, administrador")]
        public float CalculaFrequenciaGeral(Declaracao declaracao)
        {
            float pctFaltas;
            pctFaltas = (declaracao.qtdFaltas * 100) / 1000;
            return pctFaltas;
        }

        //Métodos para declaração de frequência de aluno ProSic
        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateProSic()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        public async Task<IActionResult> CreateProSic([Bind("Id,Name,Father_name,Mother_name,Birth_date,anoLetivo,anoSerie,turma,numeroDoNis,codigoSeed,dataDeEmissao")] DeclaracaoProSic declaracaoProSic)
        {
            if (ModelState.IsValid)
            {
                declaracaoProSic.dataDeEmissao = DateTime.Now;
                _context.Add(declaracaoProSic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracaoProSic);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracaoProSic(DeclaracaoProSic declaracaoProSic)
        {
            declaracaoProSic.codigoAutenticacao = GerarCodigoDeAutenticacaoProSic(declaracaoProSic);
            float porcentagemDeFaltas = 100 - CalculaFrequenciaGeralProSic(declaracaoProSic);
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
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));

                //textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 115, page.Width, page.Height));
                //textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 130, page.Width, page.Height));
                //textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 145, page.Width, page.Height));
                //textFomatter.DrawString("SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 160, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                string dataNascString = declaracaoProSic.Birth_date.ToString("dd/MM/yyyy");

                if (declaracaoProSic.numeroDoNis == null)
                {
                    declaracaoProSic.numeroDoNis = "Não encontrado em nossos registros!";
                }
                if (declaracaoProSic.Father_name != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoProSic.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoProSic.Mother_name.ToUpper() + " e " + declaracaoProSic.Father_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracaoProSic.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino, na turma de correção de fluxo, fase " + declaracaoProSic.faseProSic + ", do programa " +
                        "Sergipe na Idade Certa, tendo como turma de origem " + declaracaoProSic.serieOrigem + "º ano,  e da carga horária anual (833 horas), possui frequência de " + porcentagemDeFaltas + "% nesta data.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoProSic.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoProSic.Mother_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracaoProSic.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino na turma de correção de fluxo, fase " + declaracaoProSic.faseProSic + " do Programa Sergipe na Idade Certa, tendo como turma de origem " + declaracaoProSic.serieOrigem + "º ano,  e da carga horária anual (833 horas), possui frequência de " + porcentagemDeFaltas + "% nesta data.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + declaracaoProSic.numeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Matrícula SIAE: " + declaracaoProSic.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observação: Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + declaracaoProSic.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoProSic.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://sgi-eejmc.azurewebsites.net/Declaracao/VerificarAutenticidade, preencha os dados " +
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
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + declaracaoProSic.Name + ".pdf";
                    //Salvando no banco
                    declaracaoProSic.dataDeEmissao = DateTime.Now;
                    _context.Add(declaracaoProSic);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoProSic(DeclaracaoProSic declaracaoProSic)
        {
            int numeroDeclaracaoGerado;

            Random random = new Random();

            numeroDeclaracaoGerado = random.Next().GetHashCode();
            declaracaoProSic.numeroDeclaracao = numeroDeclaracaoGerado;
            return numeroDeclaracaoGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoProSic(DeclaracaoProSic declaracaoProSic)
        {
            string codigoAutenticacao = GerarNumeroDeclaracaoProSic(declaracaoProSic).ToString("x");
            declaracaoProSic.codigoAutenticacao = codigoAutenticacao;
            return codigoAutenticacao;
        }

        [Authorize(Roles = "usuario, administrador")]
        public float CalculaFrequenciaGeralProSic(DeclaracaoProSic declaracao)
        {
            float pctFaltas;
            pctFaltas = (declaracao.qtdFaltas * 100) / 1000;
            return pctFaltas;
        }

        //Métodos para declaração de transferência de aluno Regular
        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateDeclaracaoTransferenciaRegular()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        public async Task<IActionResult> CreateDeclaracaoTransferenciaRegular([Bind("Id,Name,Father_name,Mother_name,Birth_date,anoLetivo,anoSerie,turma,numeroDoNis,codigoSeed,dataDeEmissao")] DeclaracaoTransferenciaRegular declaracaoTransferenciaRegular)
        {
            if (ModelState.IsValid)
            {
                declaracaoTransferenciaRegular.dataDeEmissao = DateTime.Now;
                _context.Add(declaracaoTransferenciaRegular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracaoTransferenciaRegular);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracaoTransferenciaAlunoRegular(DeclaracaoTransferenciaRegular declaracaoTransferenciaRegular)
        {
            declaracaoTransferenciaRegular.codigoAutenticacao = GerarCodigoDeAutenticacaoTransferenciaRegular(declaracaoTransferenciaRegular);
            //float porcentagemDeFaltas = 100 - CalculaFrequenciaGeral(declaracaoTransferenciaRegular);
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
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));

                //textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 115, page.Width, page.Height));
                //textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 130, page.Width, page.Height));
                //textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 145, page.Width, page.Height));
                //textFomatter.DrawString("SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 160, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = declaracaoTransferenciaRegular.turma.ToString();
                string dataNascString = declaracaoTransferenciaRegular.Birth_date.ToString("dd/MM/yyyy");

                if (declaracaoTransferenciaRegular.numeroDoNis == null)
                {
                    declaracaoTransferenciaRegular.numeroDoNis = "Não encontrado em nossos registros!";
                }
                if (declaracaoTransferenciaRegular.Father_name != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoTransferenciaRegular.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoTransferenciaRegular.Mother_name.ToUpper() + " e " + declaracaoTransferenciaRegular.Father_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracaoTransferenciaRegular.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + declaracaoTransferenciaRegular.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e nesta data (" + DateTime.Now.ToShortDateString() + ") seu responsável legal solicitou transferência do discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoTransferenciaRegular.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoTransferenciaRegular.Mother_name.ToUpper() + ", no ano letivo " +
                        " de " + declaracaoTransferenciaRegular.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino no " + declaracaoTransferenciaRegular.anoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e nesta data (" + DateTime.Now.ToShortDateString() + ") seu responsável legal solicitou transferência do discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + declaracaoTransferenciaRegular.numeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Matrícula SIAE: " + declaracaoTransferenciaRegular.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observações:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 675, page.Width, page.Height));
                textFomatter.DrawString("1. O período para a confecção da transferência é de até 30 dias, caso a documentação do aluno esteja em dias, caso contrário, pode ultrapassar esse prazo. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 695, page.Width, page.Height));
                textFomatter.DrawString("2. Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + declaracaoTransferenciaRegular.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoTransferenciaRegular.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://sgi-eejmc.azurewebsites.net/Declaracao/VerificarAutenticidade, preencha os dados " +
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
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + declaracaoTransferenciaRegular.Name + ".pdf";
                    //Salvando no banco
                    declaracaoTransferenciaRegular.dataDeEmissao = DateTime.Now;
                    _context.Add(declaracaoTransferenciaRegular);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoTransferenciaRegular(DeclaracaoTransferenciaRegular declaracaoTransferenciaRegular)
        {
            int numeroDeclaracaoGerado;

            Random random = new Random();

            numeroDeclaracaoGerado = random.Next().GetHashCode();
            declaracaoTransferenciaRegular.numeroDeclaracao = numeroDeclaracaoGerado;
            return numeroDeclaracaoGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoTransferenciaRegular(DeclaracaoTransferenciaRegular declaracaoTransferenciaRegular)
        {
            string codigoAutenticacao = GerarNumeroDeclaracaoTransferenciaRegular(declaracaoTransferenciaRegular).ToString("x");
            declaracaoTransferenciaRegular.codigoAutenticacao = codigoAutenticacao;
            return codigoAutenticacao;
        }


        //Métodos para declaração de transferência de aluno ProSic
        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateDeclaracaoTransferenciaProSic()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        public async Task<IActionResult> CreateDeclaracaoTransferenciaProSic([Bind("Id,Name,Father_name,Mother_name,Birth_date,anoLetivo,anoSerie,turma,numeroDoNis,codigoSeed,dataDeEmissao")] DeclaracaoTransferenciaProSic declaracaoTransferenciaProSic)
        {
            if (ModelState.IsValid)
            {
                declaracaoTransferenciaProSic.dataDeEmissao = DateTime.Now;
                _context.Add(declaracaoTransferenciaProSic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracaoTransferenciaProSic);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracaoTransferenciaProSic(DeclaracaoTransferenciaProSic declaracaoTransferenciaProSic)
        {
            declaracaoTransferenciaProSic.codigoAutenticacao = GerarCodigoDeAutenticacaoTransferenciaProSic(declaracaoTransferenciaProSic);
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

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));

                //textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 115, page.Width, page.Height));
                //textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 130, page.Width, page.Height));
                //textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 145, page.Width, page.Height));
                //textFomatter.DrawString("SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 160, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                string dataNascString = declaracaoTransferenciaProSic.Birth_date.ToString("dd/MM/yyyy");

                if (declaracaoTransferenciaProSic.numeroDoNis == null)
                {
                    declaracaoTransferenciaProSic.numeroDoNis = "Não encontrado em nossos registros!";
                }
                if (declaracaoTransferenciaProSic.Father_name != null)
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoTransferenciaProSic.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoTransferenciaProSic.Mother_name.ToUpper() + " e " + declaracaoTransferenciaProSic.Father_name.ToUpper() + ", " +
                        "no ano letivo de " + declaracaoTransferenciaProSic.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino, na turma de correção de fluxo, fase " + declaracaoTransferenciaProSic.faseProSic + ", do programa " +
                        "Sergipe na Idade Certa, tendo como turma de origem " + declaracaoTransferenciaProSic.serieOrigem + "º ano,  e nesta data (" + DateTime.Now.ToShortDateString() + ") seu responsável legal solicitou transferência do discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }
                else
                {
                    textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                    textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoTransferenciaProSic.Name.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoTransferenciaProSic.Mother_name.ToUpper() + " e no ano letivo de " + declaracaoTransferenciaProSic.anoLetivo + ", encontra-se matriculado(a) nesta Unidade de Ensino, na " +
                        "turma de correção de fluxo, fase " + declaracaoTransferenciaProSic.faseProSic + ", do programa Sergipe na Idade Certa, tendo como turma de origem " + declaracaoTransferenciaProSic.serieOrigem + "º ano,  e nesta data (" + DateTime.Now.ToShortDateString() + ") seu responsável legal solicitou transferência do discente para outra Unidade de Ensino.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                }


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + declaracaoTransferenciaProSic.numeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Matrícula SIAE: " + declaracaoTransferenciaProSic.codigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observações:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 675, page.Width, page.Height));
                textFomatter.DrawString("1. O período para a confecção da transferência é de até 30 dias, caso a documentação do aluno esteja em dias, caso contrário, pode ultrapassar esse prazo. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 695, page.Width, page.Height));
                textFomatter.DrawString("2. Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));


                textFomatter.DrawString("Número do documento: " + declaracaoTransferenciaProSic.numeroDeclaracao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoTransferenciaProSic.codigoAutenticacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://sgi-eejmc.azurewebsites.net/Declaracao/VerificarAutenticidade, preencha os dados " +
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
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração " + declaracaoTransferenciaProSic.Name + ".pdf";
                    //Salvando no banco
                    declaracaoTransferenciaProSic.dataDeEmissao = DateTime.Now;
                    _context.Add(declaracaoTransferenciaProSic);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoTransferenciaProSic(DeclaracaoTransferenciaProSic declaracaoTransferenciaProSic)
        {
            int numeroDeclaracaoGerado;

            Random random = new Random();

            numeroDeclaracaoGerado = random.Next().GetHashCode();
            declaracaoTransferenciaProSic.numeroDeclaracao = numeroDeclaracaoGerado;
            return numeroDeclaracaoGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoTransferenciaProSic(DeclaracaoTransferenciaProSic declaracaoTransferenciaProSic)
        {
            string codigoAutenticacao = GerarNumeroDeclaracaoTransferenciaProSic(declaracaoTransferenciaProSic).ToString("x");
            declaracaoTransferenciaProSic.codigoAutenticacao = codigoAutenticacao;
            return codigoAutenticacao;
        }

        [Authorize(Roles = "usuario, administrador")]
        public float CalculaFrequenciaTransferenciaProSic(DeclaracaoTransferenciaProSic declaracaoTransferenciaProSic)
        {
            float pctFaltas;
            pctFaltas = (declaracaoTransferenciaProSic.qtdFaltas * 100) / 1000;
            return pctFaltas;
        }

        //Métodos para declaração concludentes ProSic 
        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateDeclaracaoConcludentesProSic()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDeclaracaoConcludentesProSic([Bind("Id,Nome,NomeDoPai,NomeDaMae,DataNascimento,AnoLetivo,FaseProSic,SerieOrigem,numeroDoNis,CodigoSeed,DataDeEmissao, ResultadoFinal")] DeclaracaoConcludentesRegular declaracaoConcludentesRegular)
        {
            if (ModelState.IsValid)
            {
                declaracaoConcludentesRegular.DataDeEmissao = DateTime.Now;
                _context.Add(declaracaoConcludentesRegular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracaoConcludentesRegular);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracaoConcludentesProSic(DeclaracaoConcludentesProSic declaracaoConcludentesProSic)
        {
            declaracaoConcludentesProSic.codigoAutenticacaoConcludenteProSic = GerarCodigoDeAutenticacaoConcludentesProSic(declaracaoConcludentesProSic);
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
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO DE CONCLUSÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                string dataNascString = declaracaoConcludentesProSic.DataNascimento.ToString("dd/MM/yyyy");

                if (declaracaoConcludentesProSic.NumeroDoNis == null)
                {
                    declaracaoConcludentesProSic.NumeroDoNis = "Não encontrado em nossos registros!";
                }

                if ((declaracaoConcludentesProSic.ResultadoFinal.Equals("Aprovado")) || (declaracaoConcludentesProSic.ResultadoFinal.Equals("Acelerado")))
                {
                    if (declaracaoConcludentesProSic.NomeDoPai != null)
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesProSic.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesProSic.NomeDaMae.ToUpper() + " e " + declaracaoConcludentesProSic.NomeDoPai.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesProSic.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino em turma de correção de fluxo Fase " + declaracaoConcludentesProSic.FaseProSic + " (8º e 9º anos), do programa Sergipe na Idade Certa, tendo como sua turma de origem o " + declaracaoConcludentesProSic.SerieDeOrigem + "º ano e ao final do ano letivo foi " + declaracaoConcludentesProSic.ResultadoFinal + ", tendo direito a matricular-se no 1º Ano do Ensino Médio.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                    else
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesProSic.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesProSic.NomeDaMae.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesProSic.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino em turma de correção de fluxo Fase " + declaracaoConcludentesProSic.FaseProSic + " (8º e 9º anos), do programa Sergipe na Idade Certa, tendo como sua turma de origem o " + declaracaoConcludentesProSic.SerieDeOrigem + "º ano e ao final do ano letivo foi " + declaracaoConcludentesProSic.ResultadoFinal + ", tendo direito a matricular-se no 1º Ano do Ensino Médio.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                }
                else
                {
                    if (declaracaoConcludentesProSic.NomeDoPai != null)
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesProSic.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesProSic.NomeDaMae.ToUpper() + " e " + declaracaoConcludentesProSic.NomeDoPai.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesProSic.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino em turma de correção de fluxo Fase " + declaracaoConcludentesProSic.FaseProSic + " (8º e 9º anos), do programa Sergipe na Idade Certa, tendo como sua turma de origem o " + declaracaoConcludentesProSic.SerieDeOrigem + "º ano e ao final do ano letivo foi " + declaracaoConcludentesProSic.ResultadoFinal + ", tendo direito a matricular-se no " + declaracaoConcludentesProSic.SerieDeOrigem + "º Ano do Ensino Fundamental.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                    else
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesProSic.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesProSic.NomeDaMae.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesProSic.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino em turma de correção de fluxo Fase " + declaracaoConcludentesProSic.FaseProSic + " (8º e 9º anos), do programa Sergipe na Idade Certa, tendo como sua turma de origem o " + declaracaoConcludentesProSic.SerieDeOrigem + "º ano e ao final do ano letivo foi " + declaracaoConcludentesProSic.ResultadoFinal + ", tendo direito a matricular-se no " + declaracaoConcludentesProSic.SerieDeOrigem + "º Ano do Ensino Fundamental.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                }
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + declaracaoConcludentesProSic.NumeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Matrícula SIAE: " + declaracaoConcludentesProSic.CodigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observações:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 675, page.Width, page.Height));
                textFomatter.DrawString("1. O período para a confecção do certificado de conclusão é de até 30 dias, caso a documentação do aluno esteja em dias, caso contrário, pode ultrapassar esse prazo. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 695, page.Width, page.Height));
                textFomatter.DrawString("2. Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 510, page.Width, 80, 10, 10);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Informações adicionais", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 510, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                if (declaracaoConcludentesProSic.Observacao == null)
                {
                    declaracaoConcludentesProSic.Observacao = "Sem informações adicionais!";
                }
                textFomatter.DrawString(declaracaoConcludentesProSic.Observacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(7, 525, 575, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;

                textFomatter.DrawString("Número do documento: " + declaracaoConcludentesProSic.numeroDeclaracaoConcludenteProSic, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoConcludentesProSic.codigoAutenticacaoConcludenteProSic, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://sgi-eejmc.azurewebsites.net/Declaracao/VerificarAutenticidade, preencha os dados " +
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
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração de conclusão " + declaracaoConcludentesProSic.Nome + ".pdf";
                    //Salvando no banco
                    declaracaoConcludentesProSic.DataDeEmissao = DateTime.Now;
                    _context.Add(declaracaoConcludentesProSic);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoConcludentesProSic(DeclaracaoConcludentesProSic declaracaoConcludentesProSic)
        {
            int numeroDeclaracaoConcludenteProSicGerado;

            Random random = new Random();

            numeroDeclaracaoConcludenteProSicGerado = random.Next().GetHashCode();
            declaracaoConcludentesProSic.numeroDeclaracaoConcludenteProSic = numeroDeclaracaoConcludenteProSicGerado;
            return numeroDeclaracaoConcludenteProSicGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoConcludentesProSic(DeclaracaoConcludentesProSic declaracaoConcludentesProSic)
        {
            string codigoAutenticacaoConcludentesProSic = GerarNumeroDeclaracaoConcludentesProSic(declaracaoConcludentesProSic).ToString("x");
            declaracaoConcludentesProSic.codigoAutenticacaoConcludenteProSic = codigoAutenticacaoConcludentesProSic;
            return codigoAutenticacaoConcludentesProSic;
        }

        //Métodos para declaração concludentes regular 
        [Authorize(Roles = "usuario, administrador")]
        [HttpGet]
        public IActionResult CreateDeclaracaoConcludentesRegular()
        {
            return View();
        }

        [Authorize(Roles = "usuario, administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDeclaracaoConcludentesRegular([Bind("Id,Nome,NomeDoPai,NomeDaMae,DataNascimento,AnoLetivo,AnoSerie,Turma,numeroDoNis,CodigoSeed,DataDeEmissao, ResultadoFinal")] DeclaracaoConcludentesRegular declaracaoConcludentesRegular)
        {
            if (ModelState.IsValid)
            {
                declaracaoConcludentesRegular.DataDeEmissao = DateTime.Now;
                _context.Add(declaracaoConcludentesRegular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(declaracaoConcludentesRegular);
        }

        [Authorize(Roles = "usuario, administrador")]
        public FileResult gerarDeclaracaoConcludentesRegular(DeclaracaoConcludentesRegular declaracaoConcludentesRegular)
        {
            declaracaoConcludentesRegular.codigoAutenticacaoConcludente = GerarCodigoDeAutenticacaoConcludentesRegular(declaracaoConcludentesRegular);
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
                //graphics.DrawImage(imgBrasao, 275, 20, 50, 75);
                graphics.DrawImage(imgBrasao, 0, 30, 50, 75);
                graphics.DrawImage(imgEscudo, 75, 280, 450, 450);

                textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 30, page.Width, page.Height));
                textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 45, page.Width, page.Height));
                textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 60, page.Width, page.Height));
                textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 75, page.Width, page.Height));
                textFomatter.DrawString("PRAÇA ABEL JACÓ DOS SANTOS, Nº 892, CENTRO, SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(55, 90, page.Width, page.Height));
                textFomatter.DrawString("_____________________________________________________________________________________", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));

                //textFomatter.DrawString("GOVERNO DO ESTADO DE SERGIPE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 100, page.Width, page.Height));
                //textFomatter.DrawString("SECRETARIA DE ESTADO DA EDUCAÇÃO, DO ESPORTE E DA CULTURA", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 115, page.Width, page.Height));
                //textFomatter.DrawString("ESCOLA ESTADUAL JOÃO DE MATTOS CARVALHO", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 130, page.Width, page.Height));
                //textFomatter.DrawString("CNPJ: 01.902.194/0001-83", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 145, page.Width, page.Height));
                //textFomatter.DrawString("SIMÃO DIAS - SE", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 160, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("DECLARAÇÃO DE CONCLUSÃO", fonteTitulo, corFonte, new PdfSharpCore.Drawing.XRect(0, 200, page.Width, page.Height));

                //melhorar isso aqui
                string turmaString = declaracaoConcludentesRegular.Turma.ToString();
                string dataNascString = declaracaoConcludentesRegular.DataNascimento.ToString("dd/MM/yyyy");

                if (declaracaoConcludentesRegular.NumeroDoNis == null)
                {
                    declaracaoConcludentesRegular.NumeroDoNis = "Não encontrado em nossos registros!";
                }

                if (declaracaoConcludentesRegular.ResultadoFinal.Equals("Aprovado"))
                {
                    if (declaracaoConcludentesRegular.NomeDoPai != null)
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesRegular.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesRegular.NomeDaMae.ToUpper() + " e " + declaracaoConcludentesRegular.NomeDoPai.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesRegular.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino no " + declaracaoConcludentesRegular.AnoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e ao final do ano foi " + declaracaoConcludentesRegular.ResultadoFinal + ", tendo direito a matricular-se no 1º Ano do Ensino Médio.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                    else
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesRegular.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesRegular.NomeDaMae.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesRegular.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino no " + declaracaoConcludentesRegular.AnoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e ao final do ano foi " + declaracaoConcludentesRegular.ResultadoFinal + ", tendo direito a matricular-se no 1º Ano do Ensino Médio.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                }
                else
                {
                    if (declaracaoConcludentesRegular.NomeDoPai != null)
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesRegular.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesRegular.NomeDaMae.ToUpper() + " e " + declaracaoConcludentesRegular.NomeDoPai.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesRegular.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino no " + declaracaoConcludentesRegular.AnoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e ao final do ano foi " + declaracaoConcludentesRegular.ResultadoFinal + ", tendo direito a matricular-se no 9º Ano do Ensino Fundamental.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                    else
                    {
                        textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                        textFomatter.DrawString("Declaro para os devidos fins que o aluno(a) " + declaracaoConcludentesRegular.Nome.ToUpper() + ", nascido(a) em " + dataNascString + ", filho(a) de " + declaracaoConcludentesRegular.NomeDaMae.ToUpper() + ", " +
                            "no ano letivo de " + declaracaoConcludentesRegular.AnoLetivo + ", foi matriculado(a) nesta Unidade de Ensino no " + declaracaoConcludentesRegular.AnoSerie + "º ano, turma \"" + turmaString.ToUpper() + "\" e ao final do ano foi " + declaracaoConcludentesRegular.ResultadoFinal + ", tendo direito a matricular-se no 9º Ano do Ensino Fundamental.", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 270, page.Width, page.Height));
                    }
                }

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                textFomatter.DrawString("NIS: " + declaracaoConcludentesRegular.NumeroDoNis, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 400, page.Width, page.Height));
                textFomatter.DrawString("Matrícula SIAE: " + declaracaoConcludentesRegular.CodigoSeed, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 415, page.Width, page.Height));
                textFomatter.DrawString("Observações:", fonteDesricaoBold, corFonte, new PdfSharpCore.Drawing.XRect(0, 675, page.Width, page.Height));
                textFomatter.DrawString("1. O período para a confecção do certificado de conclusão é de até 30 dias, caso a documentação do aluno esteja em dias, caso contrário, pode ultrapassar esse prazo. ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 695, page.Width, page.Height));
                textFomatter.DrawString("2. Esta declaração não contém emendas nem rasuras e é válida por um período de 30 dias ", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 730, page.Width, page.Height));


                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                textFomatter.DrawString("__________________________________________________________", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 470, page.Width, page.Height));
                textFomatter.DrawString("Equipe Diretiva", fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 480, page.Width, page.Height));
                graphics.DrawRoundedRectangle(PdfSharpCore.Drawing.XPens.Black, PdfSharpCore.Drawing.XBrushes.Transparent, 0, 510, page.Width, 80, 10, 10);
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                textFomatter.DrawString("Informações adicionais", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(5, 510, page.Width, page.Height));
                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Justify;
                if (declaracaoConcludentesRegular.Observacao == null)
                {
                    declaracaoConcludentesRegular.Observacao = "Sem informações adicionais!";
                }
                textFomatter.DrawString(declaracaoConcludentesRegular.Observacao, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(7, 525, 575, page.Height));

                textFomatter.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;

                textFomatter.DrawString("Número do documento: " + declaracaoConcludentesRegular.numeroDeclaracaoConcludente, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 600, page.Width, page.Height));
                textFomatter.DrawString("Código de verificação: " + declaracaoConcludentesRegular.codigoAutenticacaoConcludente, fonteDesricao, corFonte, new PdfSharpCore.Drawing.XRect(0, 613, page.Width, page.Height));
                textFomatter.DrawString("Para verificar a autenticidade deste documento acesse: https://sgi-eejmc.azurewebsites.net/Declaracao/VerificarAutenticidade, preencha os dados " +
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
                textFomatter.DrawString("SGI-Sistema de Gestão Interna - EEJMC ", fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 30, page.Width, page.Height));
                textFomatter.DrawString("Usuário: " + User.Identity.Name, fonteRodape, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";
                    doc.Save(stream, false);
                    var nomeArquivo = "Declaração de conclusão " + declaracaoConcludentesRegular.Nome + ".pdf";
                    //Salvando no banco
                    declaracaoConcludentesRegular.DataDeEmissao = DateTime.Now;
                    _context.Add(declaracaoConcludentesRegular);
                    _context.SaveChangesAsync();
                    return File(stream.ToArray(), contentType, nomeArquivo);
                }

            }
        }

        [Authorize(Roles = "usuario, administrador")]
        public int GerarNumeroDeclaracaoConcludentesRegular(DeclaracaoConcludentesRegular declaracaoConcludentesRegular)
        {
            int numeroDeclaracaoConcludenteRegularGerado;

            Random random = new Random();

            numeroDeclaracaoConcludenteRegularGerado = random.Next().GetHashCode();
            declaracaoConcludentesRegular.numeroDeclaracaoConcludente = numeroDeclaracaoConcludenteRegularGerado;
            return numeroDeclaracaoConcludenteRegularGerado;
        }

        [Authorize(Roles = "usuario, administrador")]
        public string GerarCodigoDeAutenticacaoConcludentesRegular(DeclaracaoConcludentesRegular declaracaoConcludentesRegular)
        {
            string codigoAutenticacaoConcludentesRegular = GerarNumeroDeclaracaoConcludentesRegular(declaracaoConcludentesRegular).ToString("x");
            declaracaoConcludentesRegular.codigoAutenticacaoConcludente = codigoAutenticacaoConcludentesRegular;
            return codigoAutenticacaoConcludentesRegular;
        }

    }
}
