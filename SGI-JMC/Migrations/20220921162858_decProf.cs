using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class decProf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    dataSabado = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoSabadoLetivo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeclaracaoSabadoLetivo");
        }
    }
}
