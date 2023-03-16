using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class DeclaracaoProSic
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

        [Display(Name = "Fase do ProSic")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(3, 4, ErrorMessage = "fase deve ser 3 ou 4 - 3 para 6º e 7º anos e 4 para 8º e 9º")]
        public int faseProSic { get; set; }

        [Display(Name = "Série de Origem")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(6, 9, ErrorMessage = "este campo deve estar entre 6º e 9º ano")]
        public int serieOrigem { get; set; }

        [Display(Name = "Número do NIS")]
        public string numeroDoNis { get; set; }

        [Display(Name = "Código do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string codigoSeed { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime dataDeEmissao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Quantidade de faltas")]
        public int qtdFaltas { get; set; }

        public int numeroDeclaracao { get; set; }
        public string codigoAutenticacao { get; set; }
    }
}

