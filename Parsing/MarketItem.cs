namespace SteamTradeCalculator.Parsing
{
    public class MarketItem
    {
        //private Currency _currency;
        private string _name;
        private float _price;

        //public Currency Currency { get => _currency; }
        public string Name { get => _name; }
        public float Price { get => _price; }

        public MarketItem(string name, float price)
        {
            _name = name;
            _price = price;
            //_currency = currency;
        }

        public override string ToString()
        {
            return ($"{_price} - {_name}");
        }
    }
}
