using System.Collections.Immutable;
using CarWashProcessor.Models;
using CarWashProcessor.Services.CarWashServices;
using CarWashProcessor.Services.AddOnServices;

namespace CarWashProcessor.Services;

public class CarJobProcessorService
{
	private readonly CarWashServiceFactory _carWashServiceFactory;

	private readonly AddOnServiceFactory _addOnServiceFactory;

	public CarJobProcessorService(
		CarWashServiceFactory carWashServiceFactory,
		AddOnServiceFactory addOnServiceFactory
	)
	{
		// Set services
		_carWashServiceFactory = carWashServiceFactory;
		_addOnServiceFactory = addOnServiceFactory;
	}

	public async Task ProcessCarJobAsync(CarJob carJob)
	{
		var washService = _carWashServiceFactory.GetWashService(carJob.ServiceWash);
		// Do the wash	
		await washService.DoWash(carJob);

		foreach (var addOnService in _addOnServiceFactory.GetAddOnServices(carJob.ServiceAddons))
		{
			// Do the add-on service
			await addOnService.DoAddOn(carJob);
		}

	}

}
