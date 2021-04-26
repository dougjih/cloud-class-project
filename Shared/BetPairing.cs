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
            get => Math.Round(BetCalc.CalcWin(Bet1.Odds, Bet1.Wager) + Bet1.Offset - Bet2.Wager, 2);
        }

        public decimal Win2
        {
            get => Math.Round(BetCalc.CalcWin(Bet2.Odds, Bet2.Wager) + Bet2.Offset - Bet1.Wager, 2);
        }
    }
}
