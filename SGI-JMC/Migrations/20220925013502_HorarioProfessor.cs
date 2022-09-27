using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class HorarioProfessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    Disciplina = table.Column<string>(nullable: false),
                    Cargo = table.Column<string>(nullable: false),
                    CargaHorariaSemanal = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioProfessor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioProfessor");
        }
    }
}
