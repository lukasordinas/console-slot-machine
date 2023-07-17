using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;

namespace SlotMachineApp.SlotMachine
{
	public class SlotGameConfig : ISlotGameConfig
	{
        public IEnumerable<SlotSymbol> Symbols { get; } = Enumerable.Empty<SlotSymbol>();

        public string GameTitle { get; } = string.Empty;

        public SlotGameConfig(string configFilePath)
		{
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFilePath);

            var config = configBuilder.Build();

            if (config != null)
            {
                this.GameTitle = config["GameTitle"]!;
                this.Symbols = config.GetSection("SlotSymbols").Get<IEnumerable<SlotSymbol>>()!;
            }
            else
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
        }
    }
}
