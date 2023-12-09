using System.ComponentModel.DataAnnotations;

namespace WebAdinux.Context.Entities
{
    public class Appointment : BaseEntity
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }
        [Required, MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        public DateTime PreferredDate { get; set; }
        public string? Description { get; set; }
    }
}