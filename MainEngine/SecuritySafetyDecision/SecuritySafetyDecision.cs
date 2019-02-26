
namespace SecuritySafetyModule
{
    public static class SecuritySafetyDecision
    {
        public static SecuritySafetyResult GetSecuritySafetyDecision(string fileLocation, int mode)
        {
            var resultObject = SecuritySafetyRunner.GetWinCheckSecResultObject(fileLocation);
            int percentageResult = GetSecuritySafetyPercentage(resultObject, mode);
            // -1, 0 or 1
            int ternaryResult = GetSecuritySafetyResultFromPercentage(percentageResult, mode);
            return new SecuritySafetyResult
            {
                Safety = ternaryResult,
                SafetyAndSecurityPercentage = percentageResult,
                SafetyAndSecurityPercentageBase = GetSecuritySafetyMaxPercentage(mode),
                Security = ternaryResult,
                winCheckSecResultObject = resultObject

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
            int percentage = 0;
            if (resultObject.Aslr) percentage += 20;
            if (resultObject.DynamicBase) percentage += 20;
            if (resultObject.Nx) percentage += 20;
            if (resultObject.Seh) percentage += 20;
            if (mode == 0) return percentage;

            if (resultObject.Isolation) percentage += 5;
            if (resultObject.Gs) percentage += 4;
            if (resultObject.Cfg) percentage += 3;
            if (mode == 1) return percentage;

            if (resultObject.HighEntropyVa) percentage += 2;
            if (resultObject.Authenticode) percentage += 2;
            if (resultObject.SafeSeh) percentage += 2;
            if (resultObject.Rfg) percentage += 1;
            if (resultObject.ForceIntegrity) percentage += 1;

            return percentage;
        }
    }
}
