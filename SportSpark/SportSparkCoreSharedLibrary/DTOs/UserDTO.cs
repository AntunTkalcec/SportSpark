using SportSparkCoreSharedLibrary.DTOs.Base;
using SportSparkCoreSharedLibrary.Authentication.Models;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Bio { get; set; }
        public decimal? Rating { get; set; }

        public int? Age { get; set; }

        public double? DesiredRadius { get; set; }

        #region relations
        public int? ProfileImageId { get; set; }
        public DocumentDTO ProfileImage { get; set; }

        public ICollection<EventDTO> Events { get; set; }

        public ICollection<FriendshipDTO> RequestedFriendships { get; set; }

        public ICollection<FriendshipDTO> ReceivedFriendships { get; set; }
        #endregion

        public AuthenticationInfo AuthenticationInfo { get; set; }
    }
}
