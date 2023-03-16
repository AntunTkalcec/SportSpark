using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class DocumentDTO : BaseDTO
    {
        public string BlobId { get; set; }

        public UserDTO Owner { get; set; }
    }
}
