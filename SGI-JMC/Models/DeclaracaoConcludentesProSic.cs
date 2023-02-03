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
        public string FaseProSic { get; set; }

        [Display(Name = "Série de origem")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
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
