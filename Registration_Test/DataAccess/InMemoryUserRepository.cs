using System.Collections.Generic;
using System.IO;
using System.Linq;
using Registration_Test.Entities;

namespace Registration_Test.DataAccess;

public class InMemoryUserRepository : IUserRepository
{
    private List<User> _users;
    private int _idCounter;
    private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "users.txt");

    public InMemoryUserRepository()
    {
        _users = new List<User>();
        if (File.Exists(FilePath))
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int id))
                {
                    _users.Add(new User
                    {
                        Id = id,
                        FullName = parts[1],
                        Email = parts[2]
                    });
                }
            }

            _idCounter = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        }
        else
        {
            _idCounter = 1;
        }
    }
    
    public void Add(User user)
    {
        user.Id = _idCounter++;
        _users.Add(user);
        SaveToFile();
    }
    
    public bool Delete(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
            SaveToFile();
            return true;
        }
        return false;
    }
    
    public bool Update(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null) return false;

        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        return true;
    }

    
    public User GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }


    public List<User> GetAll() => _users;

    public bool EmailExists(string email) => _users.Any(u => u.Email == email);
    
    public List<User> SearchUsers(string keyword)
    {
        return _users
            .Where(u => u.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                        || u.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    
    private void SaveToFile()
    {
        try
        {
            var lines = _users.Select(u => $"{u.Id},{u.FullName},{u.Email}");
            File.WriteAllLines(FilePath, lines);
            Console.WriteLine("Kullanıcılar başarıyla kaydedildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Dosya kaydı hatası: {ex.Message}");
        }
    }
    public void SaveChanges()
    {
        SaveToFile();
    }
}