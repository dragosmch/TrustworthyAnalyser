using System;

namespace SecuritySafetyModule
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
        public string Path { get; set; }

        public string ToString(int mode)
        {
            var stringRepresentation = $"Safe compilation settings: {Environment.NewLine}" 
                                        + $"DynamicBase: {DynamicBase}{Environment.NewLine}"
                                        + $"Aslr: {Aslr}{ Environment.NewLine}"
                                        + $"Nx: {Nx}{ Environment.NewLine}"
                                        + $"Seh: {Seh}";
            if (mode > 0)
                stringRepresentation += $@"{Environment.NewLine}Isolation: {Isolation}{Environment.NewLine}" 
                        + $"Gs: {Gs}{Environment.NewLine}"
                        + $"Cfg: {Cfg}";
            if (mode == 2)
                stringRepresentation += $@"{Environment.NewLine}HighEntropyVa: {HighEntropyVa}{Environment.NewLine}"
                    + $"ForceIntegrity: {ForceIntegrity}{Environment.NewLine}"
                    + $"Rfg: {Rfg}{Environment.NewLine}"
                    + $"SafeSeh: {SafeSeh}{Environment.NewLine}"
                    + $"Authenticode: {Authenticode}";
            return stringRepresentation + Environment.NewLine;
        }
    }
}
