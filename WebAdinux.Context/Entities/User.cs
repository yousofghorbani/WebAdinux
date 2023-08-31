using System.ComponentModel.DataAnnotations;

namespace WebAdinux.Context.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public short Role { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required, MaxLength(255)]
        public string Password { get; set; }
    }
}
