using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class Frequencia
    {
        public int Id { get; set; }
        [Display(Name = "Quantidade de faltas")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public int QuantidadeFaltas { get; set; }
    }
}
