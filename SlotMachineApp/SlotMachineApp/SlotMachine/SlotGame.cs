using SlotMachineApp.Accounts;
using SlotMachineApp.SlotMachine.Spins;
using System;

namespace SlotMachineApp.SlotMachine
{
	public class SlotGame : ISlotGame
	{
		private ISlotGameConfig config;

		private IAccountGameService account;
        public string GameTitle { get => config.GameTitle; }

        public SlotGame(ISlotGameConfig config, IAccountGameService account)
		{
			this.config = config;
			this.account = account;
		}

		public SpinResult? Spin(decimal betAmount)
		{
			if (!ValidateBet(betAmount))
			{
				return null;
			}

			this.account.DebitBalance(betAmount);

			var rowResults = new List<RowResult>();

			for (int i = 0; i < this.config.Rows; i++)
			{
                rowResults.Add(GenerateRowResult(betAmount));
			}

			var spinResult = new SpinResult(rowResults);
			if (spinResult.TotalWinnings > 0)
			{
				this.account.CreditBalance(spinResult.TotalWinnings);
			}

			return spinResult;
		}

        private RowResult GenerateRowResult(decimal betAmount)
        {
			var symbols = new List<SlotSymbol>();

            for (int c = 0; c < this.config.Columns; c++)
			{
				var symbol = GenerateRandomSymbol();
				if (symbol != null)
                {
                    symbols.Add(symbol);
                }
			}

			var winnings = CalculateRowWinnings(symbols, betAmount);

			return new RowResult(symbols, winnings);
        }

        private decimal CalculateRowWinnings(List<SlotSymbol> symbols, decimal betAmount)
        {
			// to win, a symbol must match all other symbols excluding wildcards
			// the game is lost the first time a non-matching symbol is found.
			SlotSymbol? symbolToMatch = null;

			foreach (var symbol in symbols)
			{
				if (symbol.IsWildcard)
				{
					continue;
				}

				if (symbolToMatch == null)
				{
					symbolToMatch = symbol;
				}
				else if (symbol != symbolToMatch)
				{
					return 0; // lost
				}
			}

			// won
			float totalCoefficient = 0;
			foreach (var symbol in symbols)
			{
				totalCoefficient += symbol.Coefficient;
			}

			return betAmount * (decimal)totalCoefficient;
        }

        private SlotSymbol? GenerateRandomSymbol()
        {
			Random random = new Random();
			var randomNumber = random.NextDouble();
			double cumulativeProbability = 0;

			foreach (var symbol in this.config.Symbols)
			{
				cumulativeProbability += symbol.Probability;
				if (randomNumber < cumulativeProbability)
				{
					return symbol;
				}
			}

			return null;
        }

        private bool ValidateBet(decimal amount)
        {
			return amount > 0 && amount <= this.account.Balance;
        }
    }
}