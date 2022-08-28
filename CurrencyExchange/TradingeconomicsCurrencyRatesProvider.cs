using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using SteamTradeCalculator.Utilities;

namespace SteamTradeCalculator.CurrencyExchange
{
    public class TradingeconomicsCurrencyRatesProvider : ICurrencyRatesProvider
    {
        private Dictionary<string, double> _currencyRatePairs 
            = new Dictionary<string, double>();

        public bool TryGetCurrencyRate(string currencyShortName, out double rate)
        {
            return _currencyRatePairs.TryGetValue(currencyShortName, out rate);

        }

        public void UpdateRates()
        {
            string url = "https://tradingeconomics.com/currencies?base=usd";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var currenciesNodes = doc.DocumentNode.SelectNodes(
                "//tr[@class=\'datatable-row\'] " +
                "| //tr[@class=\'datatable-row-alternating\']");


            foreach (var item in currenciesNodes)
            {
                var nameNode = item.SelectSingleNode("td/a/b");
                string name = nameNode.InnerText;
                if (Regex.IsMatch(name, "^USD.*"))
                { 
                    string currencyShortName = Regex.Replace(name, "USD", "");
                    if (!_currencyRatePairs.ContainsKey(currencyShortName))
                    {
                        var rateNode = item.SelectSingleNode("td[@id=\'p\']");
                        float currencyRate = SmartConverter.ToFloat(rateNode.InnerText);
                        _currencyRatePairs.Add(currencyShortName, currencyRate);
                    }
                }
            }
        }
    }
}
