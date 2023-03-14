using SportSparkCoreSharedLibrary.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string? Bio { get; set; }
        public decimal? Rating { get; set; }

        public int? Age { get; set; }

        #region relations
        public int? ProfileImageId { get; set; }
        public DocumentDTO ProfileImage { get; set; }

        public ICollection<EventDTO> Events { get; set; }

        public ICollection<FriendshipDTO> Friendships { get; set; }
        #endregion
    }
}
