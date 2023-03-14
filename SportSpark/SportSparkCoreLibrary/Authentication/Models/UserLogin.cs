using System.ComponentModel.DataAnnotations;

namespace SportSparkCoreLibrary.Authentication.Models;

public class UserLogin
{
    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }
}
