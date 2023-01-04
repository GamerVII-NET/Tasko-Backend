namespace Tasko.CardService.Infrastructure.Repositories
{
    public class CardRepository : CardRepositoryBase, ICardRepository
    {
        public async Task<string> HelloWorld(string line)
        {
            return await Task.FromResult($"Hello World! Your line: {line}");
        }
    }
}
