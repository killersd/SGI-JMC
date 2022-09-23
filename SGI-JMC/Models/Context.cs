using Microsoft.EntityFrameworkCore;
using SGI_JMC.Models;

namespace SGI_JMC.Models
{
    public class Context : DbContext
    {
        public DbSet<aluno> Alunos { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<SGI_JMC.Models.TipoUsuario> TipoUsuario { get; set; }
        public DbSet<SGI_JMC.Models.AcessoTipoUsuario> AcessoTipoUsuario { get; set; }
        public DbSet<SGI_JMC.Models.PerfilUsuario> PerfilUsuario { get; set; }
        public DbSet<SGI_JMC.Models.Declaracao> Declaracao { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoProSic> DeclaracaoProSic { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoTransferenciaRegular> DeclaracaoTransferenciaRegular { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoTransferenciaProSic> DeclaracaoTransferenciaProSic { get; set; }
        public DbSet<SGI_JMC.Models.Advertencia> Advertencia { get; set; }
        public DbSet<SGI_JMC.Models.DeclaracaoSabadoLetivo> DeclaracaoSabadoLetivo { get; set; }
        public DbSet<SGI_JMC.Models.NotificacaoPendenciaDiario> NotificacaoPendenciaDiario { get; set; }
        public DbSet<SGI_JMC.Models.OficioAssumiuFuncao> OficioAssumiuFuncao { get; set; }

    }
}
