namespace Tasko.CardService.Infrastructure.Repositories
{
    public interface ICardRepository
    {
        Task<string> HelloWorld(string line);
    }
}
