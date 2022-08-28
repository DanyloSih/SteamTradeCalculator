namespace SteamTradeCalculator.CurrencyExchange
{
    public struct Currency
    {
        private string _shortName;
        private string _sign;

        public static Currency USD => new Currency("USD", "$");
        public static Currency UAH => new Currency("UAH", "₴");
        public static Currency CNY => new Currency("CNY", "¥");

        public string ShortName { get => _shortName; }
        public string Sign { get => _sign; }

        public Currency(string shortName, string sign)
        {
            _shortName = shortName;
            _sign = sign;
        }
    }
}
