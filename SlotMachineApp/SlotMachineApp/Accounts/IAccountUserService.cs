using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp.Accounts
{
    public interface IAccountUserService
    {
        public decimal Balance { get; }

        public bool Deposit(decimal amount);
    }
}
