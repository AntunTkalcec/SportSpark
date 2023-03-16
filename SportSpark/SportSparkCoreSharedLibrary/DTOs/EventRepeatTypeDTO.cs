using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class EventRepeatTypeDTO : BaseDTO
    {
        public string Description { get; set; }

        #region Relations
        public ICollection<EventDTO> Events { get; set; }
        #endregion
    }
}
