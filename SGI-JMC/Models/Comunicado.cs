using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class Comunicado
    {
        [Key]
        public int Id { get; set; }   

        [Display(Name = "Nome do aluno")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string NomeAluno { get; set; }


        [Display(Name = "Informações adicionais")]
        public string Observacao { get; set; }


        [Display(Name = "Ano/Série")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string AnoSerie { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Turno { get; set; }


        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Turma { get; set; }

        [Display(Name = "Membro da equipe diretiva")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string MembroEquipeDiretiva { get; set; }

        [Display(Name = "Data emissão")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Data para comparecer na escola")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DataComparecimento { get; set; }
    }
}
