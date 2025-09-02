using CarWashProcessor.Models;

namespace CarWashProcessor.Services.AddOnServices;

public class TireShineService : IAddOnService
{
	private readonly ILogger<TireShineService> _logger;

	public TireShineService(ILogger<TireShineService> logger)
	{
		// Set services
		_logger = logger;
	}

	public async Task DoAddOn(CarJob carJob)
	{
		await shineTiresAsync(carJob);
	}

	private async Task shineTiresAsync(CarJob carJob)
	{
		// Wait a second
		await Task.Delay(TimeSpan.FromSeconds(1));
		// Log information
		_logger.LogInformation("--> Tires have been shined for customer {}!", carJob.CustomerId);
	}
}
