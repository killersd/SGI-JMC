using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations.ApplicationDb
{
    public partial class nova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sab01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sab02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sab03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sab04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sab05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tsab01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tsab02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tsab03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tsab04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tsab05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlunoAtual",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Pai = table.Column<string>(nullable: true),
                    Mae = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    anoSerie = table.Column<int>(nullable: false),
                    turma = table.Column<string>(nullable: false),
                    numeroDoNis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoAtual", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "sab01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "sab02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "sab03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "sab04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "sab05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tsab01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tsab02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tsab03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tsab04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tsab05",
                table: "HorarioProfessor");
        }
    }
}
