using CarWashProcessor.Models;

namespace CarWashProcessor.Services.AddOnServices;

public interface IAddOnService
{
    Task DoAddOn(CarJob carJob);
}