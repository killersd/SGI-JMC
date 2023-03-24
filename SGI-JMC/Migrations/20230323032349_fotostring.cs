using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class fotostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlFoto",
                table: "AlunoAtual",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlFoto",
                table: "AlunoAtual");
        }
    }
}
