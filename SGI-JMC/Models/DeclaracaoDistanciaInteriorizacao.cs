using System;
using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class DeclaracaoDistanciaInteriorizacao
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do servidor")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "CPF do servidor")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CPF { get; set; }

        [Display(Name = "RG do servidor")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string RG { get; set; }

        [Display(Name = "Orgão expedidor do RG")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string OrgaoExpedidor { get; set; }

        [Display(Name = "Início do exercício")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime InicioExercicio { get; set; }

        [Display(Name = "Carga horária semanal")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CargaHorariaSemanal { get; set; }

        [Display(Name = "Distância em Km")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Distancia { get; set; }

        [Display(Name = "Cidade origem")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Origem { get; set; }

        [Display(Name = "Cidade destino")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Destino { get; set; }

        [Display(Name = "Turmas nas quais leciona")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Turmas { get; set; }

        [Display(Name = "Cidade de entrada")]
        public string CidadeEntrada { get; set; }

        [Display(Name = "Estado qual reside")]
        public string EstadoResidencia { get; set; }

        [Display(Name = "Data de emissão")]
        [DataType(DataType.Date)]
        public DateTime DataDeEmissao { get; set; }

        public int NumeroDeclaracaoDistanciaInteriorizacao { get; set; }
        public string CodigoAutenticacaoDeclaracaoInteriorizacao { get; set; }


    }
}
