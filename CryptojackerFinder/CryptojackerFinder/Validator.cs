namespace CryptojackerFinder
{
    public class Validator
    {
        public bool IsCryptoJacker(double cpuStandardDeviation, double ramUsage, double netUsage, int cryptoApiCalls)
        {
            return !cryptojackerrrr(cpuStandardDeviation,ramUsage,netUsage,cryptoApiCalls);

        }

        public bool cryptojackerrrr(double cpuStandardDeviation, double ramUsage, double netUsage, int cryptoApiCalls)
        {
            return cpuStandardDeviation >= CpuStandardDeviationBorder &&
                ramUsage >= RamUsageBorder &&
                netUsage >= NetUsageBorder &&
                cryptoApiCalls >= CryptoApiCallsBorder;
        }


        public double CpuStandardDeviationBorder
        {
            get;
            set;
        }

        public float RamUsageBorder
        {
            get;
            set;
        }

        
        public float NetUsageBorder
        {
            get;
            set;
        }


        public int CryptoApiCallsBorder
        {
            get;
            set;
        }
    }
}
