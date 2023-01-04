using Tasko.CardService.Infrastructure.Protos;

namespace Tasko.CardService.Infrastructure.Repositories
{
    public interface ICardRepository
    {
        Task<HelloWorldReply> HelloWorld(HelloWorldRequest line);
    }
}
