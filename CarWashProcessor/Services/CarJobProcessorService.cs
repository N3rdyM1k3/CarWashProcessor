using System.Collections.Immutable;
using CarWashProcessor.Models;
using CarWashProcessor.Services.CarWashServices;
using CarWashProcessor.Services.AddOnServices;

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
		foreach (var addOnService in getAddOnService(carJob.ServiceAddons))
		{
			await addOnService.DoAddOn(carJob);
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

	private IEnumerable<IAddOnService> getAddOnService(ImmutableArray<EServiceAddon> serviceAddons)
	{

		// Check if tire shine
		if (serviceAddons.Contains(EServiceAddon.TireShine))
		{
			// Shine tires
			yield return _tireShineService;
		}
		// Check if interior clean
		if (serviceAddons.Contains(EServiceAddon.InteriorClean))
		{
			// Clean interior
			yield return _interiorCleanService;
		}
		// Check if hand wax and shine
		if (serviceAddons.Contains(EServiceAddon.HandWaxAndShine))
		{
			// Hand wax and shine
			yield return _handWaxAndShineService;
		}

		yield break;
	}
}
