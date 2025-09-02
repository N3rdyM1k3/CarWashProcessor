using CarWashProcessor.Services;
using CarWashProcessor.Services.AddOnServices;
using CarWashProcessor.Services.CarWashServices;

namespace CarWashProcessor;

public class Program
{
	public static void Main(string[] args)
	{
		// Create builder
		var builder = Host.CreateApplicationBuilder(args);
		// Configure logging
		builder.Logging.AddSystemdConsole();
		// Register services
		builder.Services.AddHostedService<Worker>();
		builder.Services.AddSingleton<CarJobService>();
		_RegisterServices(builder.Services);
		// Build and run host
		var host = builder.Build();
		host.Run();
	}

	private static void _RegisterServices(IServiceCollection services)
	{
		// Register services
		_AddCarWashServices(services);
		services.AddSingleton<TireShineService>();
		services.AddSingleton<InteriorCleanService>();
		services.AddSingleton<HandWaxAndShineService>();
	}

	private static void _AddCarWashServices(IServiceCollection services)
	{
		services.AddSingleton<CarJobProcessorService>();
		services.AddSingleton<BasicWashService>();
		services.AddSingleton<AwesomeWashService>();
		services.AddSingleton<ToTheMaxWashService>();

		services.AddSingleton<CarWashServiceFactory>();
	}
}
