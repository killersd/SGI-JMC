using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class declaracaoconcludenteregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ResultadoFinal = table.Column<int>(nullable: false),
                    numeroDeclaracaoConcludente = table.Column<int>(nullable: false),
                    codigoAutenticacaoConcludente = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoConcludentesRegular", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeclaracaoConcludentesRegular");
        }
    }
}
