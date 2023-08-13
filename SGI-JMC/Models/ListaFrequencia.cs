using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGI_JMC.Models
{
    [NotMapped]
    public class ListaFrequencia
    {
        [Display(Name = "Ano")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Ano { get; set; }

        [Display(Name = "Turma")]
        public string Turma { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Turno { get; set; }

        [Display(Name = "Mês")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Mes { get; set; }

        public string NomeDoAluno { get; set; }
        public int Numero { get; set; }

    }
}
