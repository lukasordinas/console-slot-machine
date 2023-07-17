using SlotMachineApp.Accounts;
using System;

namespace SlotMachineApp.SlotMachine
{
	public class SlotGame : ISlotGame
	{
		private ISlotGameConfig config;

		private IAccountGameService account;

		public SlotGame(ISlotGameConfig config, IAccountGameService account)
		{
			this.config = config;
			this.account = account;
		}

		public string GameTitle { get => config.GameTitle; }
	}
}