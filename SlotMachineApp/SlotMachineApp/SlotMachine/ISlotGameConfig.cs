using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp.SlotMachine
{
    public interface ISlotGameConfig
    {
        public IEnumerable<SlotSymbol> Symbols { get; }

        public string GameTitle { get; }

        public int Rows { get; }

        public int Columns { get; }
    }
}
