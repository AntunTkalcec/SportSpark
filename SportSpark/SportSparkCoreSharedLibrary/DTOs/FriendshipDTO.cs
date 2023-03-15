using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class FriendshipDTO : BaseDTO
    {
        public int UserId { get; set; }

        public int User2Id { get; set; }

        public int Status { get; set; }

        public UserDTO User1 { get; set; }

        public UserDTO User2 { get; set; }
    }
}
