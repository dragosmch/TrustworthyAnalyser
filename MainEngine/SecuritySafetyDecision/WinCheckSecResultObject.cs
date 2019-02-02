using Newtonsoft.Json;

namespace SecuritySafetyDecision
{
    public class WinCheckSecResultObject
    {
        public bool DynamicBase { get; set; }

        public bool Aslr { get; set; }


        public bool HighEntropyVa { get; set; }
        public bool ForceIntegrity { get; set; }
        public bool Isolation { get; set; }
        public bool Nx { get; set; }
        public bool Seh { get; set; }
        public bool Cfg { get; set; }
        public bool Rfg { get; set; }
        public bool SafeSeh { get; set; }
        public bool Gs { get; set; }
        public bool Authenticode { get; set; }
        public bool DotNet { get; set; }
    }
}
