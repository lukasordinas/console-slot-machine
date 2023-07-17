using Microsoft.Extensions.DependencyInjection;
using SlotMachineApp.Accounts;
using SlotMachineApp.SlotMachine;

namespace SlotMachineApp
{
    internal class SlotMachineApp
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var slotMachineApp = serviceProvider.GetRequiredService<SlotMachineApp>();
            slotMachineApp.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Account>();
            services.AddSingleton<IAccountUserService>(provider => provider.GetRequiredService<Account>());
            services.AddSingleton<IAccountGameService>(provider => provider.GetRequiredService<Account>());
            services.AddSingleton<ISlotGameConfig>(new SlotGameConfig(Path.Combine("GameConfigs", "BedeSlots.json")));
            services.AddSingleton<ISlotGame>(provider => new SlotGame(
                provider.GetRequiredService<ISlotGameConfig>(),
                provider.GetRequiredService<IAccountGameService>()
                ));
            services.AddSingleton<SlotMachineApp>();
            var serviceProvider = services.BuildServiceProvider();

        }

        private void Run()
        {
            throw new NotImplementedException();
        }
    }
}