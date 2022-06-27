using CardGameAPI.Entities;
using System.Text.Json;
using System.IO;

namespace CardGameAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private IWebHostEnvironment _hostEnvironment;
        public GameRepository(IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;
        }
        public void SaveGame(Game game)
        {
            Directory.CreateDirectory(Path.Combine(_hostEnvironment.ContentRootPath, "gamedata"));
            File.WriteAllText(Path.Combine(_hostEnvironment.ContentRootPath, "gamedata", game.Id.ToString() + ".txt"), JsonSerializer.Serialize(game));
        }
        public Game GetGame(int id)
        {
            string path = Path.Combine(_hostEnvironment.ContentRootPath, "gamedata", id.ToString() + ".txt");
            if (File.Exists(path))
                return JsonSerializer.Deserialize<Game>(File.ReadAllText(path));
            return null;
        }
        public Game CreateGame()
        {
            Random random = new Random();
            Game game = new Game();
            while (GetGame(game.Id) != null)
                game.Id = random.Next();
            SaveGame(game);
            return game;
        }

        public void DeleteGame(int id)
        {
            string path = Path.Combine(_hostEnvironment.ContentRootPath, "gamedata", id.ToString() + ".txt");
            if (File.Exists(path))
                File.Delete(path);
        }

        public Deck AddDeck(int gameId)
        {
            Game game = GetGame(gameId);
            if (game == null)
                return null;

            Deck deck = new Deck();
            game.Cards.AddRange(deck.Cards);

            SaveGame(game);

            return deck;
        }

        public Player AddPlayer(int gameId, string name)
        {
            Game game = GetGame(gameId);
            if (game == null)
                return null;

            Player player = new Player { Name = name };
            game.Players.Add(player);

            SaveGame(game);

            return player;
        }

        public void DeletePlayer(int gameId, string name)
        {
            Game game = GetGame(gameId);
            if (game == null)
                return;

            Player player = game.Players.Where(p => p.Name == name).FirstOrDefault();

            if (player == null)
                return;

            game.Players.Remove(player);

            SaveGame(game);
        }
    }
}
