using Microsoft.AspNetCore.Identity;
using System;

namespace SGI_JMC.Models
{
    public class Inicializador
    {
        private static void InicializarPerfis(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("administrador").Result)
            {
                var perfil = new IdentityRole();
                perfil.Name = "administrador";
                roleManager.CreateAsync(perfil).Wait();
            }
            if (!roleManager.RoleExistsAsync("usuario").Result)
            {
                var perfil = new IdentityRole();
                perfil.Name = "usuario";
                roleManager.CreateAsync(perfil).Wait();
            }
        }

        private static void InicializarUsuarios(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin@email.com").Result == null)
            {
                var usuario = new UsuarioModel();
                usuario.UserName = "admin@email.com";
                usuario.Email = "admin@email.com";
                usuario.NomeCompleto = "Administrador do Sistema";
                usuario.DataNascimento = new DateTime(1980, 1, 1);
                usuario.PhoneNumber = "99999999999";
                usuario.CPF = "00000000000";
                var resultado = userManager.CreateAsync(usuario, "123Aa@").Result;
                if (resultado.Succeeded)
                {
                    userManager.AddToRoleAsync(usuario, "administrador").Wait();
                }
            }
        }

        public static void InicializarIdentity(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            InicializarPerfis(roleManager);
            InicializarUsuarios(userManager);
        }
    }
}
