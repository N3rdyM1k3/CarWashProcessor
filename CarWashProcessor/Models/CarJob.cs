using System.Collections.Immutable;

namespace CarWashProcessor.Models;

public enum ECarMake { Ford, Toyota, Hyundai, Subaru, Dodge, Chevy }

public enum EServiceWash { Basic, Awesome, ToTheMax }

public enum EServiceAddon { TireShine, InteriorClean, HandWaxAndShine }

public record CarJob(
	int CustomerId,
	ECarMake CarMake,
	EServiceWash ServiceWash,
	ImmutableArray<EServiceAddon> ServiceAddons
);
