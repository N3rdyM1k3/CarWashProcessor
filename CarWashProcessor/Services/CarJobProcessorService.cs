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
		var washService = getWashService(carJob.ServiceWash);
		await washService.DoWash(carJob);
		
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

	// TODO: Eventually move to a factory pattern. 
	private ICarWashService getWashService(EServiceWash serviceWash) =>
		serviceWash switch
		{
			EServiceWash.Basic => _basicWashService,
			EServiceWash.Awesome => _awesomeWashService,
			EServiceWash.ToTheMax => _toTheMaxWashService,
			_ => throw new InvalidOperationException(
				$"Wash service ({serviceWash}) not recognized."
			),
		};
}
