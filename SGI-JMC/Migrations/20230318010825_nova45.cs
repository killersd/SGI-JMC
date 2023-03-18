using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class nova45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numeroDoNis",
                table: "AlunoAtual",
                newName: "NumeroDoNis");

            migrationBuilder.AlterColumn<string>(
                name: "turma",
                table: "AlunoMatriculado",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AlterColumn<int>(
                name: "anoSerie",
                table: "AlunoMatriculado",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SerieOrigem",
                table: "AlunoMatriculado",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FaseProSic",
                table: "AlunoMatriculado",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "turma",
                table: "AlunoAtual",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AlterColumn<int>(
                name: "anoSerie",
                table: "AlunoAtual",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FaseProSic",
                table: "AlunoAtual",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SerieOrigem",
                table: "AlunoAtual",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaseProSic",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "SerieOrigem",
                table: "AlunoAtual");

            migrationBuilder.RenameColumn(
                name: "NumeroDoNis",
                table: "AlunoAtual",
                newName: "numeroDoNis");

            migrationBuilder.AlterColumn<string>(
                name: "turma",
                table: "AlunoMatriculado",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "anoSerie",
                table: "AlunoMatriculado",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SerieOrigem",
                table: "AlunoMatriculado",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FaseProSic",
                table: "AlunoMatriculado",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "turma",
                table: "AlunoAtual",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "anoSerie",
                table: "AlunoAtual",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
