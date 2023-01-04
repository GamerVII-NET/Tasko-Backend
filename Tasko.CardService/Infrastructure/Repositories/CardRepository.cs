using Tasko.CardService.Infrastructure.Protos;

namespace Tasko.CardService.Infrastructure.Repositories
{
    public class CardRepository : CardRepositoryBase, ICardRepository
    {
        public async Task<HelloWorldReply> HelloWorld(HelloWorldRequest line)
        {
            return await Task.FromResult(new HelloWorldReply { Message = "Hi Anton"});
        }
    }
}
