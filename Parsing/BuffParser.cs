using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using OpenQA.Selenium;
using SteamTradeCalculator.Utilities;

namespace SteamTradeCalculator.Parsing
{
    public class BuffParser
    {
        private ListBox _outputListBox;
        private IWebDriver _driver;
        private IJavaScriptExecutor _javaScriptExecutor;     

        private Cookie[] _buffCookie = new Cookie[]
        {
            new Cookie("session", "1-LnK2t505ZG7t4MUVoV15Nowl4Vqm-DeID407qgSctRQ82034912167"),
            new Cookie("csrf_token", "ImI2ODVlNjVmYzE3OWQzMmEwMTJlNGEzYTdjNWMzMDQ3YjQwZGIyOGQi.Feljrg.Nv4CF4y1douWeYP746SbvWYUzE0"),
        };

        public BuffParser(IWebDriver driver, ListBox outputListBox)
        {
            _outputListBox = outputListBox;
            _driver = driver;
            _javaScriptExecutor = (IJavaScriptExecutor)_driver;
            LoginInBuff();
        }

        public void NextPage()
        {
            foreach (var item in FindMarketItemsOnPage())
                _outputListBox.Items.Add(item);

            _javaScriptExecutor.ExecuteScript(
                "btn = document.querySelector('.next');" +
                "if(btn != null)" +
                "    btn.dispatchEvent(new MouseEvent('click'));");
        }

        private List<MarketItem> FindMarketItemsOnPage()
        {
            List<MarketItem> marketItems = new List<MarketItem>();
            IWebElement itemsBox = _driver.FindElement(By.ClassName("card_csgo"));
            foreach (var item in itemsBox.FindElements(By.TagName("li")))
            {
                string itemName = item.FindElement(By.XPath("h3/a")).GetAttribute("title");
                string itemPriceText = item.FindElement(By.XPath("p/strong")).Text;

                float itemPrice = SmartConverter.ToFloat(itemPriceText);

                marketItems.Add(new MarketItem(itemName, itemPrice));
            }

            return marketItems;
        }

        private void LoginInBuff()
        {
            _driver.Navigate().GoToUrl("https://buff.163.com/");
            InitializeCookie(_buffCookie);
            _driver.Navigate().GoToUrl("https://buff.163.com/market/csgo#tab=selling&page_num=1");
        }

        private void InitializeCookie(Cookie[] cookie)
        {
            var options = _driver.Manage();
            foreach (var item in cookie)
                options.Cookies.AddCookie(item);
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
