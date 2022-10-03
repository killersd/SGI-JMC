using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class concProSic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeclaracaoConcludentesProSic");
        }
    }
}
