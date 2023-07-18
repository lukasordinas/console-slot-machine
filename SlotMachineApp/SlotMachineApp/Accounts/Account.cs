using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp.Accounts
{
    public class Account : IAccountUserService, IAccountGameService
    {
        public decimal Balance { get; private set; }

        public Account() 
        {
            this.Balance = 0;
        }

        public bool Deposit(decimal amount)
        {
            return CreditBalance(amount);
        }

        public bool CreditBalance(decimal amount)
        {
            if (amount > 0)
            {
                this.Balance += amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DebitBalance(decimal amount)
        {
            if (this.Balance >= amount)
            {
                this.Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
