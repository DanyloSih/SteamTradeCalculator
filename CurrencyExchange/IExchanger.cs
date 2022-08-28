namespace SteamTradeCalculator.CurrencyExchange
{
    public interface IExchanger
    {
        double Exchange(double value, string fromShortName, string toShortName);
    }
}