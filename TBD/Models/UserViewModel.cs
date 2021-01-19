using System.ComponentModel.DataAnnotations;
using TBD.Enums;

namespace TBD.Models
{
    public class UserViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
