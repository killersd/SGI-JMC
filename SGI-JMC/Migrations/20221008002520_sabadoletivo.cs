using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class sabadoletivo : Migration
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
                    dataSabado = table.Column<DateTime>(nullable: false),
                    CargoServidor = table.Column<string>(nullable: false),
                    TurnoDeTrabalho = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoSabadoLetivo", x => x.Id);
                });

        }
    }
}
