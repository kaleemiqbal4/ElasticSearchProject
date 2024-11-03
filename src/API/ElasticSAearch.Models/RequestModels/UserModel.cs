using System.ComponentModel.DataAnnotations;

namespace ElasticSAearch.Models.RequestModels;

public class UserModel
{
    [Required(ErrorMessage ="Name is required")]
    [StringLength(100, ErrorMessage ="Name can't be exceed than 100 characters")]
    public string Name { get;set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(255, ErrorMessage = "Email can't be exceed than 255 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Contact is required")]
    [StringLength(13, ErrorMessage = "Contact can't be exceed than 13 characters")]
    public string Contact { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password can't be exceed than 100 characters")]
    public string Password { get; set; }
}
