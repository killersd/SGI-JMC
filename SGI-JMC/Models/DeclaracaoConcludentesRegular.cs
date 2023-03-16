using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class DeclaracaoConcludentesRegular
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

        [Display(Name = "Ano/Série")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(9, 9, ErrorMessage = "Aluno não é concludente, este campo deve ser 9º ano")]
        public int AnoSerie { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public char Turma { get; set; }

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

        public int numeroDeclaracaoConcludente { get; set; }
        public string codigoAutenticacaoConcludente { get; set; }
        
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
