using UserApi.Lesson08.Dtos;
using UserApi.Lesson08.Entities;

namespace UserApi.Lesson08.Repositories;

public class UserRepository
{
    private readonly List<User> _users = [];
    private int _lastId;

    public User? Add(CreateUserRequest request)
    {
        var emailTaken = _users.Any(u => string.Equals(u.Email, request.Email, StringComparison.OrdinalIgnoreCase));
        if (emailTaken)
        {
            return null;
        }

        var user = new User
        {
            Id = ++_lastId,
            Username = request.Username,
            Email = request.Email,
            Age = request.Age,
        };

        _users.Add(user);
        return user;
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public List<User> Search(UserFilter filter)
    {
        IEnumerable<User> result = _users;

        if (!string.IsNullOrEmpty(filter.Name))
        {
            result = result.Where(u => u.Username.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.MinAge is not null)
        {
            result = result.Where(u => u.Age >= filter.MinAge);
        }

        if (filter.MaxAge is not null)
        {
            result = result.Where(u => u.Age <= filter.MaxAge);
        }

        if (filter.Limit is not null)
        {
            result = result.Take(filter.Limit.Value);
        }

        return result.ToList();
    }
}
