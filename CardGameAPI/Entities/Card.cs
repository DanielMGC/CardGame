namespace CardGameAPI.Entities
{
    public class Card
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }

        public override string ToString()
        {
            string valueStr = "";
            switch (Value)
            {
                case CardValue.Ace:
                    valueStr = "A";
                    break;
                case CardValue.Two:
                    valueStr = "2";
                    break;
                case CardValue.Three:
                    valueStr = "3";
                    break;
                case CardValue.Four:
                    valueStr = "4";
                    break;
                case CardValue.Five:
                    valueStr = "5";
                    break;
                case CardValue.Six:
                    valueStr = "6";
                    break;
                case CardValue.Seven:
                    valueStr = "7";
                    break;
                case CardValue.Eight:
                    valueStr = "8";
                    break;
                case CardValue.Nine:
                    valueStr = "9";
                    break;
                case CardValue.Ten:
                    valueStr = "10";
                    break;
                case CardValue.Jack:
                    valueStr = "J";
                    break;
                case CardValue.Queen:
                    valueStr = "Q";
                    break;
                case CardValue.King:
                    valueStr = "K";
                    break;
            }

            string suitStr = "";
            switch (Suit)
            {
                case CardSuit.Diamonds:
                    suitStr = "♥";
                    break;
                case CardSuit.Hearts:
                    suitStr = "♦";
                    break;
                case CardSuit.Clubs:
                    suitStr = "♣";
                    break;
                case CardSuit.Spades:
                    suitStr = "♠";
                    break;
            }

            return $"{valueStr}{suitStr}";
        }
    }
}
