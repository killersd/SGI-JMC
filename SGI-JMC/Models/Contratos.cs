using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class Contratos
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do servidor")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Cargo { get; set; }

        [Display(Name = "Início do contrato")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime InicioContrato { get; set; }

        [Display(Name = "Fim do contrato")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime FimContrato { get; set; }

        [Display(Name = "Tempo de contrato")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public int TempoDeContrato { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CPF { get; set; }

        public bool renovado { get; set; }
    }
}
