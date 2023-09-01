using System.ComponentModel.DataAnnotations;

namespace WebAdinux.Core.ViewMoels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(4)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
