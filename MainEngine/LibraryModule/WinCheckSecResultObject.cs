using System.Text;

namespace LibraryModule
{
    /// <summary>
    /// Object encapsulating security compilation properties,
    /// obtained from WinCheckSec tool.
    /// </summary>
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

        /// <summary>
        /// String format used for the results report
        /// </summary>
        /// <param name="mode">Basic, Medium or Advanced</param>
        /// <returns>The string representation of the results of WinCheckSec</returns>
        public string ToString(AnalysisMode mode)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Safe compilation settings:");
            stringBuilder.AppendLine($"DynamicBase: {DynamicBase}");
            stringBuilder.AppendLine($"Aslr: {Aslr}");
            stringBuilder.AppendLine($"Nx: {Nx}");
            stringBuilder.AppendLine($"Seh: {Seh}");

            if (mode != AnalysisMode.Basic)
            {
                stringBuilder.AppendLine($"Isolation: {Isolation}");
                stringBuilder.AppendLine($"Gs: {Gs}");
                stringBuilder.AppendLine($"Cfg: {Cfg}");
            }

            if (mode == AnalysisMode.Advanced)
            {
                stringBuilder.AppendLine($"HighEntropyVa: {HighEntropyVa}");
                stringBuilder.AppendLine($"ForceIntegrity: {ForceIntegrity}");
                stringBuilder.AppendLine($"Rfg: {Rfg}");
                stringBuilder.AppendLine($"SafeSeh: {SafeSeh}");
                stringBuilder.AppendLine($"Authenticode: {Authenticode}");
            }

            return stringBuilder.ToString();
        }
    }
}
