using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class HorarioServidor
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Unidade de Lotação")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Unidade { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [Display(Name = "Carga horária semanal")]
        public int CargaHorariaSemanal { get; set; }

        [Display(Name = "Vínculo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Vinculo { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Turno { get; set; }

        [Display(Name = "Horário de trabalho")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string TurnoHorario { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public string segunda { get; set; }
        public string terca { get; set; }
        public string quarta { get; set; }
        public string quinta { get; set; }
        public string sexta { get; set; }
        public string sabado { get; set; }
        public string domingo { get; set; }

    }
}
