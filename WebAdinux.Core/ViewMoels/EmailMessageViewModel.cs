using System.ComponentModel.DataAnnotations;

namespace WebAdinux.Core.ViewMoels;

public class GetEmailMessageViewModel : EmailMessageViewModel
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
public class EmailMessageViewModel
{
    [Required, MaxLength(100)]
    public string Name { get; set; }
    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; }
    [Required, MaxLength(255)]
    public string subject { get; set; }
    [Required, MaxLength(2000)]
    public string MessageContent { get; set; }
}