using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamTradeCalculator.Utilities
{
    public static class SmartConverter
    {
        private static string s_digitSeparator 
            = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static string NumberDecimalSeparator => s_digitSeparator;

        public static float ToFloat(string text) 
        {
            string result = Regex.Replace(text, "[^0-9,.]", "");
            result = Regex.Replace(result, "[,.]", NumberDecimalSeparator);
            try
            {
                return float.Parse(result);
            }
            catch 
            {
                return 0;
            }
        }
    }
}
