using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class assumiufuncao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OficioAssumiuFuncao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    NumeroOficio = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: false),
                    destinatario = table.Column<string>(nullable: false),
                    DataAssumiuFuncao = table.Column<DateTime>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    vinculo = table.Column<string>(nullable: false),
                    CargaHoraria = table.Column<string>(nullable: false),
                    disciplina = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficioAssumiuFuncao", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OficioAssumiuFuncao");
        }
    }
}
