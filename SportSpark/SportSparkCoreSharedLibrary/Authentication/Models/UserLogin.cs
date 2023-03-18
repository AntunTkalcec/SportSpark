using System.ComponentModel.DataAnnotations;

namespace SportSparkCoreSharedLibrary.Authentication.Models;

public class UserLogin
{
    [Required]
    public string EmailOrUserName { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }
}
