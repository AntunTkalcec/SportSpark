using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSparkCoreLibrary.Entities;

[Table("Document")]
public class Document : BaseEntity
{
    [Required, MaxLength(511)]
    public string BlobId { get; set; }

    #region Relations
    [NotMapped]
    public User Owner { get; set; }
    #endregion
}
