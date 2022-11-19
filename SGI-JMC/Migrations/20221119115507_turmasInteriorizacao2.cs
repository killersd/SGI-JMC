using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class turmasInteriorizacao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CidadeEntrada",
                table: "DeclaracaoDistanciaInteriorizacao",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstadoResidencia",
                table: "DeclaracaoDistanciaInteriorizacao",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CidadeEntrada",
                table: "DeclaracaoDistanciaInteriorizacao");

            migrationBuilder.DropColumn(
                name: "EstadoResidencia",
                table: "DeclaracaoDistanciaInteriorizacao");
        }
    }
}
