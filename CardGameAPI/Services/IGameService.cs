using CardGameAPI.Entities;

namespace CardGameAPI.Services
{
    public interface IGameService
    {
        (Result result, Game game) CreateGame();
        (Result result, Game game) GetGame(int gameId);
        Result DeleteGame(int gameId);
        Result AddPlayer(int gameId, string name);
        Result RemovePlayer(int gameId, string name);
        (Result result, List<Player> players) GetPlayers(int gameId);
        Result AddDeck(int gameId);
        Result DealCards(int gameId, string playerName, int numberOfCards);
        (Result result, List<Card> cards) GetPlayerCards(int gameId, string playerName);
        (Result result, string totals) GetUndealtCardsBySuit(int gameId);
        (Result result, string totals) GetUndealtCards(int gameId);
        Result Shuffle(int gameId);

    }
}
