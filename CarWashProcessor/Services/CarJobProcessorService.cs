using CarWashProcessor.Models;

namespace CarWashProcessor.Services;

public class CarJobProcessorService : ICarJobProcessorService
{
	private readonly ICarJobProcessorService _carWashService;
	private readonly ICarJobProcessorService _addOnService;

	public CarJobProcessorService(
		CarWashProcessorService carWashService,
		AddOnProcessorService addOnService
	)
	{
		_carWashService = carWashService;
		_addOnService = addOnService;
	}

	public async Task ProcessCarJobAsync(CarJob carJob)
	{
		await _carWashService.ProcessCarJobAsync(carJob);
		await _addOnService.ProcessCarJobAsync(carJob);
	}
}
