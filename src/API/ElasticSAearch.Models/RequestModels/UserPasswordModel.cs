using System.ComponentModel.DataAnnotations;

namespace ElasticSAearch.Models.RequestModels;

public class UserPasswordModel
{
    /// <summary>
    /// Foreign key referencing the user
    /// </summary>
    [Required(ErrorMessage ="User id is required")]
    public Guid UserId { get; set; }

    /// <summary>
    ///  Hashed password
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MaxLength(100)]
    public string Password { get; set; }

}
