using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace CryptojackerFinder
{
    class CryptoApiChecker
    {
        private static CancellationTokenSource _ts;
        private static uint _checkersCount;
        private static Dictionary<int, int> _cryptoApiCalls = new Dictionary<int, int>();
        private int _processId;

        public CryptoApiChecker(int processId)
        {
            _processId = processId;
        }
        public void Start()
        {
            if (_checkersCount++ == 0)
            {
                _ts = new CancellationTokenSource();
                CancellationToken ct = _ts.Token;
                Task.Run(() =>
                {
                    while (true)
                    {
                        var namedPipeServer = new NamedPipeServerStream("my-very-cool-pipe-example", PipeDirection.InOut, 1,
                            PipeTransmissionMode.Byte);
                        var streamReader = new StreamReader(namedPipeServer);

                        if (ct.IsCancellationRequested)
                        {
                            streamReader.Dispose();
                            namedPipeServer.Dispose();
                            break;
                        }

                        try
                        {
                            var task = namedPipeServer.WaitForConnectionAsync(ct);
                            task.Wait(ct);
                        }
                        catch (Exception)
                        {
                            streamReader.Dispose();
                            namedPipeServer.Dispose();
                            break;
                        }
                        

                        while (!streamReader.EndOfStream)
                        {
                            if (int.TryParse(streamReader.ReadLine(), out int pid))
                            {
                                if (_cryptoApiCalls.ContainsKey(pid))
                                    ++_cryptoApiCalls[pid];
                                else
                                    _cryptoApiCalls.Add(pid, 1);
                            }

                        }
                        streamReader.Dispose();
                        namedPipeServer.Dispose();
                    }
                }, ct);
            }
        }

        public void Stop()
        {
            if (_checkersCount > 0 && --_checkersCount == 0)
            {
                _ts.Cancel();
                _cryptoApiCalls = new Dictionary<int, int>();
            }
        }

        public int GetNetUsage()
        {
            return _cryptoApiCalls.ContainsKey(_processId)? _cryptoApiCalls[_processId] : 0;
        }

    }
}
