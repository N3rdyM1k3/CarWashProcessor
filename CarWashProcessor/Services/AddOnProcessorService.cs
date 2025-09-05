using System.Collections.Immutable;
using CarWashProcessor.Models;
using CarWashProcessor.Services.AddOnServices;

namespace CarWashProcessor.Services;

public class AddOnProcessorService : ICarJobProcessorService
{
    private readonly Dictionary<EServiceAddon, IAddOnService> _addOnServiceProvider;

    public AddOnProcessorService(
        TireShineService tireShineService,
        InteriorCleanService interiorCleanService,
        HandWaxAndShineService handWaxAndShineService)
    {
        _addOnServiceProvider = new Dictionary<EServiceAddon, IAddOnService>
        {
            { EServiceAddon.TireShine, tireShineService },
            { EServiceAddon.InteriorClean, interiorCleanService },
            { EServiceAddon.HandWaxAndShine, handWaxAndShineService }
        };
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
            if (_addOnServiceProvider.TryGetValue(addon, out var service))
            {
                yield return service;
            }
            else
            {
                throw new InvalidOperationException($"Add-on service ({addon}) not recognized.");
            }
        }
    }
}