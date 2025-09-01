using CarWashProcessor.Models;

namespace CarWashProcessor.Services;

public class CarJobProcessorService
{
	private readonly BasicWashService _basicWashService;
	private readonly AwesomeWashService _awesomeWashService;
	private readonly ToTheMaxWashService _toTheMaxWashService;
	private readonly TireShineService _tireShineService;
	private readonly InteriorCleanService _interiorCleanService;
	private readonly HandWaxAndShineService _handWaxAndShineService;

	public CarJobProcessorService(
		BasicWashService basicWashService,
		AwesomeWashService awesomeWashService,
		ToTheMaxWashService toTheMaxWashService,
		TireShineService tireShineService,
		InteriorCleanService interiorCleanService,
		HandWaxAndShineService handWaxAndShineService
	)
	{
		// Set services
		_basicWashService = basicWashService;
		_awesomeWashService = awesomeWashService;
		_toTheMaxWashService = toTheMaxWashService;
		_tireShineService = tireShineService;
		_interiorCleanService = interiorCleanService;
		_handWaxAndShineService = handWaxAndShineService;
	}

	public async Task ProcessCarJobAsync(CarJob carJob)
	{
		// Check if wash service
		switch (carJob.ServiceWash)
		{
			case EServiceWash.Basic:
				// Do basic wash
				await _basicWashService.DoBasicWashAsync(carJob);
				break;
			case EServiceWash.Awesome:
				// Do awesome wash
				await _awesomeWashService.DoAwesomeWashAsync(carJob);
				break;
			case EServiceWash.ToTheMax:
				// Do to the max wash
				await _toTheMaxWashService.DoToTheMaxWashAsync(carJob);
				break;
			default:
				// Throw error
				throw new InvalidOperationException(
					$"Wash service ({carJob.ServiceWash}) not recognized."
				);
		}
		// Check if tire shine
		if (carJob.ServiceAddons.Contains(EServiceAddon.TireShine))
		{
			// Shine tires
			await _tireShineService.ShineTiresAsync(carJob);
		}
		// Check if interior clean
		if (carJob.ServiceAddons.Contains(EServiceAddon.InteriorClean))
		{
			// Clean interior
			await _interiorCleanService.CleanInteriorAsync(carJob);
		}
		// Check if hand wax and shine
		if (carJob.ServiceAddons.Contains(EServiceAddon.HandWaxAndShine))
		{
			// Hand wax and shine
			await _handWaxAndShineService.HandWaxAndShineAsync(carJob);
		}
	}
}
