using CarWashProcessor.Models;
using CarWashProcessor.Services.CarWashServices;

namespace CarWashProcessor.Services;

public class CarWashProcessorService : ICarJobProcessorService
{
    private readonly Dictionary<EServiceWash, ICarWashService> _washServiceProvider;

    public CarWashProcessorService(
        BasicWashService basicWashService,
        AwesomeWashService awesomeWashService,
        ToTheMaxWashService toTheMaxWashService)
    {
        _washServiceProvider = new Dictionary<EServiceWash, ICarWashService>
        {
            { EServiceWash.Basic, basicWashService },
            { EServiceWash.Awesome, awesomeWashService },
            { EServiceWash.ToTheMax, toTheMaxWashService }
        };
    }

    public async Task ProcessCarJobAsync(CarJob carJob)
    {
        var washService = getWashService(carJob.ServiceWash);
        await washService.DoWash(carJob);
    }

    private ICarWashService getWashService(EServiceWash serviceWash)
    {
        if (_washServiceProvider.TryGetValue(serviceWash, out var service))
        {
            return service;
        }
        
        throw new InvalidOperationException(
            $"Wash service ({serviceWash}) not recognized."
        );
    }
}