using MongoDB.Driver;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.BoardSevice.Infrasructure.Repositories;

#region Interfaces
public interface IBoardRepository
{
    Task<IBoard> FindBoardAsync(Guid id);

    Task<IEnumerable<IBoard>> GetBoardsAsync();

    Task<IBoard> CreateBoardAsync(Board board);

    Task<IBoard> UpdateBoardAsync(Board board);

    Task DeleteBoardAsync(Board board);

}
#endregion

#region DataBase context
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

#endregion

#region Repository
public class BoardRepository : BoardRepositoryBase, IBoardRepository
{
    public BoardRepository(IMongoDatabase databaseContext) : base(databaseContext)
    {
    }

    public async Task<IBoard> CreateBoardAsync(Board board)
    {
        board.CreatedAt = DateTime.Now;

        await BoardCollection.InsertOneAsync(board);

        return board;
    }

    public Task DeleteBoardAsync(Board board)
    {
        throw new NotImplementedException();
    }

    public async Task<IBoard> FindBoardAsync(Guid id) =>
           await BoardCollection.Find(c => c.Id == id).SingleOrDefaultAsync();

    public async Task<IEnumerable<IBoard>> GetBoardsAsync() => await BoardCollection.Find(_ => true).ToListAsync();

    public Task<IBoard> UpdateBoardAsync(Board board)
    {
        throw new NotImplementedException();
    }
}

#endregion