using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class EventTypeDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        #region Relations
        public ICollection<EventDTO> Events { get; set; }
        #endregion
    }
}
