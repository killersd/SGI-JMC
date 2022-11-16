using System.ComponentModel.DataAnnotations;
using System;

namespace SGI_JMC.Models
{
    public class DeclaracaoGenerica
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Conteudo da declaração")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string CorpoDaDeclaracao { get; set; }

        public DateTime DataEmissao { get; set; }
        public int numeroDeclaracaoGenerica { get; set; }
        public string codigoAutenticacaoDeclaracaoGenerica { get; set; }
    }
}
