using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class DeclaracaoAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

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
        }
    }
}
