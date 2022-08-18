using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class aluno
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }

        public string Father_name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Mother_name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Birth_date { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Year_folder { get; set; }


    }
}
