using System;
using System.Collections.Generic;
using System.Text;

namespace dYdX.Net.Types
{
    public static class Enums
    {
        public static string[] CandleResolutionArray = { "", "1DAY", "4HOURS", "1HOUR", "30MINS", "15MINS", "5MINS", "1MIN" };
        public static string[] LeaderboardPnlPeriodArray = { "", "DAILY", "WEEKLY", "MONTHLY", "ALLTIME", "COMPETITION", "DAILY_COMPETITION", "LEAGUES" };
        public static string[] OrderStatusArray = { "", "PENDING", "OPEN", "FILLED", "CANCELED", "UNTRIGGERED" };
        public static string[] PNLArray = { "", "ABSOLUTE", "PERCENT" };
        public static string[] NftRevealTypeArray = { "", "DAY", "WEEK" };
        public static string[] SideArray = { "", "BUY", "SELL" };
        public static string[] PositionStatusArray = { "", "OPEN", "CLOSED", "LIQUIDATED" };
        public static string[] TransferTypeArray = { "", "DEPOSIT", "WITHDRAWAL ", "FAST_WITHDRAWAL" };
        public static string[] OrderTypeArray = { "", "LIMIT", "MARKET", "STOP_LIMIT ", "TRAILING_STOP", "TAKE_PROFIT" };
        public static string[] TimeInForceArray = { "GTT", "FOK", "IOC" };

        public enum CandleResolution
        {
            NULL,
            ONE_DAY,
            FOUR_HOURS,
            ONE_HOUR,
            THIRTY_MINS,
            FIFTEEN_MINS,
            FIVE_MINS,
            ONE_MIN
        }

        public enum LeaderboardPnlPeriod
        {
            NULL,
            DAILY,
            WEEKLY,
            MONTHLY,
            ALLTIME,
            COMPETITION,
            DAILY_COMPETITION,
            LEAGUES
        }

        public enum OrderStatus
        {
            NULL,
            PENDING,
            OPEN,
            FILLED,
            CANCELED,
            UNTRIGGERED
        }

        public enum PNL
        {
            NULL,
            ABSOLUTE,
            PERCENT
        }

        public enum NftRevealType
        {
            NULL,
            DAY,
            WEEK
        }

        public enum Side
        {
            NULL,
            BUY,
            SELL
        }

        public enum PositionStatus
        {
            NULL,
            OPEN,
            CLOSED,
            LIQUIDATED
        }

        public enum TransferType
        {
            NULL,
            DEPOSIT,
            WITHDRAWAL,
            FAST_WITHDRAWAL
        }

        public enum OrderType
        {
            NULL,
            LIMIT,
            MARKET,
            STOP_LIMIT,
            TRAILING_STOP,
            TAKE_PROFIT
        }

        public enum TimeInForce
        {
            GTT,
            FOK,
            IOC
        }

        public enum NetworkType
        {
            MAINNET,
            ROPSTEN
        }
    }
}
