using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class aluno
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Nome do aluno")]
        public string Name { get; set; }

        [Display(Name = "Nome do pai")]
        public string Father_name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Nome da mãe")]
        public string Mother_name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime Birth_date { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Ano da pasta")]
        public string Year_folder { get; set; }


    }
}
