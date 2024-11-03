using POCProject.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticSAearch.Entities.Entities;

[Table(TableConstants.UserTableName)]
public class UserEntity: BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(15)]
    public string Contact { get; set; }

    [Required]
    [StringLength(15)]
    public string Email { get; set; }
}
