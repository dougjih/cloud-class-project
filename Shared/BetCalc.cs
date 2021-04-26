using System;

namespace BlazorApp.Shared
{
    public static class BetCalc
    {
        public static decimal CalcWin(int odds, decimal wager)
        {
            return wager * ConvertOdds(odds);
        }

        public static decimal ConvertOdds(int odds)
        {
            return odds >= 0 ? odds / 100M : 1M / (-odds / 100M);
        }

        public static decimal CalcBalancedWager(Bet source, Bet target)
        {
            return CalcBalancedWager(target.Odds, target.Offset, source.Odds, source.Wager, source.Offset);
        }

        public static decimal CalcBalancedWager(int odds, decimal offset, int otherOdds, decimal otherWager, decimal otherOffset)
        {
            return (otherWager * ConvertOdds(otherOdds) + otherOffset + otherWager - offset) / (ConvertOdds(odds) + 1);
        }
    }
}
