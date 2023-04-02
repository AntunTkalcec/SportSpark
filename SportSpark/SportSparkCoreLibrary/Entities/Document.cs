using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSparkCoreLibrary.Entities;

[Table("Document")]
public class Document : BaseEntity
{
    [Required, MaxLength(250)]
    public string ImageTitle { get; set; }

    public byte[] ImageData { get; set; }

    #region Relations
    [NotMapped]
    public User Owner { get; set; }
    #endregion
}
