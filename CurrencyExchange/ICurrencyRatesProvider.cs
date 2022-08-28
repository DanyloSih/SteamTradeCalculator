namespace SteamTradeCalculator.CurrencyExchange
{
    public interface ICurrencyRatesProvider
    {
        void UpdateRates();

        bool TryGetCurrencyRate(string currencyShortName, out double rate);
    }
}
