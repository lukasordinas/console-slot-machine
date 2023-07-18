using SlotMachineApp.Accounts;
using SlotMachineApp.SlotMachine;
using SlotMachineApp.SlotMachine.Spins;

namespace SlotMachineApp.UserInterface
{
    public class SlotGameUserInterface : ISlotGameUserInterface
    {
        private readonly IAccountUserService account;

        private readonly ISlotGame game;

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
                    if (account.Deposit(Math.Round(amount, 2)))
                    {
                        deposited = true;
                        Console.WriteLine("Deposit successful, new balance: {0}\n", account.Balance.ToString("F"));
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

                if (Math.Round(this.account.Balance, 2) <= 0)
                {
                    playing = false;
                }
            }
        }

        private void Spin(decimal amount)
        {
            Console.WriteLine("");
            var result = this.game.Spin(amount);

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
                Console.WriteLine("Total winnings: {0}", result.TotalWinnings.ToString("F"));
            }

            Console.WriteLine("New balance: {0}", this.account.Balance.ToString("F"));
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
                rowString += " Won: " + row.Winnings.ToString("F");
            }

            Console.WriteLine(rowString);
        }

        private void End()
        {
            Console.WriteLine("Game over, thanks for playing {0}", this.game.GameTitle);
        }
    }
}
