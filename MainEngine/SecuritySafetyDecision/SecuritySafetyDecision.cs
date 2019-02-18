namespace SecuritySafetyModule
{
    public static class SecuritySafetyDecision
    {

        public static int GetSecuritySafetyResultFromPercentage(int percentage)
        {
            if (percentage >= 80)
                return 1;
            if (percentage <= 40)
                return -1;
            return 0;
        }

        public static int GetSecuritySafetyPercentage(string fileLocation)
        {
            var resultObject = SecuritySafetyRunner.GetWinCheckSecResultObject(fileLocation);
            int percentage = 0;
            if (resultObject.Aslr) percentage += 20;
            if (resultObject.DynamicBase) percentage += 20;
            if (resultObject.Nx) percentage += 20;
            if (resultObject.Seh) percentage += 20;
            if (resultObject.Isolation) percentage += 5;
            if (resultObject.Gs) percentage += 4;
            if (resultObject.Cfg) percentage += 3;
            if (resultObject.HighEntropyVa) percentage += 2;
            if (resultObject.Authenticode) percentage += 2;
            if (resultObject.SafeSeh) percentage += 1;
            if (resultObject.Rfg) percentage += 1;
            if (resultObject.ForceIntegrity) percentage += 1;

            return percentage;
        }
    }
}
