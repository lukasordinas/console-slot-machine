using SlotMachineApp.Accounts;
using SlotMachineApp.SlotMachine;
using SlotMachineApp.SlotMachine.Spins;
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
                    if (account.Deposit(amount))
                    {
                        deposited = true;
                        Console.WriteLine("Deposit successful, new balance: {0}", Math.Round(account.Balance, 2));
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
            var playing = true;
            while (playing)
            {
                Console.WriteLine("Please enter the amount you would like to bet and press ENTER to spin");
                if (decimal.TryParse(Console.ReadLine(), out var amount))
                {
                    this.Spin(amount);
                }
            }
        }

        private void Spin(decimal amount)
        {
            Console.WriteLine("");
            var result = game.Spin(amount);

            if (result == null)
            {
                Console.WriteLine("Spin aborted, please try again");
                return;
            }

            foreach (var row in result.RowResults)
            {
                OutputRow(row);
            }

            if (result.TotalWinnings > 0)
            {
                Console.WriteLine("Total winnings: {0}", Math.Round(result.TotalWinnings, 2));
            }

            Console.WriteLine("New balance: {0}", Math.Round(this.account.Balance, 2));
            Console.WriteLine("");
        }

        private void OutputRow(RowResult row)
        {
            var rowString = string.Empty;

            foreach (var symbol in row.Symbols)
            {
                rowString += symbol.Symbol;
            }

            if (row.Winnings > 0)
            {
                rowString += " Won: " + Math.Round(row.Winnings, 2);
            }

            Console.WriteLine(rowString);
        }

        private void End()
        {
            throw new NotImplementedException();
        }
    }
}
