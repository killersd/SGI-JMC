using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI_JMC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SGI_JMC.Models.TipoUsuario> TipoUsuario { get; set; }
        public DbSet<SGI_JMC.Models.AcessoTipoUsuario> AcessoTipoUsuario { get; set; }
        public DbSet<SGI_JMC.Models.PerfilUsuario> PerfilUsuario { get; set; }

        public DbSet<IdentityUser> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Seed();
        }

    }
}
