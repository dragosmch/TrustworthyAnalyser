
namespace SecuritySafetyModule
{
    public static class SecuritySafetyDecision
    {
        public static SecuritySafetyResult GetSecuritySafetyDecision(string fileLocation, int mode)
        {
            var resultObject = SecuritySafetyRunner.GetWinCheckSecResultObject(fileLocation);
            var percentageResult = GetSecuritySafetyPercentage(resultObject, mode);
            // -1, 0 or 1
            int ternaryResult = GetSecuritySafetyResultFromPercentage(percentageResult, mode);
            return new SecuritySafetyResult
            {
                Safety = ternaryResult,
                SafetyAndSecurityPercentage = percentageResult,
                SafetyAndSecurityPercentageBase = GetSecuritySafetyMaxPercentage(mode),
                Security = ternaryResult,
                WinCheckSecResultObject = resultObject

            };
        }

        private static int GetSecuritySafetyResultFromPercentage(int percentage, int mode)
        {
            switch (mode)
            {
                case 1:
                    if (percentage > 70) return 1;
                    if (percentage >= 60) return 0;
                    return -1;
                case 2:
                    if (percentage > 90) return 1;
                    if (percentage >= 70) return 0;
                    return -1;
                default:
                    if (percentage > 50) return 1;
                    if (percentage >= 40) return 0;
                    return -1;               
            }
        }

        private static int GetSecuritySafetyMaxPercentage(int mode)
        {
            switch (mode)
            {
                case 1:
                    return 92;
                case 2:
                    return 100;
                default:
                    return 80;
            }
        }
        

        private static int GetSecuritySafetyPercentage(WinCheckSecResultObject resultObject, int mode)
        {
            if (resultObject == null) return -1;
            int percentage = 0;
            if (resultObject.Aslr) percentage += 20;
            if (resultObject.DynamicBase) percentage += 20;
            if (resultObject.Nx) percentage += 20;
            if (resultObject.Seh || resultObject.DotNet)
            {
                percentage += 20;
                resultObject.Seh = true;
            }
            if (mode == 0) return percentage;

            if (resultObject.Isolation) percentage += 5;
            if (resultObject.Gs || resultObject.DotNet)
            {
                percentage += 4;
                resultObject.Gs = true;
            }
            if (resultObject.Cfg || resultObject.DotNet)
            {
                percentage += 3;
                resultObject.Cfg = true;
            }
            if (mode == 1) return percentage;

            if (resultObject.HighEntropyVa) percentage += 2;
            if (resultObject.Authenticode) percentage += 2;
            if (resultObject.SafeSeh || resultObject.DotNet)
            {
                percentage += 2;
                resultObject.SafeSeh = true;
            }
            if (resultObject.Rfg || resultObject.DotNet)
            {
                percentage += 1;
                resultObject.Rfg = true;
            }
            if (resultObject.ForceIntegrity) percentage += 1;

            return percentage;
        }
    }
}
