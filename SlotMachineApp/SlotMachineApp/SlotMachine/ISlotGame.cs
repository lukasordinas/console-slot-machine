using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlotMachineApp.SlotMachine.Spins;

namespace SlotMachineApp.SlotMachine
{
    public interface ISlotGame
    {
        public string GameTitle { get; }

        public SpinResult? Spin(decimal amount);
    }
}
