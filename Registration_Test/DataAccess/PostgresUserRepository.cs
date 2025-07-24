using Registration_Test.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Registration_Test.DataAccess;

public class PostgresUserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public PostgresUserRepository(UserDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public bool Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }
    
    public bool Update(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null) return false;

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
        return _context.Users.AsNoTracking().ToList();
    }

    public bool EmailExists(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
    
    public List<User> SearchUsers(string keyword)
    {
        return _context.Users
            .Where(u => EF.Functions.ILike(u.FullName, $"%{keyword}%") ||
                        EF.Functions.ILike(u.Email, $"%{keyword}%"))
            .ToList();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}