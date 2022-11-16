using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Father_name = table.Column<string>(nullable: true),
                    Mother_name = table.Column<string>(nullable: false),
                    Birth_date = table.Column<DateTime>(nullable: false),
                    anoSerie = table.Column<int>(nullable: false),
                    turma = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    descricaoDoFato = table.Column<string>(nullable: false),
                    turno = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Father_name = table.Column<string>(nullable: true),
                    Mother_name = table.Column<string>(nullable: false),
                    Birth_date = table.Column<DateTime>(nullable: false),
                    Year_folder = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunoSemTransferencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NomePai = table.Column<string>(nullable: true),
                    NomeMae = table.Column<string>(nullable: false),
                    DataNasciimento = table.Column<DateTime>(nullable: false),
                    EscolaAnterior = table.Column<string>(nullable: false),
                    TemPendencia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoSemTransferencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comunicado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAluno = table.Column<string>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    Turno = table.Column<string>(nullable: false),
                    Turma = table.Column<string>(nullable: false),
                    MembroEquipeDiretiva = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    DataComparecimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Declaracao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Father_name = table.Column<string>(nullable: true),
                    Mother_name = table.Column<string>(nullable: false),
                    Birth_date = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    anoSerie = table.Column<int>(nullable: false),
                    turma = table.Column<string>(nullable: false),
                    numeroDoNis = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    qtdFaltas = table.Column<int>(nullable: false),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declaracao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoConcludentesProSic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NomeDoPai = table.Column<string>(nullable: true),
                    NomeDaMae = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    AnoLetivo = table.Column<int>(nullable: false),
                    FaseProSic = table.Column<string>(nullable: false),
                    SerieDeOrigem = table.Column<string>(nullable: false),
                    NumeroDoNis = table.Column<string>(nullable: true),
                    CodigoSeed = table.Column<string>(nullable: false),
                    DataDeEmissao = table.Column<DateTime>(nullable: false),
                    ResultadoFinal = table.Column<string>(nullable: false),
                    numeroDeclaracaoConcludenteProSic = table.Column<int>(nullable: false),
                    codigoAutenticacaoConcludenteProSic = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoConcludentesProSic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoConcludentesRegular",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NomeDoPai = table.Column<string>(nullable: true),
                    NomeDaMae = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    AnoLetivo = table.Column<int>(nullable: false),
                    AnoSerie = table.Column<int>(nullable: false),
                    Turma = table.Column<string>(nullable: false),
                    NumeroDoNis = table.Column<string>(nullable: true),
                    CodigoSeed = table.Column<string>(nullable: false),
                    DataDeEmissao = table.Column<DateTime>(nullable: false),
                    ResultadoFinal = table.Column<string>(nullable: false),
                    numeroDeclaracaoConcludente = table.Column<int>(nullable: false),
                    codigoAutenticacaoConcludente = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoConcludentesRegular", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoExAluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NomePai = table.Column<string>(nullable: true),
                    NomeMae = table.Column<string>(nullable: false),
                    DataNasimento = table.Column<DateTime>(nullable: false),
                    AnoLetivo = table.Column<int>(nullable: false),
                    Serie = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    DataDeEmissao = table.Column<DateTime>(nullable: false),
                    ResultadoFinal = table.Column<string>(nullable: false),
                    NumeroDeclaracao = table.Column<int>(nullable: false),
                    CodigoAutenticacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoExAluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoExServidor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    vinculo = table.Column<string>(nullable: false),
                    CargaHoraria = table.Column<string>(nullable: false),
                    cargo = table.Column<string>(nullable: false),
                    DataInicioExercicio = table.Column<DateTime>(nullable: false),
                    DataFimExercicio = table.Column<DateTime>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    numeroDeclaracaoExServidor = table.Column<int>(nullable: false),
                    codigoAutenticacaoExServidor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoExServidor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoProSic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Father_name = table.Column<string>(nullable: true),
                    Mother_name = table.Column<string>(nullable: false),
                    Birth_date = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    faseProSic = table.Column<int>(nullable: false),
                    serieOrigem = table.Column<int>(nullable: false),
                    numeroDoNis = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    qtdFaltas = table.Column<int>(nullable: false),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoProSic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoSabadoLetivo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    numeroDeclaracaoSabado = table.Column<int>(nullable: false),
                    codigoAutenticacaoSabado = table.Column<string>(nullable: true),
                    dataSabado = table.Column<DateTime>(nullable: false),
                    CargoServidor = table.Column<string>(nullable: false),
                    TurnoDeTrabalho = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoSabadoLetivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoServidor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    vinculo = table.Column<string>(nullable: false),
                    CargaHoraria = table.Column<string>(nullable: false),
                    cargo = table.Column<string>(nullable: false),
                    DataInicioExercicio = table.Column<DateTime>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    numeroDeclaracaoServidor = table.Column<int>(nullable: false),
                    codigoAutenticacaoServidor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoServidor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoTransferenciaProSic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Father_name = table.Column<string>(nullable: true),
                    Mother_name = table.Column<string>(nullable: false),
                    Birth_date = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    faseProSic = table.Column<int>(nullable: false),
                    serieOrigem = table.Column<int>(nullable: false),
                    numeroDoNis = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    qtdFaltas = table.Column<int>(nullable: false),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoTransferenciaProSic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracaoTransferenciaRegular",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Father_name = table.Column<string>(nullable: true),
                    Mother_name = table.Column<string>(nullable: false),
                    Birth_date = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    anoSerie = table.Column<int>(nullable: false),
                    turma = table.Column<string>(nullable: false),
                    numeroDoNis = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    DataSolicitacao = table.Column<DateTime>(nullable: false),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoTransferenciaRegular", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HorarioApoioEscolar2",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    Cargo = table.Column<string>(nullable: false),
                    CargaHorariaMensal = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    s01 = table.Column<string>(nullable: true),
                    s02 = table.Column<string>(nullable: true),
                    s03 = table.Column<string>(nullable: true),
                    s04 = table.Column<string>(nullable: true),
                    s05 = table.Column<string>(nullable: true),
                    t01 = table.Column<string>(nullable: true),
                    t02 = table.Column<string>(nullable: true),
                    t03 = table.Column<string>(nullable: true),
                    t04 = table.Column<string>(nullable: true),
                    t05 = table.Column<string>(nullable: true),
                    q01 = table.Column<string>(nullable: true),
                    q02 = table.Column<string>(nullable: true),
                    q03 = table.Column<string>(nullable: true),
                    q04 = table.Column<string>(nullable: true),
                    q05 = table.Column<string>(nullable: true),
                    qu01 = table.Column<string>(nullable: true),
                    qu02 = table.Column<string>(nullable: true),
                    qu03 = table.Column<string>(nullable: true),
                    qu04 = table.Column<string>(nullable: true),
                    qu05 = table.Column<string>(nullable: true),
                    se01 = table.Column<string>(nullable: true),
                    se02 = table.Column<string>(nullable: true),
                    se03 = table.Column<string>(nullable: true),
                    se04 = table.Column<string>(nullable: true),
                    se05 = table.Column<string>(nullable: true),
                    ts01 = table.Column<string>(nullable: true),
                    ts02 = table.Column<string>(nullable: true),
                    ts03 = table.Column<string>(nullable: true),
                    ts04 = table.Column<string>(nullable: true),
                    ts05 = table.Column<string>(nullable: true),
                    tt01 = table.Column<string>(nullable: true),
                    tt02 = table.Column<string>(nullable: true),
                    tt03 = table.Column<string>(nullable: true),
                    tt04 = table.Column<string>(nullable: true),
                    tt05 = table.Column<string>(nullable: true),
                    tq01 = table.Column<string>(nullable: true),
                    tq02 = table.Column<string>(nullable: true),
                    tq03 = table.Column<string>(nullable: true),
                    tq04 = table.Column<string>(nullable: true),
                    tq05 = table.Column<string>(nullable: true),
                    tqu01 = table.Column<string>(nullable: true),
                    tqu02 = table.Column<string>(nullable: true),
                    tqu03 = table.Column<string>(nullable: true),
                    tqu04 = table.Column<string>(nullable: true),
                    tqu05 = table.Column<string>(nullable: true),
                    tse01 = table.Column<string>(nullable: true),
                    tse02 = table.Column<string>(nullable: true),
                    tse03 = table.Column<string>(nullable: true),
                    tse04 = table.Column<string>(nullable: true),
                    tse05 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioApoioEscolar2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HorarioProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    Disciplina = table.Column<string>(nullable: false),
                    Cargo = table.Column<string>(nullable: false),
                    CargaHorariaSemanal = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    s01 = table.Column<string>(nullable: true),
                    s02 = table.Column<string>(nullable: true),
                    s03 = table.Column<string>(nullable: true),
                    s04 = table.Column<string>(nullable: true),
                    s05 = table.Column<string>(nullable: true),
                    t01 = table.Column<string>(nullable: true),
                    t02 = table.Column<string>(nullable: true),
                    t03 = table.Column<string>(nullable: true),
                    t04 = table.Column<string>(nullable: true),
                    t05 = table.Column<string>(nullable: true),
                    q01 = table.Column<string>(nullable: true),
                    q02 = table.Column<string>(nullable: true),
                    q03 = table.Column<string>(nullable: true),
                    q04 = table.Column<string>(nullable: true),
                    q05 = table.Column<string>(nullable: true),
                    qu01 = table.Column<string>(nullable: true),
                    qu02 = table.Column<string>(nullable: true),
                    qu03 = table.Column<string>(nullable: true),
                    qu04 = table.Column<string>(nullable: true),
                    qu05 = table.Column<string>(nullable: true),
                    se01 = table.Column<string>(nullable: true),
                    se02 = table.Column<string>(nullable: true),
                    se03 = table.Column<string>(nullable: true),
                    se04 = table.Column<string>(nullable: true),
                    se05 = table.Column<string>(nullable: true),
                    ts01 = table.Column<string>(nullable: true),
                    ts02 = table.Column<string>(nullable: true),
                    ts03 = table.Column<string>(nullable: true),
                    ts04 = table.Column<string>(nullable: true),
                    ts05 = table.Column<string>(nullable: true),
                    tt01 = table.Column<string>(nullable: true),
                    tt02 = table.Column<string>(nullable: true),
                    tt03 = table.Column<string>(nullable: true),
                    tt04 = table.Column<string>(nullable: true),
                    tt05 = table.Column<string>(nullable: true),
                    tq01 = table.Column<string>(nullable: true),
                    tq02 = table.Column<string>(nullable: true),
                    tq03 = table.Column<string>(nullable: true),
                    tq04 = table.Column<string>(nullable: true),
                    tq05 = table.Column<string>(nullable: true),
                    tqu01 = table.Column<string>(nullable: true),
                    tqu02 = table.Column<string>(nullable: true),
                    tqu03 = table.Column<string>(nullable: true),
                    tqu04 = table.Column<string>(nullable: true),
                    tqu05 = table.Column<string>(nullable: true),
                    tse01 = table.Column<string>(nullable: true),
                    tse02 = table.Column<string>(nullable: true),
                    tse03 = table.Column<string>(nullable: true),
                    tse04 = table.Column<string>(nullable: true),
                    tse05 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioProfessor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HorarioServidor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Unidade = table.Column<string>(nullable: false),
                    Cargo = table.Column<string>(nullable: false),
                    CargaHorariaSemanal = table.Column<int>(nullable: false),
                    Vinculo = table.Column<string>(nullable: false),
                    Turno = table.Column<string>(nullable: false),
                    TurnoHorario = table.Column<string>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    segunda = table.Column<string>(nullable: true),
                    terca = table.Column<string>(nullable: true),
                    quarta = table.Column<string>(nullable: true),
                    quinta = table.Column<string>(nullable: true),
                    sexta = table.Column<string>(nullable: true),
                    sabado = table.Column<string>(nullable: true),
                    domingo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioServidor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificacaoPendenciaDiario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    dataLimite = table.Column<DateTime>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    qtdAulas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacaoPendenciaDiario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OficioAssumiuFuncao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    NumeroOficio = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: false),
                    destinatario = table.Column<string>(nullable: false),
                    DataAssumiuFuncao = table.Column<DateTime>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    vinculo = table.Column<string>(nullable: false),
                    CargaHoraria = table.Column<string>(nullable: false),
                    disciplina = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    CidadeDestinatario = table.Column<string>(nullable: false),
                    CargoDestinatario = table.Column<string>(nullable: false),
                    FonteRecursos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficioAssumiuFuncao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OficioAssumiuFuncaoApoioEscolar2",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    NumeroOficio = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: false),
                    destinatario = table.Column<string>(nullable: false),
                    DataAssumiuFuncao = table.Column<DateTime>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    vinculo = table.Column<string>(nullable: false),
                    CargaHorariaMensal = table.Column<string>(nullable: false),
                    disciplina = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    CidadeDestinatario = table.Column<string>(nullable: false),
                    CargoDestinatario = table.Column<string>(nullable: false),
                    FRC = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficioAssumiuFuncaoApoioEscolar2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OficioAssumiuFuncaoServidor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    NumeroOficio = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: false),
                    destinatario = table.Column<string>(nullable: false),
                    DataAssumiuFuncao = table.Column<DateTime>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    vinculo = table.Column<string>(nullable: false),
                    CargaHoraria = table.Column<string>(nullable: false),
                    cargo = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    CidadeDestinatario = table.Column<string>(nullable: false),
                    CargoDestinatario = table.Column<string>(nullable: false),
                    FRC = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficioAssumiuFuncaoServidor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OficioGeral",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOficio = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: false),
                    destinatario = table.Column<string>(nullable: false),
                    CargoDoDestinatario = table.Column<string>(nullable: false),
                    CorpoDoOficio = table.Column<string>(nullable: false),
                    Remetente = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    Cidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficioGeral", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertencia");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "AlunoSemTransferencia");

            migrationBuilder.DropTable(
                name: "Comunicado");

            migrationBuilder.DropTable(
                name: "Declaracao");

            migrationBuilder.DropTable(
                name: "DeclaracaoConcludentesProSic");

            migrationBuilder.DropTable(
                name: "DeclaracaoConcludentesRegular");

            migrationBuilder.DropTable(
                name: "DeclaracaoExAluno");

            migrationBuilder.DropTable(
                name: "DeclaracaoExServidor");

            migrationBuilder.DropTable(
                name: "DeclaracaoProSic");

            migrationBuilder.DropTable(
                name: "DeclaracaoSabadoLetivo");

            migrationBuilder.DropTable(
                name: "DeclaracaoServidor");

            migrationBuilder.DropTable(
                name: "DeclaracaoTransferenciaProSic");

            migrationBuilder.DropTable(
                name: "DeclaracaoTransferenciaRegular");

            migrationBuilder.DropTable(
                name: "HorarioApoioEscolar2");

            migrationBuilder.DropTable(
                name: "HorarioProfessor");

            migrationBuilder.DropTable(
                name: "HorarioServidor");

            migrationBuilder.DropTable(
                name: "NotificacaoPendenciaDiario");

            migrationBuilder.DropTable(
                name: "OficioAssumiuFuncao");

            migrationBuilder.DropTable(
                name: "OficioAssumiuFuncaoApoioEscolar2");

            migrationBuilder.DropTable(
                name: "OficioAssumiuFuncaoServidor");

            migrationBuilder.DropTable(
                name: "OficioGeral");
        }
    }
}
