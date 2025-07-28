# 🧾 Registration_Test (.NET Console App + MongoDB Integration)

Bu proje, kullanıcı kayıt ve yönetimini destekleyen bir **.NET Core Console** uygulamasıdır. Başlangıçta In-Memory, PostgreSQL gibi veritabanlarıyla çalışan yapı, daha sonra **MongoDB** entegrasyonu ile genişletilmiştir. Kullanıcılar eklenebilir, silinebilir, güncellenebilir ve filtrelenebilir.

---

## 📌 Özellikler

- 🧍 Kullanıcı oluşturma, silme, güncelleme
- 🔍 Ad veya e-posta ile arama (MongoDB regex destekli)
- 💾 MongoDB ile kalıcı veri saklama
- 🗃 Repository pattern & katmanlı mimari
- 📦 .NET Dependency Injection (DI) altyapısı
  
---

## 🧰 Kullanılan Teknolojiler

- **.NET 9.0** (Console App)
- **MongoDB** (MongoDB Atlas veya MongoDB Compass ile kullanılabilir)
- **C# 12**
- **Katmanlı Mimari:**
  - `Business/` (Servis katmanı)
  - `DataAccess/` (MongoDB, PostgreSQL, InMemory repository’ler)
  - `Entities/` (User modeli)
  - `Program.cs` (Uygulama giriş noktası)

---

## 📦 Kullanılan NuGet Paketleri

| Paket Adı | Açıklama |
|----------|----------|
| `MongoDB.Driver` | MongoDB bağlantısı ve CRUD işlemleri için resmi C# sürücüsü |
| `Microsoft.Extensions.DependencyInjection` | Katmanlar arasında bağımlılık enjeksiyonu (DI) sağlar |
| `Microsoft.EntityFrameworkCore` | (Eski PostgreSQL desteği içindi – hâlen referans olarak kalabilir) |
| `Microsoft.EntityFrameworkCore.Design` | (Migration yönetimi için – yalnızca EF Core ile) |

> 🔁 Projeyi MongoDB’ye çevirdiğim için PostgreSQL bağlantı kısımları artık kullanılmayabilir.

---

## 🧑‍💻 Kurulum ve Çalıştırma

### 1. Repo`yu klonlayın

```bash
git clone https://github.com/kullanici-adiniz/Registration_Test.git
cd Registration_Test
