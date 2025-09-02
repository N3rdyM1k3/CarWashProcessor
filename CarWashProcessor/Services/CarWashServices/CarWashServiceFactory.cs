using CarWashProcessor.Models;
using CarWashProcessor.Services.CarWashServices;

namespace CarWashProcessor.Services.CarWashServices;

public class CarWashServiceFactory
{
    private BasicWashService _basicWashService;
    private AwesomeWashService _awesomeWashService;
    private ToTheMaxWashService _toTheMaxWashService;

    public CarWashServiceFactory(
        BasicWashService basicWashService,
        AwesomeWashService awesomeWashService,
        ToTheMaxWashService toTheMaxWashService
    )
    {
        _basicWashService = basicWashService;
        _awesomeWashService = awesomeWashService;
        _toTheMaxWashService = toTheMaxWashService;
    }

    public ICarWashService GetWashService(EServiceWash serviceType)
    {
        return serviceType switch
        {
            EServiceWash.Basic => _basicWashService,
            EServiceWash.Awesome => _awesomeWashService,
            EServiceWash.ToTheMax => _toTheMaxWashService,
            _ => throw new InvalidOperationException($"Wash service ({serviceType}) not recognized.")
        };
    }
}