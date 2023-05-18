using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSparkCoreLibrary.Entities;

[Table("User")]
public class User : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    [StringLength(500)]
    public string? Bio { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal? Rating { get; set; }

    public int? Age { get; set; }

    public double? DesiredRadius { get; set; }

    #region relations
    public int? ProfileImageId { get; set; }
    public Document ProfileImage { get; set; }

    public ICollection<Event> Events { get; set; }

    public virtual ICollection<Friendship> RequestedFriendships { get; set; }
    public virtual ICollection<Friendship> ReceivedFriendships { get; set; }
    #endregion
}
