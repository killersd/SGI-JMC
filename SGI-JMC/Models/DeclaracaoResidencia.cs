using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class DeclaracaoResidencia
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do servidor")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "CPF do servidor")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CPF { get; set; }     

        [Display(Name = "Data de emissão")]
        [DataType(DataType.Date)]
        public DateTime DataDeEmissao { get; set; }

        public int NumeroDeclaracaoResidencia { get; set; }
        public string CodigoAutenticacaoDeclaracaoResidencia { get; set; }
    }
}
