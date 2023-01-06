using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations.ApplicationDb
{
    public partial class decGen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "DeclaracaoGenerica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorpoDaDeclaracao = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    numeroDeclaracaoGenerica = table.Column<int>(nullable: false),
                    codigoAutenticacaoDeclaracaoGenerica = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracaoGenerica", x => x.Id);
                });
            
        }
    }
}
