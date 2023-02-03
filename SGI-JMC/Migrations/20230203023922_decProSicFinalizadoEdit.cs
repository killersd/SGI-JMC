using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class decProSicFinalizadoEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "turmaOrigem",
                table: "DeclaracaoTransferenciaProSicAnoFinalizado",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "turmaOrigem",
                table: "DeclaracaoTransferenciaProSicAnoFinalizado",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
