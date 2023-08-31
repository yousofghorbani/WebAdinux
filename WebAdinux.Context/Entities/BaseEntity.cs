using System.ComponentModel.DataAnnotations;

namespace WebAdinux.Context.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModiFiedAt { get; set; } = DateTime.Now;
    }
}
