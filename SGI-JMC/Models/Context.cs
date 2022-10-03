using Microsoft.EntityFrameworkCore;
using SGI_JMC.Models;

namespace SGI_JMC.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<aluno> Alunos { get; set; }
        public DbSet<SGI_JMC.Models.Declaracao> Declaracao { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoProSic> DeclaracaoProSic { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoTransferenciaRegular> DeclaracaoTransferenciaRegular { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoTransferenciaProSic> DeclaracaoTransferenciaProSic { get; set; }
        public DbSet<SGI_JMC.Models.Advertencia> Advertencia { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoSabadoLetivo> DeclaracaoSabadoLetivo { get; set; }
        public DbSet<SGI_JMC.Models.NotificacaoPendenciaDiario> NotificacaoPendenciaDiario { get; set; }
        public DbSet<SGI_JMC.Models.OficioAssumiuFuncao> OficioAssumiuFuncao { get; set; }
        public DbSet<SGI_JMC.Models.HorarioProfessor> HorarioProfessor { get; set; }
        public DbSet<SGI_JMC.Models.HorarioServidor> HorarioServidor { get; set; }
        public DbSet<SGI_JMC.Models.OficioAssumiuFuncaoServidor> OficioAssumiuFuncaoServidor { get; set; }
        public DbSet<SGI_JMC.Models.OficioGeral> OficioGeral { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoExServidor> DeclaracaoExServidor { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoServidor> DeclaracaoServidor { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoConcludentesRegular> DeclaracaoConcludentesRegular { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoConcludentesProSic> DeclaracaoConcludentesProSic { get; set; }

    }
}
