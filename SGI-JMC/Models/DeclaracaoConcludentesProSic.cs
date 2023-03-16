using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class DeclaracaoConcludentesProSic
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Nome do pai")]
        public string NomeDoPai { get; set; }

        [Display(Name = "Nome da mãe")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string NomeDaMae { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Ano letivo")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int AnoLetivo { get; set; }

        [Display(Name = "Fase ProSic")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(3, 4, ErrorMessage = "fase deve ser 3 ou 4 - 3 para 6º e 7º anos e 4 para 8º e 9º")]
        public string FaseProSic { get; set; }

        [Display(Name = "Série de origem")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(8, 9, ErrorMessage = "Aluno não é concludente, este campo deve estar entre 8º e 9º ano")]
        public string SerieDeOrigem { get; set; }

        [Display(Name = "Número do NIS")]
        public string NumeroDoNis { get; set; }

        [Display(Name = "Código do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CodigoSeed { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime DataDeEmissao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Resultado final")]
        public string ResultadoFinal { get; set; }

        public int numeroDeclaracaoConcludenteProSic { get; set; }
        public string codigoAutenticacaoConcludenteProSic { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
