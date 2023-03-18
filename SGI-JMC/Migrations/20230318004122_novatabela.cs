using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class novatabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlunoMatriculado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Pai = table.Column<string>(nullable: true),
                    Mae = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: true),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    anoSerie = table.Column<int>(nullable: true),
                    turma = table.Column<string>(nullable: true),
                    FaseProSic = table.Column<int>(nullable: true),
                    SerieOrigem = table.Column<int>(nullable: true),
                    NumeroDoNis = table.Column<string>(nullable: true),
                    CorrecaoDeFluxo = table.Column<string>(nullable: false),
                    Transferido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoMatriculado", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoMatriculado");
        }
    }
}
