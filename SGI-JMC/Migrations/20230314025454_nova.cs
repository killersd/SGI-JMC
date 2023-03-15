using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations
{
    public partial class nova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerieOrigem",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "SerieTurma",
                table: "AlunoAtual");

            migrationBuilder.AddColumn<int>(
                name: "anoLetivo",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "anoSerie",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "codigoAutenticacao",
                table: "AlunoAtual",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "codigoSeed",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataDeEmissao",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "numeroDeclaracao",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "numeroDoNis",
                table: "AlunoAtual",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "turma",
                table: "AlunoAtual",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anoLetivo",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "anoSerie",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "codigoAutenticacao",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "codigoSeed",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "dataDeEmissao",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "numeroDeclaracao",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "numeroDoNis",
                table: "AlunoAtual");

            migrationBuilder.DropColumn(
                name: "turma",
                table: "AlunoAtual");

            migrationBuilder.AddColumn<int>(
                name: "SerieOrigem",
                table: "AlunoAtual",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SerieTurma",
                table: "AlunoAtual",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
