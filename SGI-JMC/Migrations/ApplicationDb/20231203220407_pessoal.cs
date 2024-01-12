using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI_JMC.Migrations.ApplicationDb
{
    public partial class pessoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
               

            migrationBuilder.CreateTable(
                name: "AlunoAtual",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Pai = table.Column<string>(nullable: true),
                    Mae = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: true),
                    numeroDeclaracao = table.Column<int>(nullable: false),
                    codigoAutenticacao = table.Column<string>(nullable: true),
                    codigoSeed = table.Column<string>(nullable: false),
                    dataDeEmissao = table.Column<DateTime>(nullable: false),
                    anoLetivo = table.Column<int>(nullable: false),
                    anoSerie = table.Column<int>(nullable: true),
                    turma = table.Column<string>(nullable: true),
                    FaseProSic = table.Column<int>(nullable: true),
                    SerieOrigem = table.Column<int>(nullable: true),
                    NumeroDoNis = table.Column<string>(nullable: true),
                    CorrecaoDeFluxo = table.Column<string>(nullable: false),
                    Transferido = table.Column<bool>(nullable: false),
                    UrlFoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoAtual", x => x.Id);
                });          
            

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertencia");

            migrationBuilder.DropTable(
                name: "AlunoAtual");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Declaracao");

            migrationBuilder.DropTable(
                name: "DeclaracaoConcludentesProSic");

            migrationBuilder.DropTable(
                name: "DeclaracaoConcludentesRegular");

            migrationBuilder.DropTable(
                name: "DeclaracaoExServidor");

            migrationBuilder.DropTable(
                name: "DeclaracaoProSic");

            migrationBuilder.DropTable(
                name: "DeclaracaoSabadoLetivo");

            migrationBuilder.DropTable(
                name: "DeclaracaoServidor");

            migrationBuilder.DropTable(
                name: "DeclaracaoTransferenciaProSic");

            migrationBuilder.DropTable(
                name: "DeclaracaoTransferenciaProSicAnoFinalizado");

            migrationBuilder.DropTable(
                name: "DeclaracaoTransferenciaRegular");

            migrationBuilder.DropTable(
                name: "DeclaracaoTransferenciaRegularAnoFinalizado");

            migrationBuilder.DropTable(
                name: "HorarioApoioEscolar2");

            migrationBuilder.DropTable(
                name: "HorarioProfessor");

            migrationBuilder.DropTable(
                name: "HorarioServidor");

            migrationBuilder.DropTable(
                name: "NotificacaoPendenciaDiario");

            migrationBuilder.DropTable(
                name: "OficioAssumiuFuncao");

            migrationBuilder.DropTable(
                name: "OficioAssumiuFuncaoApoioEscolar2");

            migrationBuilder.DropTable(
                name: "OficioAssumiuFuncaoServidor");

            migrationBuilder.DropTable(
                name: "OficioGeral");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
