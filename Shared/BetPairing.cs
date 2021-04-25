using System;

namespace BlazorApp.Shared
{
    public class BetPairing
    {
        public DateTimeOffset EventTime { get; set; }
        public Bet Bet1 { get; set; }
        public Bet Bet2 { get; set; }
        
        public decimal Win1
        {
            get => CalcWin(Bet1.Odds, Bet1.Wager) + Bet1.Offset - Bet2.Wager;
        }

        public decimal Win2
        {
            get => CalcWin(Bet2.Odds, Bet2.Wager) + Bet2.Offset - Bet1.Wager;
        }

        private static decimal CalcWin(int odds, decimal wager)
        {
            return Math.Round(odds > 0 ? wager * (odds / 100M) : wager / (-odds / 100M), 2);
        }
    }
}
