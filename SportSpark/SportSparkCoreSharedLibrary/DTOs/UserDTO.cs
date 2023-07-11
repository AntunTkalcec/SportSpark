using SportSparkCoreSharedLibrary.DTOs.Base;
using SportSparkCoreSharedLibrary.Authentication.Models;
using System.ComponentModel.DataAnnotations;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class UserDTO : BaseDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "This is not a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Need 6-20 characters, 1 uppercase letter, 1 lowercase letter and 1 number.")]
        public string Password { get; set; }

        public string? Bio { get; set; }

        public decimal? Rating { get; set; }

        public int? VoteCount { get; set; }

        public int? Age { get; set; }

        public double? DesiredRadius { get; set; }

        #region relations
        public int? ProfileImageId { get; set; }
        public DocumentDTO ProfileImage { get; set; }

        public byte[] ProfileImageData { get; set; }

        public ICollection<EventDTO> Events { get; set; }

        public ICollection<FriendshipDTO> RequestedFriendships { get; set; }

        public ICollection<FriendshipDTO> ReceivedFriendships { get; set; }
        #endregion

        public AuthenticationInfo AuthenticationInfo { get; set; }
    }
}
