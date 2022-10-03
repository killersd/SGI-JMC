using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class atualizacaoConcludenteRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResultadoFinal",
                table: "DeclaracaoConcludentesRegular",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ResultadoFinal",
                table: "DeclaracaoConcludentesRegular",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
