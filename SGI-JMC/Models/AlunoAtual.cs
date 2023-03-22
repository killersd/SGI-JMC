using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace SGI_JMC.Models
{
    public class AlunoAtual
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Nome do aluno")]
        public string Nome { get; set; }

        [Display(Name = "Nome do pai")]
        public string Pai { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Nome da mãe")]
        public string Mae { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(15, ErrorMessage = "O campo {0} deve ter {1} dígitos.")]
        public string Telefone { get; set; }

        public int numeroDeclaracao { get; set; }
        public string codigoAutenticacao { get; set; }

        [Display(Name = "Código do aluno")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string codigoSeed { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime dataDeEmissao { get; set; }

        [Display(Name = "Ano letivo")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int anoLetivo { get; set; }

        [Display(Name = "Ano/Série")]
        public int? anoSerie { get; set; }

        [Display(Name = "Turma")]
        public char? turma { get; set; }

        [Display(Name = "Fase ProSic")]
        public int? FaseProSic { get; set; }

        [Display(Name = "Série de origem")]
        public int? SerieOrigem { get; set; }

        [Display(Name = "Número do NIS")]
        public string NumeroDoNis { get; set; }

        [Display(Name = "Correção de Fluxo?")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CorrecaoDeFluxo { get; set; }

        public bool Transferido { get; set; }

    }
}
