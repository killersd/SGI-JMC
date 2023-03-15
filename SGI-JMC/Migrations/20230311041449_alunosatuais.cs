using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class alunosatuais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {          

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
                    SerieTurma = table.Column<string>(nullable: false),
                    SerieOrigem = table.Column<int>(nullable: false)
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

            migrationBuilder.AlterColumn<string>(
                name: "turma",
                table: "Advertencia",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "anoSerie",
                table: "Advertencia",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
