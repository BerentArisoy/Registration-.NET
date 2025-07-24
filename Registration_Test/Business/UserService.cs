using System.Text.RegularExpressions;
using Registration_Test.DataAccess;
using Registration_Test.Entities;

namespace Registration_Test.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.FullName) || string.IsNullOrEmpty(user.Email))
            throw new Exception("Ad ve e-posta zorunludur.");
        
        if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new Exception("Geçerli bir e-posta adresi giriniz.");

        if (_userRepository.EmailExists(user.Email))
            throw new Exception("Bu e-posta zaten kayıtlı.");

        _userRepository.Add(user);
    }
    
    public bool DeleteUser(int id)
    {
        return _userRepository.Delete(id);
    }
    
    public bool UpdateUser(User updatedUser)
    {
        var existingUser = _userRepository.GetById(updatedUser.Id);
        if (existingUser == null)
            return false;

        existingUser.FullName = updatedUser.FullName;
        existingUser.Email = updatedUser.Email;
        return _userRepository.Update(existingUser);
    }
    
    public User GetUserById(int id)
    {
        return _userRepository.GetById(id);
    }
    public List<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }
    
    public List<User> SearchUsers(string keyword)
    {
        return _userRepository.SearchUsers(keyword);
    }

    public void Save()
    {
        _userRepository.SaveChanges();
    }
}