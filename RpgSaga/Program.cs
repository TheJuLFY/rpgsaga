namespace RPGSagaConsoleApp
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using RpgSaga.Core;
    using RpgSaga.Core.BattleLogic;
    using RpgSaga.Core.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    DIConfig.CreateListOfDI(services);
                    services.AddSingleton<IProcessArgumentsReader>(_ => new ProcessArgumentsReader(args));
                });
    }
}
