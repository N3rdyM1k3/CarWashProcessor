using CarWashProcessor.Models;

public interface ICarWashService
{
	Task DoWash(CarJob carJob);
}