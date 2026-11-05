namespace UserApi.Lesson08.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = default!;

    public string Email { get; set; } = default!;

    public int Age { get; set; }
}
