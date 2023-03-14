using SportSparkCoreLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkCoreLibrary.Entities;

[Table("Event")]
public class Event : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    [StringLength(150)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(8, 6)")]
    public decimal? Lat { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal? Long { get; set; }

    public string? TimeOfDay { get; set; }

    public string? Duration { get; set; }

    public string? Price { get; set; }

    public int? NumberOfParticipants { get; set; }

    [Required]
    public Privacy Privacy { get; set; }

    #region Relations
    public int UserId { get; set; }

    public User User { get; set; }
    
    public int TypeId { get; set; }

    public EventType EventType { get; set; }
    
    public int RepeatTypeId { get; set; }

    public EventRepeatType RepeatType { get; set; }
    #endregion
}
