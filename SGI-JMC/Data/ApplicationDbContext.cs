using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace SGI_JMC.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<IdentityUser> Usuario { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
