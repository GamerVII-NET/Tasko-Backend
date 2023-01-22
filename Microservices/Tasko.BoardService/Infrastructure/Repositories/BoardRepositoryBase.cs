using Tasko.Domains.Models.Structural;

namespace Tasko.BoardSevice.Infrasructure.Repositories;
public class BoardRepositoryBase
{
    public BoardRepositoryBase(IMongoDatabase databaseContext)
    {
        BoardFilter = Builders<Board>.Filter;
        BoardCollection = databaseContext.GetCollection<Board>("Boards");
        UsersCollection = databaseContext.GetCollection<User>("Users");
    }

    internal IMongoCollection<Board> BoardCollection { get; set; }
    internal IMongoCollection<User> UsersCollection { get; set; }

    internal FilterDefinitionBuilder<Board> BoardFilter { get; }
}
