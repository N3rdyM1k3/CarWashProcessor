using CarWashProcessor.Models;

public interface IAddOnService
{
    Task DoAddOn(CarJob carJob);
}