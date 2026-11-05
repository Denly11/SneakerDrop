using System.ComponentModel.DataAnnotations;

namespace UserApi.Lesson08.Dtos;

public class CreateUserRequest
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Range(1, 150)]
    public int Age { get; set; }
}
