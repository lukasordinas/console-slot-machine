using SlotMachineApp.Accounts;
using SlotMachineApp.SlotMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp.UserInterface
{
    public class SlotGameUserInterface : ISlotGameUserInterface
    {
        private IAccountUserService account;

        private ISlotGame game;

        public SlotGameUserInterface(IAccountUserService account, ISlotGame game)
        {
            this.account = account;
            this.game = game;
        }

        public void Run()
        {
            this.WelcomeMessage();
            this.DepositPrompt();
            this.PlayGame();
            this.End();
        }

        private void WelcomeMessage()
        {
            Console.WriteLine("Welcome to {0}. Press ENTER to continue", game.GameTitle);
            Console.ReadLine();
        }

        private void DepositPrompt()
        {
            Console.WriteLine("Please enter a deposit amount:");

            bool deposited = false;
            while (!deposited)
            {
                if (decimal.TryParse(Console.ReadLine(), out var amount))
                {
                    if (account.Deposit(amount)){
                        deposited = true;
                        Console.WriteLine("Deposit successful, new balance: {0}", account.Balance);
                    }
                }

                if (!deposited)
                {
                    Console.WriteLine("Deposit failed, please try again");
                }
            }
        }

        private void PlayGame()
        {
            throw new NotImplementedException();
        }

        private void End()
        {
            throw new NotImplementedException();
        }
    }
}
