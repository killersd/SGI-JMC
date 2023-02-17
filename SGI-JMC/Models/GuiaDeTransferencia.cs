using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class GuiaDeTransferencia
    {
        public DateTime DataEmissao { get; set; }
        [Display(Name = "Nome do aluno")]
        public string NomeAluno { get; set; }
        [Display(Name = "Nome do pai")]
        public string NomePai { get; set; }
        [Display(Name = "Nome da mãe")]
        public string NomeMae { get; set; }
        [Display(Name = "Ano letivo")]
        public int AnoLetivo { get; set; }
        [Display(Name = "Ano/Série")]
        public int AnoSerie { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataDeNascimento { get; set; }



        public int numeroTransferencia { get; set; }
        public string codigoAutenticacaoTransferencia { get; set; }
    }
}
