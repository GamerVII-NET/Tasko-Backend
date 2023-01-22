using Tasko.Domains.Models.Structural;

namespace Tasko.BoardSevice.Infrasructure.Repositories;
public interface IBoardRepository
{
    Task<IBoard> FindBoardAsync(Guid id);

    Task<IEnumerable<IBoard>> GetBoardsAsync();

    Task<IBoard> CreateBoardAsync(Board board);

    Task<IBoard> UpdateBoardAsync(Board board);

    Task DeleteBoardAsync(Board board);

}
