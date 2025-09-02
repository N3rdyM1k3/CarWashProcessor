using CarWashProcessor.Models;

namespace CarWashProcessor.Services.CarWashServices;

public class BasicWashService : ICarWashService
{
	private readonly ILogger<BasicWashService> _logger;

	public BasicWashService(ILogger<BasicWashService> logger)
	{
		// Set services
		_logger = logger;
	}

	public async Task DoWash(CarJob carJob)
	{
		await doBasicWashAsync(carJob);
	}

	private async Task doBasicWashAsync(CarJob carJob)
	{
		// Wait a second
		await Task.Delay(TimeSpan.FromSeconds(1));
		// Log information
		_logger.LogInformation("--> Basic wash performed for customer {}!", carJob.CustomerId);
	}
}
