using SportSparkCoreLibrary.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSparkCoreLibrary.Entities;

[Table("Friendship")]
public class Friendship
{
    public int Id { get; set; }
    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public FriendshipStatus Status { get; set; }

    public User Sender { get; set; }

    public User Receiver { get; set; }
}
