using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations.ApplicationDb
{
    public partial class mudancaDeTipo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "turma",
                table: "Advertencia",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AlterColumn<string>(
                name: "anoSerie",
                table: "Advertencia",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
