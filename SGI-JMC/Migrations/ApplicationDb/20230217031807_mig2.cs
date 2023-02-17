using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations.ApplicationDb
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FonteRecursos",
                table: "OficioAssumiuFuncao");

            migrationBuilder.DropColumn(
                name: "teste",
                table: "OficioAssumiuFuncao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FonteRecursos",
                table: "OficioAssumiuFuncao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "teste",
                table: "OficioAssumiuFuncao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
