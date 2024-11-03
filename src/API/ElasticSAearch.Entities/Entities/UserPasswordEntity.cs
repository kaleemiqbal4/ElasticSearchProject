using POCProject.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticSAearch.Entities.Entities;

[Table(TableConstants.UserPasswordTableName)]
public class UserPasswordEntity : BaseEntity
{
    /// <summary>
    /// Foreign key referencing the user
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///  Hashed password
    /// </summary>
    [MaxLength(512)]
    public string Hash { get; set; }

    /// <summary>
    /// Salt used for hashing the password
    /// </summary>
    [MaxLength(64)]
    public string Salt { get; set; }

    /// <summary>
    /// Navigation property to the User class
    ///  Assuming you have a User class defined
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public virtual UserEntity Users { get; set; }
}