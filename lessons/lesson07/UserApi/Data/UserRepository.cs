using UserApi.Dtos;
using UserApi.Entities;

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

    public List<User> Search(string? name, int? min_age, int? max_age, int? limit)
    {
        IEnumerable<User> Query = _users;//.Where() vrací IEnumerable
        if (name != null)
        {
            Query = Query.Where(n => n.Username.Contains(name, StringComparison.OrdinalIgnoreCase));//aby to bralo jak velká ta malá písmena tak tam místo jen == je .Contains()
        }

        if (min_age != null)
        {
            Query = Query.Where(m => m.Age >= min_age);
        }

        if (max_age != null)
        {
            Query = Query.Where(m => m.Age <= max_age);
        }

        if (limit != null && limit != 0)
        {
            Query = Query.Take(limit.Value);
        }

        return Query.ToList();
    }

    public User? Edit(int id, CreateUserRequest request)
    {

        var FoundUser = GetById(id);

        if (FoundUser == null)       
            return null;     

        var emailTaken = _users.Any(u => u.Id != id && string.Equals(u.Email, request.Email, StringComparison.OrdinalIgnoreCase));//je shodný s nějakým emailem ale zároven to není zápis s naším id
        if (emailTaken)
            return null;

        FoundUser.Username = request.Username;
        FoundUser.Email = request.Email;
        FoundUser.Age = request.Age;
        return FoundUser;
    }

    public bool EmailIsUsed(string email, int id)
    {
        var FoundUser = _users.FirstOrDefault(u => u.Id != id && string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        if (FoundUser == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool Delete(int id)
    {
        var user = GetById(id);
        if (user == null)
        {
            return false;
        }

        _users.Remove(user);
        return true;
    }
}
