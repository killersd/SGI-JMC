using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGI_JMC.Models
{
    public class UsuarioModel : IdentityUser
    {
        [Display(Name="Nome completo")]
        [Required(ErrorMessage ="O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public string NomeCompleto { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [StringLength(11,ErrorMessage ="O campo {0} deve ter {1} dígitos.")]
        public string CPF { get; set; }

        [NotMapped]
        public int Idade { get => (int)Math.Floor((DateTime.Now - DataNascimento).TotalDays/365.25); }
    }
}
