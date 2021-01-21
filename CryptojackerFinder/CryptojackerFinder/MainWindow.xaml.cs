using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CryptojackerFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<CancellationTokenSource> _cancellationTokenSources = new List<CancellationTokenSource>();

        private async void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Hidden;
            StopButton.Visibility = Visibility.Visible;
            StopButton.Content = "Анализ";
            StopButton.IsEnabled = false;
            IsCanSetting = false;

            var analyzer = new CryptojackerAnalyzer(CpuUsage);

            Pretenders = new ObservableCollection<Pretender>(await analyzer.FindPretenders());

            if (Pretenders.Count == 0)
            {
                MessageBox.Show("Не найдены процессы потребляющие CPU больше заданного");
                StartButton.Visibility = Visibility.Visible;
                StopButton.Visibility = Visibility.Hidden;
                IsCanSetting = true;
                return;
            }

            Pretenders.ToList().ForEach(x=>x.Validator = new Validator()
            {
                CpuStandardDeviationBorder = CpuStandardDeviation,
                RamUsageBorder = RamUsage,
                NetUsageBorder = NetUsage,
                CryptoApiCallsBorder = CryptoApiCalls,
            });
            var analyzeTime = AnalyzeTime;
            analyzer.Begin = DateTime.Now;
            _timer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromSeconds(1),
                IsEnabled = true
            };
            _timer.Tick += (s, ee) =>
            {
                CurrentTime = DateTime.Now - analyzer.Begin;
            };
            StopButton.IsEnabled = true;
            StopButton.Content = "Стоп";
            var tasks = Pretenders.Select(async p =>
            {
                var ts = new CancellationTokenSource();
                CancellationToken ct = ts.Token;
                _cancellationTokenSources.Add(ts);

                await analyzer.StartAnalyze(p, analyzeTime, ct);
            }).ToArray();



            await Task.WhenAll(tasks).ContinueWith(task =>
            {
                Dispatcher.Invoke(() =>
                {
                    StartButton.Visibility = Visibility.Visible;
                    StopButton.Visibility = Visibility.Hidden;
                    IsCanSetting = true;
                    _timer.Stop();
                });
            });
        }

        public TimeSpan AnalyzeTime
        {
            get => (TimeSpan)GetValue(AnalyzeTimeProperty);
            set => SetValue(AnalyzeTimeProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnalyzeTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnalyzeTimeProperty =
            DependencyProperty.Register("AnalyzeTime", typeof(TimeSpan), typeof(MainWindow), new PropertyMetadata(
                    TimeSpan.FromMinutes(10d)));

        public ObservableCollection<Pretender> Pretenders
        {
            get => (ObservableCollection<Pretender>)GetValue(PretendersProperty);
            set => SetValue(PretendersProperty, value);
        }

        public TimeSpan CurrentTime     
        {
            get => (TimeSpan) GetValue(CurrentTimeProperty);
            set => SetValue(CurrentTimeProperty, value);
        }

        public bool IsCanSetting
        {
            get { return (bool) GetValue(IsCanSettingProperty); }
            set { SetValue(IsCanSettingProperty, value); }
        }

        public float CpuUsage
        {
            get { return (float) GetValue(CpuUsageProperty); }
            set { SetValue(CpuUsageProperty, value); }
        }

        public double CpuStandardDeviation
        {
            get { return (double) GetValue(CpuStandardDeviationProperty); }
            set { SetValue(CpuStandardDeviationProperty, value); }
        }

        public float RamUsage
        {
            get { return (float) GetValue(RamUsageProperty); }
            set { SetValue(RamUsageProperty, value); }
        }

        public float NetUsage
        {
            get { return (float) GetValue(NetUsageProperty); }
            set { SetValue(NetUsageProperty, value); }
        }

        public int CryptoApiCalls
        {
            get { return (int) GetValue(CryptoApiCallsProperty); }
            set { SetValue(CryptoApiCallsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pretenders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PretendersProperty =
            DependencyProperty.Register("Pretenders", typeof(ObservableCollection<Pretender>), 
                typeof(MainWindow), new PropertyMetadata(new ObservableCollection<Pretender>(),
                    (o, args) => (args.NewValue as List<Pretender>)?
                        .ForEach(x=> x.PropertyChanged += (sender, eventArgs) => {}
                        )));

        public static readonly DependencyProperty CurrentTimeProperty = DependencyProperty.Register("CurrentTime", typeof(TimeSpan), typeof(MainWindow), new PropertyMetadata(default(TimeSpan)));
        public static readonly DependencyProperty IsCanSettingProperty = DependencyProperty.Register("IsCanSetting", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));
        private DispatcherTimer _timer;
        public static readonly DependencyProperty CpuUsageProperty = DependencyProperty.Register("CpuUsage", typeof(float), typeof(MainWindow), new PropertyMetadata(10f));
        public static readonly DependencyProperty CpuStandardDeviationProperty = DependencyProperty.Register("CpuStandardDeviation", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty RamUsageProperty = DependencyProperty.Register("RamUsage", typeof(float), typeof(MainWindow), new PropertyMetadata(default(float)));
        public static readonly DependencyProperty NetUsageProperty = DependencyProperty.Register("NetUsage", typeof(float), typeof(MainWindow), new PropertyMetadata(default(float)));
        public static readonly DependencyProperty CryptoApiCallsProperty = DependencyProperty.Register("CryptoApiCalls", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        private void ButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Hidden;
            IsCanSetting = true;
            _timer.Stop();

            _cancellationTokenSources.ForEach(x=> x.Cancel());
            _cancellationTokenSources = new List<CancellationTokenSource>();
        }


    }
}
