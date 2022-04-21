using System;
using dYdX.Net.Types;

namespace dYdX.Net.Examples
{
    class Program
    {
        public static DYDXClient dYDXClient;
        
        static void Main(string[] args)
        {
            string mockApiKey = "...";
            string mockApiSecret = "...";
            string mockApiPassphrase = "...";
            string mockStarkPrivateKey = "...";
            string mockEthereumAddress = "0x...";
            int mockPositionId = 111111;

            dYDXClient = new DYDXClient(Enums.NetworkType.MAINNET);

            Structs.ApiKeyCredentials apiKeyCredentials = new Structs.ApiKeyCredentials(mockApiKey, mockApiSecret, mockApiPassphrase);
            dYDXClient._private.SetApiKeyCredentials(apiKeyCredentials);
            dYDXClient._private.SetStarkPrivateKey(mockStarkPrivateKey);
            
            // call get markets : public api 
            GetMarkets();

            // call get account : private api 
            GetAccount(mockEthereumAddress);

            // call create order : private api 
            Structs.ApiOrder apiOrder = new Structs.ApiOrder();
            apiOrder.market = "BTC-USD";
            apiOrder.side = Enums.Side.BUY;
            apiOrder.type = Enums.OrderType.MARKET;
            apiOrder.size = 1.1;
            apiOrder.timeInForce = Enums.TimeInForce.FOK;
            apiOrder.price = 10000;
            apiOrder.postOnly = false;
            apiOrder.limitFee = 1;
            apiOrder.expiration = DateTime.Now.AddSeconds(60);
            CreateOrder(apiOrder, mockPositionId);

            Console.Write("press any key to exit...");
            Console.ReadKey();
        }

        public static async void GetMarkets()
        {
            try
            {
                var markets = await dYDXClient._public.GetMarkets();
                Console.WriteLine(markets);
            }
            catch (Exception exception)
            {
                throw new Exception("GetMarkets request failed.", exception);
            }
        }

        public static async void GetAccount(string ethereumAddress)
        {
            try
            {
                var account = await dYDXClient._private.GetAccount(ethereumAddress);
                Console.WriteLine(account);
            }
            catch (Exception exception)
            {
                throw new Exception("GetAccount request failed.", exception);
            }

        }
        public static async void CreateOrder(Structs.ApiOrder apiOrder, int positionId)
        {
            try
            {
                var order = await dYDXClient._private.CreateOrder(apiOrder, positionId);
                Console.WriteLine(order);
            }
            catch (Exception exception)
            {
                throw new Exception("CreateOrder request failed.", exception);
            }
        }

    }

  
}
