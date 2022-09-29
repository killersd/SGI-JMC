using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class horarioservidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioServidor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Unidade = table.Column<string>(nullable: false),
                    Cargo = table.Column<string>(nullable: false),
                    CargaHorariaSemanal = table.Column<int>(nullable: false),
                    Vinculo = table.Column<string>(nullable: false),
                    Turno = table.Column<string>(nullable: false),
                    TurnoHorario = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioServidor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioServidor");
        }
    }
}
