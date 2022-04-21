using dYdX.Net.Types;
using System;


namespace dYdX.Net
{
    public class DYDXClient
    {
        public Public _public;
        public Private _private;
        private string mainnetBaseUrl = "https://api.dydx.exchange";
        private string ropstenBaseUrl = "https://api.stage.dydx.exchange";

        public DYDXClient (Enums.NetworkType networkType)
        {
            string baseUrl;
            int networkId;

            if (networkType == Enums.NetworkType.MAINNET)
            {
                baseUrl = mainnetBaseUrl;
                networkId = 1;
            }
            else
            {
                baseUrl = ropstenBaseUrl;
                networkId = 3;
            }
            _public = new Public(baseUrl);
            _private = new Private(baseUrl, networkId);
        }
    }
}
