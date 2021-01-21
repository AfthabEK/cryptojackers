using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptojackerFinder
{
    public abstract class Checker
    {
        protected CancellationTokenSource _ts;
        private int _delay;

        protected Checker(int delay=1000)
        {
            this._delay = delay;
        }

        public virtual void Start()
        {
            _ts = new CancellationTokenSource();
            CancellationToken ct = _ts.Token;
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(_delay, ct);
                    //Thread.Sleep(1000);
                    Check();
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, ct);
        }

        public void Stop()
        {
            _ts.Cancel();
        }

        protected abstract void Check();
    }
}
