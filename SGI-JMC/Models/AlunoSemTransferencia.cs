using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    [Serializable]
    public class AlunoSemTransferencia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Nome do aluno")]
        public string Nome { get; set; }

        [Display(Name = "Nome do pai")]
        public string NomePai { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Nome da mãe")]
        public string NomeMae { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNasciimento { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Escola anterior")]
        public string EscolaAnterior { get; set; }

        public int TemPendencia { get; set; }
    }
}
