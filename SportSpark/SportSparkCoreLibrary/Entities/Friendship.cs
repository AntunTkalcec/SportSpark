using SportSparkCoreLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSparkCoreLibrary.Entities;

[Table("Friendship")]
public class Friendship
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int User2Id { get; set; }

    public FriendshipStatus Status { get; set; }

    public User User1 { get; set; }

    public User User2 { get; set; }
}
