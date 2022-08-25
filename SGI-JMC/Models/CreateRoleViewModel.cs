using System.ComponentModel.DataAnnotations;

namespace SGI_JMC.Models
{
    public class CreateRoleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
