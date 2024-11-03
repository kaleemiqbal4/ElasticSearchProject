using POCProject.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticSAearch.Entities.Entities;

[Table(TableConstants.UserRoleTableName)]
public class RoleEntity : BaseEntity
{
    /// <summary>
    /// User Unique identifier for the role
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// User Role Name
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}
