using System.ComponentModel.DataAnnotations;

namespace UserApi.Models;

public class CreateUserRequest
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [RegularExpression(".*@.*", ErrorMessage = "The Email field must contain '@'.")]
    public string Email { get; set; } = string.Empty;

    [Range(1, 150)]
    public int Age { get; set; }
}
