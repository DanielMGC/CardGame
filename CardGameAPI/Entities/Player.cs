namespace CardGameAPI.Entities
{
    public class Player
    {
        public string Name { get; set; } 
        public List<Card> Cards { get; set; } = new List<Card>();
        public int Total
        {
            get
            {
                return Cards.Sum(c => (int)c.Value);
            }
        }
    }
}
