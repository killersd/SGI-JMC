using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class decResDis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "DeclaracaoDistanciaInteriorizacao",
                 columns: table => new
                 {
                     Id = table.Column<int>(nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     Nome = table.Column<string>(nullable: false),
                     CPF = table.Column<string>(nullable: false),
                     RG = table.Column<string>(nullable: false),
                     OrgaoExpedidor = table.Column<string>(nullable: false),
                     InicioExercicio = table.Column<DateTime>(nullable: false),
                     CargaHorariaSemanal = table.Column<string>(nullable: false),
                     Distancia = table.Column<string>(nullable: false),
                     Origem = table.Column<string>(nullable: false),
                     Destino = table.Column<string>(nullable: false),
                     DataDeEmissao = table.Column<DateTime>(nullable: false),
                     NumeroDeclaracaoDistanciaInteriorizacao = table.Column<int>(nullable: false),
                     CodigoAutenticacaoDeclaracaoInteriorizacao = table.Column<string>(nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_DeclaracaoDistanciaInteriorizacao", x => x.Id);
                 });

            migrationBuilder.CreateTable(
                name: "DeclaracaoResidencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    DataDeEmissao = table.Column<DateTime>(nullable: false),
                    NumeroDeclaracaoResidencia = table.Column<int>(nullable: false),
                    CodigoAutenticacaoDeclaracaoResidencia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoResidencia", x => x.Id);
                });
        }
    }
}
