using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp.SlotMachine.Spins
{
    public class RowResult
    {
        public IEnumerable<SlotSymbol> Symbols { get; }

        public decimal Winnings { get; }

        public RowResult(IEnumerable<SlotSymbol> symbols, decimal winnings)
        {
            Symbols = symbols;
            Winnings = winnings;
        }
    }
}
