using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class decTransf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeclaracaoTransferenciaRegular");
        }
    }
}
