using Registration_Test.Entities;
using Microsoft.EntityFrameworkCore;
using Registration_Test.DataAccess;

namespace Registration_Test;

public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public EfUserRepository()
    {
        _context = new AppDbContext();
        _context.Database.Migrate(); // automated migrate
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public bool Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }
    public bool Update(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
            return false;

        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;

        _context.SaveChanges();
        return true;
    }
    
    public User GetById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }


    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public bool EmailExists(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
    
    public List<User> SearchUsers(string keyword)
    {
        return _context.Users
            .Where(u => u.FullName.ToLower().Contains(keyword.ToLower()) ||
                        u.Email.ToLower().Contains(keyword.ToLower()))
            .ToList();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}