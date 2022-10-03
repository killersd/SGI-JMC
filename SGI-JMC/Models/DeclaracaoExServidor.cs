using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGI_JMC.Models
{
    [Serializable]
    public class DeclaracaoExServidor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do servidor")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [StringLength(11, ErrorMessage = "O campo {0} deve ter {1} dígitos.")]
        public string CPF { get; set; }

        [Display(Name = "Vínculo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string vinculo { get; set; }

        [Display(Name = "Carga horária")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CargaHoraria { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string cargo { get; set; }

        [Display(Name = "Data do início do exercício")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataInicioExercicio { get; set; }

        [Display(Name = "Data final do exercício")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataFimExercicio { get; set; }

        [Display(Name = "Data emissão")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }
        public int numeroDeclaracaoExServidor { get; set; }
        public string codigoAutenticacaoExServidor { get; set; }
        [NotMapped]
        public int TempoDeServico { get => (int)Math.Floor((DataFimExercicio - DataInicioExercicio).TotalDays/* / 365.25*/); }
    }
}
