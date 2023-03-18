using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaseProSic",
                table: "AlunoAtual",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SerieDeOrigem",
                table: "AlunoAtual",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "anoSerie",
                table: "AlunoAtual",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "turma",
                table: "AlunoAtual",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
