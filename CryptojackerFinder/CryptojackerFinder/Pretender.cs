namespace CryptojackerFinder
{
    public class Pretender: ViewModelBase
    {
        private string _processName;
        private float _cpuUsage;
        private int _processId;
        private float _ramUsage;
        private double _netUsage;
        private int _cryptoApiCalls;
        private float _cpuUsageSum;
        private float _cpuUsageSquare;
        private int _countCpuIndication;
        private double _cpuStandardDeviation;
        private bool _isCryptoJacker;


        public double CpuStandardDeviation
        {
            get => _cpuStandardDeviation;
            set => SetValue(ref _cpuStandardDeviation, value);
        }

        public int CountCpuIndication
        {
            get => _countCpuIndication;
            set => SetValue(ref _countCpuIndication, value);
        }

        public float CpuUsageSum
        {
            get => _cpuUsageSum;
            set => SetValue(ref _cpuUsageSum, value);
        }

        public float CpuUsageSquare
        {
            get => _cpuUsageSquare;
            set
            {
                SetValue(ref _cpuUsageSquare, value);
                OnPropertyChanged($"CpuStandardDeviation");
            }
        }

        public string ProcessName
        {
            get => _processName;
            set => SetValue(ref _processName, value);
        }

        public float CpuUsage
        {
            get => _cpuUsage;
            set
            {
                SetValue(ref _cpuUsage, value); 
                OnPropertyChanged($"CpuUsageView");
            }
        }

        public string CpuUsageView => $"{CpuUsage} %";

        public int ProcessId
        {
            get => _processId;
            set => SetValue(ref _processId, value);
        }

        public float RamUsage
        {
            get => _ramUsage;
            set
            {
                SetValue(ref _ramUsage, value);
                OnPropertyChanged($"RamUsageView");
            }
        }

        public string RamUsageView => $"{RamUsage} Mb";

        public double NetUsage
        {
            get => _netUsage;
            set
            {
                SetValue(ref _netUsage, value);
                OnPropertyChanged($"NetUsageView");
            }
        }

        public string NetUsageView => $"{NetUsage/1024/Minutes:N} KBytes/min";

        public int CryptoApiCalls
        {
            get => _cryptoApiCalls;
            set => SetValue(ref _cryptoApiCalls, value);
        }

        public bool IsCryptoJacker
        {
            get => _isCryptoJacker;
            set => SetValue(ref _isCryptoJacker, value);
        }

        public bool CheckCryptoJacker()
        {
            return Validator.IsCryptoJacker(CpuStandardDeviation, RamUsage, NetUsage, CryptoApiCalls);
        }

        public Validator Validator { get; set; }
        public double Minutes { get; set; }
    }
}
