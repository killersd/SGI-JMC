using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaseProSic",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "SerieDeOrigem",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "anoSerie",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "turma",
                table: "AlunoAtual");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaseProSic",
                table: "AlunoAtual",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SerieDeOrigem",
                table: "AlunoAtual",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "anoSerie",
                table: "AlunoAtual",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "turma",
                table: "AlunoAtual",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }
    }
}
