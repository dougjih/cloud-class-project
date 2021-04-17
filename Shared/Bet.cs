using System;

namespace CloudClassProject.Shared
{
    public class Bet
    {
        public string Bookmaker { get; set; }
        public string OutcomeDescription { get; set; }
        public int Odds { get; set; }
        public Decimal Wager { get; set; }
        public Decimal Offset { get; set; }
    }
}
