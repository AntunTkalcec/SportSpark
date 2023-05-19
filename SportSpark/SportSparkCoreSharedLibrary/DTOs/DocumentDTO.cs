using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class DocumentDTO : BaseDTO
    {
        public string ImageTitle { get; set; }

        public byte[] ImageData { get; set; }

        public UserDTO Owner { get; set; }
    }
}
