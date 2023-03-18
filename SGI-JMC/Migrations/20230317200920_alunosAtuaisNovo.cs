using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class alunosAtuaisNovo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "OficioAssumiuFuncaoServidor",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "OficioAssumiuFuncaoApoioEscolar2",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "OficioAssumiuFuncao",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "HorarioProfessor",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "HorarioApoioEscolar2",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "DeclaracaoServidor",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "DeclaracaoSabadoLetivo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "DeclaracaoExServidor",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "AlunoAtual",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FaseProSic",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SerieDeOrigem",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Transferido",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaseProSic",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "SerieDeOrigem",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "Transferido",
                table: "AlunoAtual");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "OficioAssumiuFuncaoServidor",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "OficioAssumiuFuncaoApoioEscolar2",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "OficioAssumiuFuncao",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "HorarioProfessor",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "HorarioApoioEscolar2",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "DeclaracaoServidor",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "DeclaracaoSabadoLetivo",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "DeclaracaoExServidor",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "AlunoAtual",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
