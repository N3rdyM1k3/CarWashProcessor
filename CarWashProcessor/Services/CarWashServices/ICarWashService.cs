using CarWashProcessor.Models;

namespace CarWashProcessor.Services.CarWashServices;

public interface ICarWashService
{
	Task DoWash(CarJob carJob);
}