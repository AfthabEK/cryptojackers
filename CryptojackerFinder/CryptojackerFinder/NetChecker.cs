using System.Diagnostics;

namespace CryptojackerFinder
{
    public class NetChecker : Checker
    {
        public NetChecker()
        {
        }

        private readonly PerformanceCounter _performanceCounter;
        private float _ramUsage;

        public NetChecker(int processId)
        {
            _performanceCounter = ProcessCpuCounter.GetPerfCounterForProcessId(processId, "IO Data Bytes/sec");
        }

        public float GetNetUsage()
        {
            return _ramUsage;
        }

        protected override void Check()
        {
            var netUsage = _performanceCounter.NextValue();
            if (netUsage != 0)
                _ramUsage = netUsage;
        }
    }
}


