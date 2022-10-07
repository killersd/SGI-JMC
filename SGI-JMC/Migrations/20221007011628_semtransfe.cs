using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class semtransfe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {




            migrationBuilder.CreateTable(
                name: "AlunoSemTransferencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NomePai = table.Column<string>(nullable: true),
                    NomeMae = table.Column<string>(nullable: false),
                    DataNasciimento = table.Column<DateTime>(nullable: false),
                    EscolaAnterior = table.Column<string>(nullable: false),
                    TemPendencia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoSemTransferencia", x => x.Id);
                });


        }
    }
}
