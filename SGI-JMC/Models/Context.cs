using Microsoft.EntityFrameworkCore;
using SGI_JMC.Models;

namespace SGI_JMC.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Comunicado> Comunicado { get; set; }
        public DbSet<aluno> Alunos { get; set; }
        public DbSet<Declaracao> Declaracao { get; set; }
        public DbSet<DeclaracaoProSic> DeclaracaoProSic { get; set; }
        public DbSet<DeclaracaoTransferenciaRegular> DeclaracaoTransferenciaRegular { get; set; }
        public DbSet<DeclaracaoTransferenciaProSic> DeclaracaoTransferenciaProSic { get; set; }
        public DbSet<Advertencia> Advertencia { get; set; }
        public DbSet<DeclaracaoSabadoLetivo> DeclaracaoSabadoLetivo { get; set; }
        public DbSet<NotificacaoPendenciaDiario> NotificacaoPendenciaDiario { get; set; }
        public DbSet<OficioAssumiuFuncao> OficioAssumiuFuncao { get; set; }
        public DbSet<HorarioProfessor> HorarioProfessor { get; set; }
        public DbSet<HorarioServidor> HorarioServidor { get; set; }
        public DbSet<OficioAssumiuFuncaoServidor> OficioAssumiuFuncaoServidor { get; set; }
        public DbSet<OficioGeral> OficioGeral { get; set; }
        public DbSet<DeclaracaoExServidor> DeclaracaoExServidor { get; set; }
        public DbSet<DeclaracaoServidor> DeclaracaoServidor { get; set; }
        public DbSet<DeclaracaoConcludentesRegular> DeclaracaoConcludentesRegular { get; set; }
        public DbSet<DeclaracaoConcludentesProSic> DeclaracaoConcludentesProSic { get; set; }
        public DbSet<OficioAssumiuFuncaoApoioEscolar2> OficioAssumiuFuncaoApoioEscolar2 { get; set; }
        public DbSet<HorarioApoioEscolar2> HorarioApoioEscolar2 { get; set; }
        public DbSet<AlunoSemTransferencia> AlunoSemTransferencia { get; set; }
        public DbSet<DeclaracaoExAluno> DeclaracaoExAluno { get; set; }
        public DbSet<DeclaracaoDistanciaInteriorizacao> DeclaracaoDistanciaInteriorizacao { get; set; }
        public DbSet<DeclaracaoResidencia> DeclaracaoResidencia { get; set; }
        public DbSet<DeclaracaoGenerica> DeclaracaoGenerica { get; set; }
        public DbSet<DeclaracaoTransferenciaRegularAnoFinalizado> DeclaracaoTransferenciaRegularAnoFinalizado { get; set; }
        public DbSet<DeclaracaoTransferenciaProSicAnoFinalizado> DeclaracaoTransferenciaProSicAnoFinalizado { get; set; }
        public DbSet<AlunoAtual> AlunoAtual { get; set; }
        public DbSet<AlunoMatriculado> AlunoMatriculado { get; set; }

        public DbSet<Contratos> Contratos { get; set; }


    }
}
