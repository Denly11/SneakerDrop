using UserApi.Entities;
using UserApi.Models;

namespace UserApi.Data;

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

     var FoundUser =_users.FirstOrDefault(u => u.Id == id);

        if(FoundUser == null)
        {
            return null;
        }
        else
        {
            return FoundUser;
        }


    }
}
