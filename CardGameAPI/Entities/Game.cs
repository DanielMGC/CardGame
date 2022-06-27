namespace CardGameAPI.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Player> Players { get; set; } = new List<Player>();

        public Game()
        {
            Random random = new Random();
            Id = random.Next();
        }

        public List<Card> TakeCards(int numberOfCards)
        {
            if (Cards.Count < numberOfCards)
                return null;

            List<Card> cards = Cards.Take(numberOfCards).ToList();
            Cards.RemoveRange(0, numberOfCards);
            return cards;
        }

        public void Shuffle()
        {
            var rand = new Random();
            var newDeck = new List<Card>();
            while (Cards.Count > 0)
            {
                int randPos = rand.Next(Cards.Count);
                newDeck.Add(Cards[randPos]);
                Cards.RemoveAt(randPos);
            }
            Cards.AddRange(newDeck);
        }
    }
}
