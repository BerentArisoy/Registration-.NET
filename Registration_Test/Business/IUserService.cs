using Registration_Test.Entities;
using System.Collections.Generic;
namespace Registration_Test.Business;

public interface IUserService
{
    void RegisterUser(User user);
    bool DeleteUser(int id);
    bool UpdateUser(User updatedUser);
    User GetUserById(int id);
    List<User> SearchUsers(string keyword);
    void Save();
    List<User> GetAllUsers();
    
}