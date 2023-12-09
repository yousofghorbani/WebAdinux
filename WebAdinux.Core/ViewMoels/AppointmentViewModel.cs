using System.ComponentModel.DataAnnotations;

namespace WebAdinux.Core.ViewMoels;

public class GetAppointmentViewModel : AppointmentViewModel
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
public class AppointmentViewModel
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