using CarWashProcessor.Models;

namespace CarWashProcessor.Services;

public class AwesomeWashService
{
	private readonly ILogger<AwesomeWashService> _logger;

	public AwesomeWashService(ILogger<AwesomeWashService> logger)
	{
		// Set services
		_logger = logger;
	}

	public async Task DoAwesomeWashAsync(CarJob carJob)
	{
		// Wait a second
		await Task.Delay(TimeSpan.FromSeconds(1));
		// Log information
		_logger.LogInformation("--> Awesome wash performed for customer {}!", carJob.CustomerId);
	}
}
