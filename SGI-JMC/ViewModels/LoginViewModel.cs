using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário")]
         [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Lembrar de mim")]
        public bool Lembrar { get; set; }

        public string ReturnUrl { get; set; }
    }
}
