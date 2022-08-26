using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SteamTradeCalculator.CustomElements
{
    public class FloatBox : TextBox
    {
        public static DependencyProperty FloatValueValueProperty = DependencyProperty.Register(
            nameof(FloatValue),
            typeof(float),
            typeof(FloatBox),
            new UIPropertyMetadata(FloatValueChangedHandler));

        private static void FloatValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = ((FloatBox)d);
            box.Text = e.NewValue.ToString();  
        }

        public float FloatValue
        { 
            get => (float)GetValue(FloatValueValueProperty);
            set => SetValue(FloatValueValueProperty, value);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            float value;
            if (ConvertToFloat(Text, out value) 
            && FloatValue != value)
            {
                FloatValue = value;
                CaretIndex = Text.Length;
            }
        }

        protected virtual bool ConvertToFloat(string text, out float result) 
        {
            string separators = ",. ";
            string convertedText = Regex.Replace(text, $"^[^0-9]+[{separators}]?[^0-9]+$", "");
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            convertedText = Regex.Replace(convertedText, $"[{separators}]", separator);

            bool isMatch = Regex.Match(convertedText, $"^[-+]?[0-9]+[{separators}]?[0-9]+$").Success;
            if (convertedText != "" 
            && isMatch)
            {
                result = float.Parse(convertedText);
                return true;
            }
            result = 0;
            return false;
        }
    }
}
