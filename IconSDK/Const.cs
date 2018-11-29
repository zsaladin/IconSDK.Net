namespace IconSDK
{
    public static class Consts
    {
        public static class ApiUrl
        {
            public const string MainNet = "https://wallet.icon.foundation/api/v3";
            public const string TestNet = "https://testwallet.icon.foundation/api/v3";

            public static int GetNetworkID(string apiUrl)
            {
                if (apiUrl == MainNet)
                    return 1;
                if (apiUrl == TestNet)
                    return 2;
                return 3;
            }
        }
    }
}