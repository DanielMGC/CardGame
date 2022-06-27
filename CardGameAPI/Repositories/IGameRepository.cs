using CardGameAPI.Entities;

namespace CardGameAPI.Repositories
{
    public interface IGameRepository
    {
        Game GetGame(int id);
        Game CreateGame();
        void SaveGame(Game game);
        void DeleteGame(int id);
        Deck AddDeck(int gameId);
        Player AddPlayer(int gameId, string name);
        void DeletePlayer(int gameId, string name);
    }
}
