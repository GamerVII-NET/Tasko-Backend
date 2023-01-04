using Tasko.CardService.Infrastructure.Repositories;

namespace Tasko.CardService.Infrastructure.Services
{
    public class CardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Task<string> HelloWorld(string line)
        {
            return _cardRepository.HelloWorld(line);
        }
    }
}
