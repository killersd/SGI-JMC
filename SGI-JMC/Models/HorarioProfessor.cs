using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGI_JMC.Models
{
    [Serializable]
    public class HorarioProfessor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [StringLength(11, ErrorMessage = "O campo {0} deve possuir {1} caracteres!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [Display(Name = "Carga horária semanal")]
        public int CargaHorariaSemanal { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        [NotMapped]
        public string[,] HorarioManha { get; set; }
        [NotMapped]
        public string[,] HorarioTarde { get; set; }

        //Horários segunda
        public string s01 { get; set; }
        public string s02 { get; set; }
        public string s03 { get; set; }
        public string s04 { get; set; }
        public string s05 { get; set; }

        //Horários terça
        public string t01 { get; set; }
        public string t02 { get; set; }
        public string t03 { get; set; }
        public string t04 { get; set; }
        public string t05 { get; set; }

        //Horários quarta
        public string q01 { get; set; }
        public string q02 { get; set; }
        public string q03 { get; set; }
        public string q04 { get; set; }
        public string q05 { get; set; }

        //Horários quinta
        public string qu01 { get; set; }
        public string qu02 { get; set; }
        public string qu03 { get; set; }
        public string qu04 { get; set; }
        public string qu05 { get; set; }

        //Horários sexta
        public string se01 { get; set; }
        public string se02 { get; set; }
        public string se03 { get; set; }
        public string se04 { get; set; }
        public string se05 { get; set; }

        //Horários sábado
        public string sab01 { get; set; }
        public string sab02 { get; set; }
        public string sab03 { get; set; }
        public string sab04 { get; set; }
        public string sab05 { get; set; }

        //Horários segunda
        public string ts01 { get; set; }
        public string ts02 { get; set; }
        public string ts03 { get; set; }
        public string ts04 { get; set; }
        public string ts05 { get; set; }

        //Horários terça
        public string tt01 { get; set; }
        public string tt02 { get; set; }
        public string tt03 { get; set; }
        public string tt04 { get; set; }
        public string tt05 { get; set; }

        //Horários quarta
        public string tq01 { get; set; }
        public string tq02 { get; set; }
        public string tq03 { get; set; }
        public string tq04 { get; set; }
        public string tq05 { get; set; }

        //Horários quinta
        public string tqu01 { get; set; }
        public string tqu02 { get; set; }
        public string tqu03 { get; set; }
        public string tqu04 { get; set; }
        public string tqu05 { get; set; }

        //Horários sexta
        public string tse01 { get; set; }
        public string tse02 { get; set; }
        public string tse03 { get; set; }
        public string tse04 { get; set; }
        public string tse05 { get; set; }

        //Horários sábado
        public string tsab01 { get; set; }
        public string tsab02 { get; set; }
        public string tsab03 { get; set; }
        public string tsab04 { get; set; }
        public string tsab05 { get; set; }

    }
}
