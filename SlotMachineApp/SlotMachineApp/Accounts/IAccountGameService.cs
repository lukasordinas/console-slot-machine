using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp.Accounts
{
    public interface IAccountGameService
    {
        public decimal Balance { get; }

        public bool CreditBalance(decimal amount);

        public bool DebitBalance(decimal amount);
    }
}
