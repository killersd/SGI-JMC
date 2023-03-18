using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaseProSic",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "SerieDeOrigem",
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
        }
    }
}
