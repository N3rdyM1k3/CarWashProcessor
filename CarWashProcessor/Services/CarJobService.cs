using CarWashProcessor.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarWashProcessor.Services;

public class CarJobService
{
	private readonly ILogger<CarJobService> _logger;

	public CarJobService(ILogger<CarJobService> logger)
	{
		// Set services
		_logger = logger;
	}

	public async Task<CarJob> GetCarJobAsync(CancellationToken stoppingToken)
	{
		// Wait a bit
		await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
		// Create car job
		var carJob = new CarJob(
			CustomerId: Random.Shared.Next(1, 1_000_000),
			CarMake: _GetRandom<ECarMake>(),
			ServiceWash: _GetRandom<EServiceWash>(),
			ServiceAddons: [
				.. Enumerable
					.Range(start: 0, count: 1000)
					.Select(index => _GetRandom<EServiceAddon>())
					.Distinct()
					.Take(Random.Shared.Next(_GetCount<EServiceAddon>())),
			]
		);
		// Log information
		_logger.LogInformation("");
		_logger.LogInformation("");
		_logger.LogInformation("");
		_logger.LogInformation("");
		_logger.LogInformation("");
		_logger.LogInformation(
			"Car job received = {}",
			JsonSerializer.Serialize(carJob, _GetJsonOptions())
		);
		// Return car job
		return carJob;
	}

	private static TEnum _GetRandom<TEnum>()
	where TEnum : struct, Enum
	{
		// Get values
		var values = Enum.GetValues<TEnum>();
		// Return random value
		return values[Random.Shared.Next(values.Length)];
	}

	private static int _GetCount<TEnum>()
	where TEnum : struct, Enum
	{
		// Return values count
		return Enum.GetValues<TEnum>().Length;
	}

	private static JsonSerializerOptions _GetJsonOptions()
	{
		// Create options
		var options = new JsonSerializerOptions();
		options.Converters.Add(new JsonStringEnumConverter());
		// Return options
		return options;
	}
}
