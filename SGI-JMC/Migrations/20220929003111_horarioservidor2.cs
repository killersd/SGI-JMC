using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class horarioservidor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "domingo",
                table: "HorarioServidor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "quarta",
                table: "HorarioServidor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "quinta",
                table: "HorarioServidor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sabado",
                table: "HorarioServidor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "segunda",
                table: "HorarioServidor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sexta",
                table: "HorarioServidor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "terca",
                table: "HorarioServidor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "domingo",
                table: "HorarioServidor");

            migrationBuilder.DropColumn(
                name: "quarta",
                table: "HorarioServidor");

            migrationBuilder.DropColumn(
                name: "quinta",
                table: "HorarioServidor");

            migrationBuilder.DropColumn(
                name: "sabado",
                table: "HorarioServidor");

            migrationBuilder.DropColumn(
                name: "segunda",
                table: "HorarioServidor");

            migrationBuilder.DropColumn(
                name: "sexta",
                table: "HorarioServidor");

            migrationBuilder.DropColumn(
                name: "terca",
                table: "HorarioServidor");
        }
    }
}
