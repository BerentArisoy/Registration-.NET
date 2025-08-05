using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Registration_Test;
using Registration_Test.Business;
using Registration_Test.DataAccess;
using Registration_Test.Entities;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        
        services.AddDbContext<UserDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=registration_db;username=postgres;password="));
        //services.AddScoped<IUserRepository, PostgresUserRepository>();
        //services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IUserRepository, MongoUserRepository>();
        services.AddScoped<IUserService, UserService>();

        
        var serviceProvider = services.BuildServiceProvider();
        var userService = serviceProvider.GetService<IUserService>();
        
        while (true)
        {
            Console.WriteLine("\n1 - Kullanıcı Ekle");
            Console.WriteLine("2 - Kullanıcıları Listele");
            Console.WriteLine("3 - Kullanıcı Sil");
            Console.WriteLine("4 - Kullanıcı Güncelle");
            Console.WriteLine("5 - Kullanıcı Ara");
            Console.WriteLine("0 - Çıkış");
            Console.Write("Seçim: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Ad Soyad: ");
                string ad = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();

                try
                {
                    userService.RegisterUser(new User { FullName = ad, Email = email });
                    Console.WriteLine("Kayıt başarılı.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
            else if (choice == "2")
            {
                var users = userService.GetAllUsers();
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id} - {user.FullName} ({user.Email})");
                }
            }
            else if (choice == "3")
            {
                Console.Write("Silinecek kullanıcı ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    bool result = userService.DeleteUser(id);
                    Console.WriteLine(result ? "Kullanıcı silindi." : "Kullanıcı bulunamadı.");
                }
                else
                {
                    Console.WriteLine("Geçersiz ID.");
                }
            }
            else if (choice == "4")
            {
                Console.Write("Güncellenecek kullanıcı ID: ");
                if (int.TryParse(Console.ReadLine(), out int updateId))
                {
                    var existingUser = userService.GetUserById(updateId);
                    if (existingUser == null)
                    {
                        Console.WriteLine("Kullanıcı bulunamadı.");
                        return;
                    }

                    Console.Write($"Yeni Ad Soyad (eski: {existingUser.FullName}): ");
                    string newName = Console.ReadLine();
                    Console.Write($"Yeni Email (eski: {existingUser.Email}): ");
                    string newEmail = Console.ReadLine();

                    existingUser.FullName = string.IsNullOrWhiteSpace(newName) ? existingUser.FullName : newName;
                    existingUser.Email = string.IsNullOrWhiteSpace(newEmail) ? existingUser.Email : newEmail;

                    bool result = userService.UpdateUser(existingUser);
                    if (result)
                        Console.WriteLine("Kullanıcı güncellendi.");
                    else
                        Console.WriteLine("Güncelleme başarısız oldu.");
                }
                else
                {
                    Console.WriteLine("Geçersiz ID.");
                }
            }
            else if (choice == "5")
            {
                Console.Write("Aranacak kelime (Ad veya Email): ");
                string keyword = Console.ReadLine();
                var results = userService.SearchUsers(keyword);
    
                if (results.Count == 0)
                {
                    Console.WriteLine("Eşleşen kullanıcı bulunamadı.");
                }
                else
                {
                    foreach (var user in results)
                    {
                        Console.WriteLine($"{user.Id} - {user.FullName} ({user.Email})");
                    }
                }
            }
            else if (choice == "0")
            {
                Console.WriteLine("Çıkış yapılıyor...");
                break;
            }
            else
                Console.WriteLine("Geçersiz seçim!");
        }
    }
}
