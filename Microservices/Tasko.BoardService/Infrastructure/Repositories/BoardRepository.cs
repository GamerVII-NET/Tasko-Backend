using Tasko.Domains.Models.Structural;

namespace Tasko.BoardSevice.Infrasructure.Repositories;

public class BoardRepository : BoardRepositoryBase, IBoardRepository
{
    public BoardRepository(IMongoDatabase databaseContext) : base(databaseContext) { }

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

    public async Task<IBoard> FindBoardAsync(Guid id)
    {
        var filter = Builders<Board>.Filter.Eq(c => c.Id, id);
        return await BoardCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<IBoard>> GetBoardsAsync() => await BoardCollection.Find(_ => true).ToListAsync();

    public Task<IBoard> UpdateBoardAsync(Board board)
    {
        throw new NotImplementedException();
    }
}
