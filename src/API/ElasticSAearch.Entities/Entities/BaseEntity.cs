using System.ComponentModel.DataAnnotations;

namespace ElasticSAearch.Entities.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Unique identifier for each password entry
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }
}
