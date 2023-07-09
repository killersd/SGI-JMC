using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class changeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TempoDeContrato",
                table: "Contratos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TempoDeContrato",
                table: "Contratos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
