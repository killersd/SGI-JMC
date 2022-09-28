using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class horarioTarde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tq01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tq02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tq03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tq04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tq05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tqu01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tqu02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tqu03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tqu04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tqu05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ts01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ts02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ts03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ts04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ts05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tse01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tse02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tse03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tse04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tse05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tt01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tt02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tt03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tt04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tt05",
                table: "HorarioProfessor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tq01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tq02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tq03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tq04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tq05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tqu01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tqu02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tqu03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tqu04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tqu05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "ts01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "ts02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "ts03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "ts04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "ts05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tse01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tse02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tse03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tse04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tse05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tt01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tt02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tt03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tt04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "tt05",
                table: "HorarioProfessor");
        }
    }
}
