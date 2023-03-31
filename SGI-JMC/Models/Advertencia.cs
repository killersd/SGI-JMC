using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class Advertencia
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Nome do pai")]
        public string Father_name { get; set; }

        [Display(Name = "Nome da mãe")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Mother_name { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime Birth_date { get; set; }        

        [Display(Name = "Ano/Série/Fase")]
        [MaxLength(2)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string anoSerie { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string turma { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime dataDeEmissao { get; set; }

        [Display(Name = "Descrição do fato")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string descricaoDoFato { get; set; }

        [Display(Name="Turno")]
        [Required(ErrorMessage ="Este campo é obrigatório")]
        public string turno { get; set; }

    }
}
