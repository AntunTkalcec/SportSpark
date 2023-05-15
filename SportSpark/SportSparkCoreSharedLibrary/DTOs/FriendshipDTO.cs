using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class FriendshipDTO : BaseDTO
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public int Status { get; set; }

        public UserDTO Sender { get; set; }

        public UserDTO Receiver { get; set; }
    }
}
