using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class oficiogeral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OficioGeral",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOficio = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: false),
                    destinatario = table.Column<string>(nullable: false),
                    CargoDoDestinatario = table.Column<string>(nullable: false),
                    CorpoDoOficio = table.Column<string>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficioGeral", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OficioGeral");
        }
    }
}
