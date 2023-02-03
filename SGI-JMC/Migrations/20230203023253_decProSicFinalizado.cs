using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class decProSicFinalizado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeclaracaoTransferenciaProSicAnoFinalizado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Father_name = table.Column<string>(nullable: true),
                    Mother_name = table.Column<string>(nullable: false),
                    Birth_date = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    faseProsic = table.Column<int>(nullable: false),
                    turmaOrigem = table.Column<string>(nullable: false),
                    numeroDoNis = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    ResultadoFinal = table.Column<string>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    SerieSeguinte = table.Column<int>(nullable: false),
                    DataSolicitacao = table.Column<DateTime>(nullable: false),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoTransferenciaProSicAnoFinalizado", x => x.Id);
                });
            

        }

    }
}
