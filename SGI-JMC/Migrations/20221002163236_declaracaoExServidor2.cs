using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class declaracaoExServidor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigoAutenticacaoExServidor",
                table: "DeclaracaoExServidor",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "numeroDeclaracaoExServidor",
                table: "DeclaracaoExServidor",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigoAutenticacaoExServidor",
                table: "DeclaracaoExServidor");

            migrationBuilder.DropColumn(
                name: "numeroDeclaracaoExServidor",
                table: "DeclaracaoExServidor");
        }
    }
}
