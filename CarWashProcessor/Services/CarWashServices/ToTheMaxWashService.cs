using CarWashProcessor.Models;

namespace CarWashProcessor.Services.CarWashServices;

public class ToTheMaxWashService : ICarWashService
{
	private readonly ILogger<ToTheMaxWashService> _logger;

	public ToTheMaxWashService(ILogger<ToTheMaxWashService> logger)
	{
		// Set services
		_logger = logger;
	}

	public async Task DoWash(CarJob carJob)
	{
		await doToTheMaxWashAsync(carJob);
	}

	private async Task doToTheMaxWashAsync(CarJob carJob)
	{
		// Wait a second
		await Task.Delay(TimeSpan.FromSeconds(1));
		// Log information
		_logger.LogInformation("--> To The Max wash performed for customer {}!", carJob.CustomerId);
	}
}
