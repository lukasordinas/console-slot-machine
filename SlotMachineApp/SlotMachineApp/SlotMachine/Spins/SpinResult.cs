using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp.SlotMachine.Spins
{
    public class SpinResult
    {
        public IEnumerable<RowResult> RowResults { get; }

        public decimal TotalWinnings { get; } = 0;

        public SpinResult(IEnumerable<RowResult> rowResults)
        {
            RowResults = rowResults;

            foreach (var rowResult in rowResults)
            {
                TotalWinnings += rowResult.Winnings;
            }
        }
    }
}
