using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSparkCoreLibrary.Entities;

[Table("EventRepeatType")]
public class EventRepeatType : BaseEntity
{
    [StringLength(250)]
    public string Description { get; set; }

    #region Relations
    public ICollection<Event> Events { get; set; }
    #endregion
}
