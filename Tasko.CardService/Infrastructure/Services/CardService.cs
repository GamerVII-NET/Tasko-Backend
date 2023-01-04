using Grpc.Core;
using Tasko.CardService.Infrastructure.Protos;
using Tasko.CardService.Infrastructure.Repositories;

namespace Tasko.CardService.Infrastructure.Services
{
    public class CardService : Card.CardBase
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public override Task<HelloWorldReply> HelloWorld(HelloWorldRequest request, ServerCallContext context)
        {
            return _cardRepository.HelloWorld(request);
        }
    }
}
