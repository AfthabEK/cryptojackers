using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Documents;

namespace CryptojackerFinder
{
    public class CpuChecker: Checker
    {
        private readonly PerformanceCounter _performanceCounter;
        private float _cpuUsage;
        
        public CpuChecker(){}
        public CpuChecker(int processId)
        {
            _process = Process.GetProcessById(processId);
            _current = _process.TotalProcessorTime;
        }

        private List<float> _accumulations = new List<float>();
        private readonly Process _process;
        private TimeSpan _current;

        protected override void Check()
        {
            var next = _process.TotalProcessorTime;
            _cpuUsage =  (float) ((next - _current).TotalSeconds / Environment.ProcessorCount * 100);
            _current = next;
        }

        public float GetCpuUsage()
        {
            return _cpuUsage;
        }
    }
}
