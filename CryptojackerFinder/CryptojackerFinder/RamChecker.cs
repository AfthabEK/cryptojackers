using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CryptojackerFinder
{
    public class RamChecker: Checker
    {
        private readonly PerformanceCounter _performanceCounter;
        private float _ramUsage;

        public RamChecker() { }
        public RamChecker(int processId)
        {
            _performanceCounter = ProcessCpuCounter.GetPerfCounterForProcessId(processId, "Working Set");
        }

        public float GetRamUsage()
        {
            return _ramUsage;
        }

        protected override void Check()
        {
            _ramUsage = _performanceCounter.NextValue() / 1024 / 1024;
        }
        public List<string> GetProcesses(string processCounterName = "Working Set")
        {
            var allProcesses = Process.GetProcesses();

            var preCounters = allProcesses.Select(x => ProcessCpuCounter.GetPerfCounterForProcessId(x.Id, processCounterName)).ToList();

            // foreach (var performanceCounter in preCounters) performanceCounter.NextValue();
            Thread.Sleep(1000);
            return preCounters.Select(x => $"{x.InstanceName}: {x.NextValue() / 1024 / 1024}").ToList();
        }
    }
}
