using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class OficioGeral
    {
        [Key]
        public int Id { get; set; }
        

        [Display(Name = "Número do ofício")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public int NumeroOficio { get; set; }

        [Display(Name = "Assunto")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Assunto { get; set; }

        [Display(Name = "Destinatário")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string destinatario { get; set; }

          [Display(Name = "Cargo do destinatário")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CargoDoDestinatario { get; set; }

        [Display(Name = "Corpo do ofício")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CorpoDoOficio { get; set; }

        [Display(Name = "Remetente")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Remetente { get; set; }

        [Display(Name = "Data emissão")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }
    }
}
