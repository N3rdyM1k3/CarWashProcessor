using System.Collections.Immutable;
using CarWashProcessor.Models;
using CarWashProcessor.Services.CarWashServices;
using CarWashProcessor.Services.AddOnServices;

namespace CarWashProcessor.Services;

public class CarJobProcessorService
{
	private readonly CarWashServiceFactory _carWashServiceFactory;
	private readonly TireShineService _tireShineService;
	private readonly InteriorCleanService _interiorCleanService;
	private readonly HandWaxAndShineService _handWaxAndShineService;

	public CarJobProcessorService(
		CarWashServiceFactory carWashServiceFactory,
		TireShineService tireShineService,
		InteriorCleanService interiorCleanService,
		HandWaxAndShineService handWaxAndShineService
	)
	{
		// Set services
		_carWashServiceFactory = carWashServiceFactory;
		_tireShineService = tireShineService;
		_interiorCleanService = interiorCleanService;
		_handWaxAndShineService = handWaxAndShineService;
	}

	public async Task ProcessCarJobAsync(CarJob carJob)
	{
		var washService = _carWashServiceFactory.GetWashService(carJob.ServiceWash);
		// Do the wash	
		await washService.DoWash(carJob);
		foreach (var addOnService in getAddOnService(carJob.ServiceAddons))
		{
			await addOnService.DoAddOn(carJob);
		}

		
	}

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
