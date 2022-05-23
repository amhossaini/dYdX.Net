using dYdX.Net.Types;
using System;


namespace dYdX.Net
{
    public class DYDXClient
    {
        public Public _public;
        public Private _private;
        public Onboarding _onboarding;
        public EthPrivate _ethPrivate;
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
            _onboarding = new Onboarding(baseUrl, networkId);
            _ethPrivate = new EthPrivate(baseUrl, networkId);
        }

        public void SetApiKeyCredentials(Structs.ApiKeyCredentials apiKeyCredentials)
        {
            _private.SetApiKeyCredentials(apiKeyCredentials);
        }

        public void SetStarkPrivateKey(string starkPrivateKey)
        {
            _private.SetStarkPrivateKey(starkPrivateKey);
        }

        public void SetEthereumPrivateKey(string ethereumPrivateKey)
        {
            _onboarding.SetEthereumPrivateKey(ethereumPrivateKey);
            _ethPrivate.SetEthereumPrivateKey(ethereumPrivateKey);
        }
    }
}
