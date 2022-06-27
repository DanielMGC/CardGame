using CardGameAPI.Entities;
using CardGameAPI.Repositories;
using System.Text.Json;

namespace CardGameAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public Result AddDeck(int gameId)
        {
            Game game =gameRepository.GetGame(gameId);
            if(game == null)
                return new Result { Success = false, Message = $"Game {gameId} does not exist" };

            gameRepository.AddDeck(gameId);

            return new Result { Success = true };
        }

        public Result AddPlayer(int gameId, string name)
        {
            Game game =gameRepository.GetGame(gameId);
            if(game == null)
                return new Result { Success = false, Message = $"Game {gameId} does not exist" };

            Player player = game.Players.Where(p => p.Name == name).SingleOrDefault();

            if (player != null)
                return new Result { Success = false, Message = $"Player {name} already exists" };

            gameRepository.AddPlayer(gameId, name);

            return new Result { Success = true };
        }

        public (Result result, Game game) CreateGame()
        {
            Game game = gameRepository.CreateGame();

            return (new Result {  Success = true }, game);
        }

        public (Result result, Game game) GetGame(int gameId)
        {
            Game game = gameRepository.GetGame(gameId);
            if (game == null)
                return (new Result { Success = false, Message = $"Game {gameId} does not exist" }, null);

            return (new Result { Success = true }, game);
        }

        public Result DealCards(int gameId, string playerName, int numberOfCards)
        {
            Game game = gameRepository.GetGame(gameId);
            if (game == null)
                return new Result { Success = false, Message = $"Game {gameId} does not exist" };

            Player player = game.Players.Where(p => p.Name == playerName).FirstOrDefault();
            if(player == null)
                return new Result { Success = false, Message = $"Player {playerName} does not exist" };


            if(game.Cards.Count < numberOfCards)
                return new Result { Success = false, Message = "Not enough cards in the deck(s)" };

            player.Cards.AddRange(game.TakeCards(numberOfCards));

            gameRepository.SaveGame(game);

            return new Result { Success = true };
        }

        public Result DeleteGame(int gameId)
        {
            Game game =gameRepository.GetGame(gameId);
            if(game == null)
                return new Result { Success = false, Message = $"Game {gameId} does not exist" };

            gameRepository.DeleteGame(gameId);

            return new Result { Success = true };
        }

        public (Result result, List<Card> cards) GetPlayerCards(int gameId, string playerName)
        {
            Game game = gameRepository.GetGame(gameId);
            if (game == null)
                return (new Result { Success = false, Message = $"Game {gameId} does not exist" }, null);

            Player player = game.Players.Where(p => p.Name == playerName).FirstOrDefault();
            if (player == null)
                return (new Result { Success = false, Message = $"Player {playerName} does not exist" }, null);

            return (new Result { Success = true }, player.Cards);
        }

        public (Result result, List<Player> players) GetPlayers(int gameId)
        {
            Game game =gameRepository.GetGame(gameId);
            if(game == null)
                return (new Result { Success = false, Message = $"Game {gameId} does not exist" }, null);

            return (new Result { Success = true }, game.Players.OrderByDescending(p => p.Total).ToList());
        }

        public (Result result, string totals) GetUndealtCards(int gameId)
        {
            Game game = gameRepository.GetGame(gameId);
            if (game == null)
                return (new Result { Success = false, Message = $"Game {gameId} does not exist" }, null);

            CardValue[] cardValues = (CardValue[])Enum.GetValues(typeof(CardValue));
            Dictionary<string, int> totals = new Dictionary<string, int>();

            foreach (CardSuit suit in (CardSuit[])Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in cardValues.Reverse())
                {
                    Card card = new Card { Suit = suit, Value = value };
                    totals.Add(card.ToString(), 0);
                }
            }

            foreach (Card card in game.Cards)
            {  
                totals[card.ToString()]++;
            }

            string totalsStr = "";
            foreach (string cardStr in totals.Keys)
            {
                if (totalsStr != "")
                    totalsStr += ", ";

                totalsStr += $"{cardStr} x {totals[cardStr]}";
            }

            return (new Result { Success = true }, totalsStr);
        }

        public (Result result, string totals) GetUndealtCardsBySuit(int gameId)
        {
            Game game = gameRepository.GetGame(gameId);
            if (game == null)
                return (new Result { Success = false, Message = $"Game {gameId} does not exist" }, null);

            Dictionary<CardSuit, int> totals = new Dictionary<CardSuit, int>();
            foreach (CardSuit suit in (CardSuit[])Enum.GetValues(typeof(CardSuit)))
                totals.Add(suit, 0);

            foreach (Card card in game.Cards)
                totals[card.Suit]++;

            string totalsStr = "";
            foreach(CardSuit suit in totals.Keys)
            {
                if (totalsStr != "")
                    totalsStr += ", ";

                totalsStr += totals[suit].ToString() + " x ";
                switch(suit)
                {
                    case CardSuit.Diamonds:
                        totalsStr += "♥";
                        break;
                    case CardSuit.Hearts:
                        totalsStr += "♦";
                        break;
                    case CardSuit.Clubs:
                        totalsStr += "♣";
                        break;
                    case CardSuit.Spades:
                        totalsStr += "♠";
                        break;
                }
            }

            return (new Result { Success = true }, totalsStr);
        }

        public Result RemovePlayer(int gameId, string name)
        {
            Game game =gameRepository.GetGame(gameId);
            if(game == null)
                return new Result { Success = false, Message = $"Game {gameId} does not exist" };

            Player player = game.Players.Where(p => p.Name == name).SingleOrDefault();

            if (player == null)
                return new Result { Success = false, Message = $"Player {name} does not exist" };

            gameRepository.DeletePlayer(gameId, name);

            return new Result { Success = true };
        }

        public Result Shuffle(int gameId)
        {
            Game game = gameRepository.GetGame(gameId);
            if (game == null)
                return new Result { Success = false, Message = $"Game {gameId} does not exist" };

            game.Shuffle();
            gameRepository.SaveGame(game);

            return new Result { Success = true };
        }
    }
}
