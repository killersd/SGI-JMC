using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class DeclaracaoExAluno
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Nome do pai")]
        public string NomePai { get; set; }

        [Display(Name = "Nome da mãe")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string NomeMae { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataNasimento { get; set; }

        [Display(Name = "Ano letivo")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int AnoLetivo { get; set; }

        [Display(Name = "Série")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Serie { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Ano { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime DataDeEmissao { get; set; }

        [Display(Name = "Resultado final")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string ResultadoFinal { get; set; }

        public int NumeroDeclaracao { get; set; }
        public string CodigoAutenticacao { get; set; }
    }
}
