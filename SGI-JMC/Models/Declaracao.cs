using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class Declaracao
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Nome do pai")]
        public string Father_name { get; set; }

        [Display(Name = "Nome da mãe")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Mother_name { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime Birth_date { get; set; }
        
        [Display(Name = "Ano letivo")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int anoLetivo { get; set; }

        [Display(Name = "Ano/Série")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int anoSerie { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public char turma { get; set; }

        [Display(Name = "Número do NIS")]
        public string numeroDoNis { get; set; }

        [Display(Name = "Código do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string codigoSeed { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime dataDeEmissao { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [Display(Name = "Quantidade de faltas")]
        public int qtdFaltas { get; set; }

        public int numeroDeclaracao { get; set; }
        public string codigoAutenticacao { get; set; }
    }
}
