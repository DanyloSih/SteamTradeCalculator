using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using SteamTradeCalculator.Parsing;

namespace SteamTradeCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private float _priceValue = 0f;

        public float PriceValue
        {
            get => _priceValue; 
            set
            {
                if (_priceValue == value)
                    return;

                _priceValue = value;
                OnPropertyChanged();
            }
        }

        private FirefoxDriver _headlessFirefoxDriver;
        private BuffParser _buffMarketParser;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            PriceValue = 4.535f;

            InitializeDriver();
            _buffMarketParser = new BuffParser(_headlessFirefoxDriver, BuffOutputListBox);
        }

        private void InitializeDriver()
        {
            FirefoxDriverService firefoxDriverService = FirefoxDriverService.CreateDefaultService();
            firefoxDriverService.HideCommandPromptWindow = true;
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArguments("-headless");
            firefoxOptions.AddArguments("window-size=1920,1080");
            _headlessFirefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxOptions);
            Application.Current.Exit += OnApplicationExit;
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            _headlessFirefoxDriver.Quit();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnStartFirefoxButtonClick(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void OnNextButtonClicked(object sender, RoutedEventArgs e)
        {
            _buffMarketParser.NextPage();
        }
    }
}
