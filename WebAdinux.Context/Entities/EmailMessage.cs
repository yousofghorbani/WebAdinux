using System.ComponentModel.DataAnnotations;

namespace WebAdinux.Context.Entities
{
    public class EmailMessage : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }
        [Required, MaxLength(255)]
        public string subject { get; set; }
        [Required, MaxLength(2000)]
        public string MessageContent { get; set; }
        public bool IsArchived { get; set; }
    }
}
