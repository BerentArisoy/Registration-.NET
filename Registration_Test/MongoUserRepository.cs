using MongoDB.Bson;
using MongoDB.Driver;
using Registration_Test.Entities;
using Registration_Test.DataAccess;

namespace Registration_Test;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;

    public MongoUserRepository()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("registration_db");
        _userCollection = database.GetCollection<User>("users");
    }

    public void Add(User user)
    {
        user.Id = GetNextSequence("users");
        _userCollection.InsertOne(user);
    }

    public bool Delete(int id)
    {
        var result = _userCollection.DeleteOne(u => u.Id == id);
        return result.DeletedCount > 0;
    }

    public List<User> GetAll()
    {
        return _userCollection.Find(_ => true).ToList();
    }

    public User GetById(int id)
    {
        return _userCollection.Find(u => u.Id == id).FirstOrDefault();
    }

    public bool Update(User updatedUser)
    {
        var result = _userCollection.ReplaceOne(u => u.Id == updatedUser.Id, updatedUser);
        return result.ModifiedCount > 0;
    }
    
    private int GetNextSequence(string name)
    {
        var counterCollection = _userCollection.Database.GetCollection<Counter>("counters");

        var filter = Builders<Counter>.Filter.Eq(c => c.Id, name);
        var update = Builders<Counter>.Update.Inc(c => c.Sequence, 1);
        var options = new FindOneAndUpdateOptions<Counter>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = true
        };

        var counter = counterCollection.FindOneAndUpdate(filter, update, options);
        return counter.Sequence;
    }


    public bool EmailExists(string email)
    {
        return _userCollection.Find(u => u.Email == email).Any();
    }
    
    public User GetByEmail(string email)
    {
        return _userCollection.Find(u => u.Email == email).FirstOrDefault();
    }
    
    public List<User> SearchByName(string keyword)
    {
        var filter = Builders<User>.Filter.Regex(u => u.FullName, new BsonRegularExpression(keyword, "i"));
        return _userCollection.Find(filter).ToList();
    }
    
    public List<User> GetUsersPaged(int page, int pageSize)
    {
        return _userCollection.Find(FilterDefinition<User>.Empty)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToList();
    }

    
    public List<User> GetUsersByNameAndEmail(string namePart, string emailDomain)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Regex(u => u.FullName, new BsonRegularExpression(namePart, "i")),
            Builders<User>.Filter.Regex(u => u.Email, new BsonRegularExpression(emailDomain + "$", "i"))
        );

        return _userCollection.Find(filter).ToList();
    }

    public List<User> SearchUsers(string keyword)
    {
        var filter = Builders<User>.Filter.Or(
            Builders<User>.Filter.Regex(u => u.FullName, new BsonRegularExpression(keyword, "i")),
            Builders<User>.Filter.Regex(u => u.Email, new BsonRegularExpression(keyword, "i"))
        );

        return _userCollection.Find(filter).ToList();
    }

    public void SaveChanges() { /* MongoDB'de gerek yok */ }
}