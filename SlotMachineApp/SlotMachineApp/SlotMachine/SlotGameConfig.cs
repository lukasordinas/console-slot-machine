using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;

namespace SlotMachineApp.SlotMachine
{
	public class SlotGameConfig : ISlotGameConfig
	{
        public IEnumerable<SlotSymbol> Symbols { get; }

        public string GameTitle { get; }

        public int Rows { get; }

        public int Columns { get; }

        public SlotGameConfig(string configFilePath)
		{
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFilePath);

            try
            {
                var config = configBuilder.Build();

                this.GameTitle = config["GameTitle"]!;
                this.Symbols = config.GetSection("SlotSymbols").Get<IEnumerable<SlotSymbol>>()!;
                this.Rows = int.Parse(config["Rows"]!);
                this.Columns = int.Parse(config["Columns"]!);
            }
            catch
            {
                throw new InvalidOperationException("Invalid config json");
            }

			ValidateConfig();
		}

        private void ValidateConfig()
        {
            if (string.IsNullOrEmpty(this.GameTitle))
            {
                throw new InvalidOperationException("Invalid GameTitle config");
            }

            if (this.Symbols == null || !this.Symbols.Any())
            {
                throw new InvalidOperationException("Invalid SlotSymbols config");
            }

            if (this.Rows < 1)
            {
                throw new InvalidOperationException("Invalid Rows config");
            }

            if (this.Columns < 1)
            {
                throw new InvalidOperationException("Invalid Columns config");
            }

            // confirm that the sum of probabilities is exactly 1
            double totalProbability = 0;
            foreach (var symbol in this.Symbols)
            {
                totalProbability += symbol.Probability;
            }
            if (Math.Round(totalProbability, 2) != 1)
            {
                throw new InvalidOperationException("Invalid Probability config");
            }
        }
    }
}
