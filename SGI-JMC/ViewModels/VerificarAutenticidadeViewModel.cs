using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.ViewModels
{
    public class VerificarAutenticidadeViewModel
    {
        [Display(Name = "Número do documento")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public int NumeroDeclaracao { get; set; }

        [Display(Name = "Código de verificação")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string CodigoDeVerificacao { get; set; }
    
    }
}
