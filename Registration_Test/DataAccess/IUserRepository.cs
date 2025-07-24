using Registration_Test.Entities;
using System.Collections.Generic;

namespace Registration_Test.DataAccess;

public interface IUserRepository
{
    void Add(User user);
    bool Delete(int id);
    bool Update(User user);
    User GetById(int id);
    void SaveChanges();
    List<User> SearchUsers(string keyword);
    List<User> GetAll();
    bool EmailExists(string email);
}