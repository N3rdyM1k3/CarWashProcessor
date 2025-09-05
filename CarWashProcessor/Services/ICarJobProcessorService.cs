using CarWashProcessor.Models;

namespace CarWashProcessor.Services;

public interface ICarJobProcessorService
{
    Task ProcessCarJobAsync(CarJob carJob);
}
