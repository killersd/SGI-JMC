using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class DeclaracaoSabadoLetivo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do servidor")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Name { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        //[StringLength(11, ErrorMessage = "O campo {0} deve ter {1} dígitos.")]
        public string CPF { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime dataDeEmissao { get; set; }

        [Display(Name = "Ano letivo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public int anoLetivo { get; set; }

        public int numeroDeclaracaoSabado { get; set; }
        public string codigoAutenticacaoSabado { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [Display(Name = "Data do sábado letivo")]
        [DataType(DataType.Date)]
        public DateTime dataSabado { get; set; }

        [Display(Name = "Cargo do servidor")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CargoServidor { get; set; }


        [Display(Name = "Turno de trabalho")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string TurnoDeTrabalho { get; set; }

    }
}
