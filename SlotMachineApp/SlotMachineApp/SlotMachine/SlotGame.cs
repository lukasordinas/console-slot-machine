using System;

namespace SlotMachineApp.SlotMachine
{
	public class SlotGame : ISlotGame
	{
		private ISlotGameConfig config;

		public SlotGame(ISlotGameConfig config)
		{
			this.config = config;
		}
	}
}