using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class variaveisparahorario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "q01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "q02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "q03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "q04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "q05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qu01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qu02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qu03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qu04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qu05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "s01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "s02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "s03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "s04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "s05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "se01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "se02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "se03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "se04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "se05",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "t01",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "t02",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "t03",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "t04",
                table: "HorarioProfessor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "t05",
                table: "HorarioProfessor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "q01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "q02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "q03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "q04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "q05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "qu01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "qu02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "qu03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "qu04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "qu05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "s01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "s02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "s03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "s04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "s05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "se01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "se02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "se03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "se04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "se05",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "t01",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "t02",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "t03",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "t04",
                table: "HorarioProfessor");

            migrationBuilder.DropColumn(
                name: "t05",
                table: "HorarioProfessor");
        }
    }
}
