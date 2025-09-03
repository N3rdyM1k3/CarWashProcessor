using System.Collections.Immutable;
using CarWashProcessor.Models;
using CarWashProcessor.Services.AddOnServices;

namespace CarWashProcessor.Services;

public class AddOnProcessorService : ICarJobProcessorService
{
    private readonly IAddOnService _tireShineService;
    private readonly IAddOnService _interiorCleanService;
    private readonly IAddOnService _handWaxAndShineService;

    public AddOnProcessorService(
        TireShineService tireShineService,
        InteriorCleanService interiorCleanService,
        HandWaxAndShineService handWaxAndShineService)
    {
        _tireShineService = tireShineService;
        _interiorCleanService = interiorCleanService;
        _handWaxAndShineService = handWaxAndShineService;
    }

    public async Task ProcessCarJobAsync(CarJob carJob)
    {
        foreach (var addOnService in getAddOnServices(carJob.ServiceAddons))
        {
            await addOnService.DoAddOn(carJob);
        }
    }

    private IEnumerable<IAddOnService> getAddOnServices(ImmutableArray<EServiceAddon> serviceAddons)
    {
        if (serviceAddons.Contains(EServiceAddon.TireShine))
        {
            yield return _tireShineService;
        }
        
        if (serviceAddons.Contains(EServiceAddon.InteriorClean))
        {
            yield return _interiorCleanService;
        }
        
        if (serviceAddons.Contains(EServiceAddon.HandWaxAndShine))
        {
            yield return _handWaxAndShineService;
        }
    }
}