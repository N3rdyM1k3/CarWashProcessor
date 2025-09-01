using CarWashProcessor.Services;

namespace CarWashProcessor
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly CarJobService _carJobService;
		private readonly CarJobProcessorService _carJobProcessorService;

		public Worker(
			ILogger<Worker> logger,
			CarJobService carJobService,
			CarJobProcessorService carJobProcessorService
		)
		{
			// Set services
			_logger = logger;
			_carJobService = carJobService;
			_carJobProcessorService = carJobProcessorService;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			// Wait a second
			await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
			// Log information
			_logger.LogInformation("Started processing car jobs...");
			// Run until canceled
			try
			{
				while (!stoppingToken.IsCancellationRequested)
				{
					// Get car job
					var carJob = await _carJobService.GetCarJobAsync(stoppingToken);
					// Process car job
					await _carJobProcessorService.ProcessCarJobAsync(carJob);
				}
			}
			finally
			{
				// Log information
				_logger.LogInformation("Stopped processing car jobs...");
			}
		}
	}
}
