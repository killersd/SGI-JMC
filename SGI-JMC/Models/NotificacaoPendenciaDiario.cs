using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class NotificacaoPendenciaDiario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do professor(a)")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Data final do período")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime dataLimite { get; set; }

        [Display(Name = "Data de emissão")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime dataDeEmissao { get; set; }

        [Display(Name = "Total de aulas sem registro")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int qtdAulas { get; set; }
    }
}
