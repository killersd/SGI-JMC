using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class OficioAssumiuFuncaoServidor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do servidor")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Name { get; set; }

        [Display(Name = "Número do ofício")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public int NumeroOficio { get; set; }

        [Display(Name = "Assunto")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Assunto { get; set; }

        [Display(Name = "Destinatário")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string destinatario { get; set; }

        [Display(Name = "Data que assumiu função")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataAssumiuFuncao { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [StringLength(11, ErrorMessage = "O campo {0} deve ter {1} dígitos.")]
        public string CPF { get; set; }

        [Display(Name = "Vínculo do servidor")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string vinculo { get; set; }

        [Display(Name = "Carga horária")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CargaHoraria { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string cargo { get; set; }

        [Display(Name = "Data emissão")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Cidade do destinatário")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CidadeDestinatario { get; set; }

        [Display(Name = "Cargo do destinatário")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CargoDestinatario { get; set; }
    }
}
