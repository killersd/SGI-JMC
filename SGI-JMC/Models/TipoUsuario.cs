using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGI_JMC.Models
{
    [Table("TipoUsuario")]
    public class TipoUsuario
    {
        [Display(Name = "Código")]

        [Column("Id")]
        public int Id { get; set; }


        [Display(Name = "Tipo Usuário")]
        [Column("NomeTipoUsuario")]
        public string NomeTipoUsuario { get; set; }

    }

}