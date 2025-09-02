using System.Collections.Immutable;
using CarWashProcessor.Models;

namespace CarWashProcessor.Services.AddOnServices;

public class AddOnServiceFactory
{
    private TireShineService _tireShineService;
    private InteriorCleanService _interiorCleanService;
    private HandWaxAndShineService _handWaxAndShineService;

    public AddOnServiceFactory(
        TireShineService tireShineService,
        InteriorCleanService interiorCleanService,
        HandWaxAndShineService handWaxAndShineService
    )
    {
        _tireShineService = tireShineService;
        _interiorCleanService = interiorCleanService;
        _handWaxAndShineService = handWaxAndShineService;
    }

    public IEnumerable<IAddOnService> GetAddOnServices(ImmutableArray<EServiceAddon> serviceTypes)
    {
        foreach (var serviceType in serviceTypes)
        {
            yield return getAddOnService(serviceType);
        }
    }
    
    private IAddOnService getAddOnService(EServiceAddon serviceType)
    {
        return serviceType switch
        {
            EServiceAddon.TireShine => _tireShineService,
            EServiceAddon.InteriorClean => _interiorCleanService,
            EServiceAddon.HandWaxAndShine => _handWaxAndShineService,
            _ => throw new InvalidOperationException($"Add-on service ({serviceType}) not recognized.")
        };
    }
}