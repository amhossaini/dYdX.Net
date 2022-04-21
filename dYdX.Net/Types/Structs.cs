using System;
using System.Collections.Generic;
using System.Text;

namespace dYdX.Net.Types
{
    public static class Structs
    {
        public struct ApiKeyCredentials
        {
            public ApiKeyCredentials(string key, string secret, string passphrase)
            {
                this.key = key;
                this.secret = secret;
                this.passphrase = passphrase;
            }

            public string key { get; }
            public string secret { get; }
            public string passphrase { get; }

            public override string ToString() => $"({key}, {secret}, {passphrase})";
        }

        public struct ApiOrder
        {
            public string market { get; set; }
            public Enums.Side side { get; set; }
            public Enums.OrderType type { get; set; }
            public double size { get; set; }
            public double price { get; set; }
            public string clientId { get; set; }
            public Enums.TimeInForce timeInForce { get; set; }
            public bool postOnly { get; set; }
            public double limitFee { get; set; }
            public string cancelId { get; set; }
            public string triggerPrice { get; set; }
            public string trailingPercent { get; set; }
            public DateTime expiration { get; set; }
            public string signature { get; set; }
        }

        public struct ApiTransfer
        {
            public double amount { get; set; }
            public string senderPositionId { get; set; }
            public string receiverAccountId { get; set; }
            public string receiverPublicKey { get; set; }
            public string receiverPositionId { get; set; }
            public string clientId { get; set; }
            public DateTime expiration { get; set; }
            public string signature { get; set; }
        }

        public struct ApiWithdrawal
        {
            public double amount { get; set; }
            public string asset { get; set; }
            public string clientId { get; set; }
            public DateTime expiration { get; set; }
            public string signature { get; set; }
        }

        public struct ApiFastWithdrawal
        {
            public string creditAsset { get; set; }
            public double creditAmount { get; set; }
            public double debitAmount { get; set; }
            public string toAddress { get; set; }
            public string lpPositionId { get; set; }
            public string lpStarkPublicKey { get; set; }
            public string clientId { get; set; }
            public DateTime expiration { get; set; }
            public string signature { get; set; }
        }
    }
}
