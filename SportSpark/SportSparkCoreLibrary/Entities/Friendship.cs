using SportSparkCoreLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSparkCoreLibrary.Entities;

[Table("Friendship")]
public class Friendship
{
    [Key, Column(Order = 1)]
    public int UserId { get; set; }

    [Key, Column(Order = 2)]
    public int User2Id { get; set; }

    public FriendshipStatus Status { get; set; }

    [NotMapped]
    public User User1 { get; set; }

    [NotMapped]
    public User User2 { get; set; }
}
