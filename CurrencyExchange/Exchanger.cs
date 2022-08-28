using System;

namespace SteamTradeCalculator.CurrencyExchange
{
    public class Exchanger : IExchanger
    {
        private ICurrencyRatesProvider _currencyRatesProvider;

        public Exchanger(ICurrencyRatesProvider currencyRatesProvider)
        {
            _currencyRatesProvider = currencyRatesProvider;
        }

        public double Exchange(double value, string fromShortName, string toShortName)
        {
            double fromRate, toRate;
            if (!_currencyRatesProvider.TryGetCurrencyRate(fromShortName, out fromRate))
                throw new ArgumentException($"Invalid currency - {fromShortName}!");
            if (!_currencyRatesProvider.TryGetCurrencyRate(toShortName, out toRate))
                throw new ArgumentException($"Invalid currency - {toShortName}!");

            double currenciesFactor = fromRate / toRate;
            return value / currenciesFactor;
        }
    }
}
