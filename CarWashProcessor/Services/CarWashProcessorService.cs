using CarWashProcessor.Models;
using CarWashProcessor.Services.CarWashServices;

namespace CarWashProcessor.Services;

public class CarWashProcessorService : ICarJobProcessorService
{
    private readonly BasicWashService _basicWashService;
    private readonly AwesomeWashService _awesomeWashService;
    private readonly ToTheMaxWashService _toTheMaxWashService;

    public CarWashProcessorService(
        BasicWashService basicWashService,
        AwesomeWashService awesomeWashService,
        ToTheMaxWashService toTheMaxWashService)
    {
        _basicWashService = basicWashService;
        _awesomeWashService = awesomeWashService;
        _toTheMaxWashService = toTheMaxWashService;
    }

    public async Task ProcessCarJobAsync(CarJob carJob)
    {
        var washService = getWashService(carJob.ServiceWash);
        await washService.DoWash(carJob);
    }

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