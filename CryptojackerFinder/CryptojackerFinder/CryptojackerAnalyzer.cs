using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptojackerFinder
{
    public class CryptojackerAnalyzer
    {
        public CryptojackerAnalyzer(float usageBorder)
        {
            _usageBorder = usageBorder;
            
        }
        public async Task StartAnalyze(Pretender pretender, TimeSpan analyzeTime, CancellationToken ct)
        {

            var cpuChecker = new CpuChecker(pretender.ProcessId);
            cpuChecker.Start();
            var ramChecker = new RamChecker(pretender.ProcessId);
            ramChecker.Start();
            var netChecker = new NetChecker(pretender.ProcessId);
            netChecker.Start();
            var cryptoApiChecker = new CryptoApiChecker(pretender.ProcessId);
            cryptoApiChecker.Start();

            while (DateTime.Now.Subtract(Begin) < analyzeTime)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }
                try
                {
                    await Task.Delay(1000, ct);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                var cpuUsage = cpuChecker.GetCpuUsage();
                pretender.CpuUsage = cpuUsage;
                pretender.CpuUsageSum += cpuUsage;
                pretender.CpuUsageSquare += cpuUsage * cpuUsage;
                if(cpuUsage != 0)
                    ++pretender.CountCpuIndication;
                pretender.CpuStandardDeviation = Math.Sqrt(pretender.CpuUsageSquare / 
                    pretender.CountCpuIndication - pretender.CpuUsageSum * pretender.CpuUsageSum/ (pretender.CountCpuIndication* pretender.CountCpuIndication));

                pretender.RamUsage = ramChecker.GetRamUsage();
                pretender.NetUsage += netChecker.GetNetUsage();
                pretender.Minutes = DateTime.Now.Subtract(Begin).TotalMinutes;
                pretender.CryptoApiCalls = cryptoApiChecker.GetNetUsage() / (DateTime.Now.Subtract(Begin).Minutes + 1);
                pretender.IsCryptoJacker = pretender.CheckCryptoJacker();

            }
            cpuChecker.Stop();
            ramChecker.Stop();
            netChecker.Stop();
            cryptoApiChecker.Stop();
        }

        public DateTime Begin { get; set; } = DateTime.Now;


        public async Task<List<Pretender>> FindPretenders()
        {
            
            var procDict = new Dictionary<(string InstanceName, int Id), float>();
            var processes = Process.GetProcesses().ToList().Select(p =>
            {
                try
                {
                    return (p, p.TotalProcessorTime);
                }
                catch (Exception)
                {
                    return (p, default);
                }
            }
            ).ToList();

            await Task.Delay(1000);
            var res = processes.Select(x =>
                {
                    try
                    {
                        return (x.p, (x.p.TotalProcessorTime - x.TotalProcessorTime).TotalSeconds /
                                     Environment.ProcessorCount * 100);
                    }
                    catch (Exception e)
                    {
                        return (x.p, 0);
                    }

                }
            ).OrderByDescending(x => x.Item2).ToList();
            return res.Where(x =>
                x.p.ProcessName.Trim().ToLower() != "idle" &&
                x.Item2 > _usageBorder).Select(x => new Pretender()
            {
                ProcessName = x.Item1.ProcessName, ProcessId = x.Item1.Id

            }).ToList();
        }

        private readonly float _usageBorder;
    }
}
