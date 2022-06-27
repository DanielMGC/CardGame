namespace CardGameAPI.Entities
{
    public class Deck
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public Deck()
        {
            Reset();
        }

        public void Reset()
        {
            Cards = new List<Card>();
            foreach (CardSuit suit in (CardSuit[])Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in (CardValue[])Enum.GetValues(typeof(CardValue)))
                {
                    Cards.Add(new Card { Suit = suit, Value = value });
                }
            }
        }

    }
}
