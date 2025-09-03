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
        foreach(var addon in serviceAddons)
        {
            switch(addon)
            {
                case EServiceAddon.TireShine:
                    // NOTE: yield return allows for scale and flexibility not 
                    //       currently needed w.r.t. async nature of services
                    yield return _tireShineService;
                    break;
                case EServiceAddon.InteriorClean:
                    yield return _interiorCleanService;
                    break;
                case EServiceAddon.HandWaxAndShine:
                    yield return _handWaxAndShineService;
                    break;
                default:
                    throw new InvalidOperationException($"Add-on service ({addon}) not recognized.");
            }
        }
    }
}