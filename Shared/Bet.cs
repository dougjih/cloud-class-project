using System;

namespace BlazorApp.Shared
{
    public class Bet
    {
        public string Bookmaker { get; set; }
        public string OutcomeDescription { get; set; }
        public int Odds { get; set; } = 100;
        public Decimal Wager { get; set; } = 0.00M;
        public Decimal Offset { get; set; } = 0.00M;
    }
}
