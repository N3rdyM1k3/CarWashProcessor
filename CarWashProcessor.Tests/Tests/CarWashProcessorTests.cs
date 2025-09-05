using Xunit;
using CarWashProcessor.Services.CarWashServices;
using CarWashProcessor.Services.AddOnServices;
using CarWashProcessor.Services;
using CarWashProcessor.Models;
using NSubstitute;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace CarWashProcessor.Tests.Tests
{
    public class CarWashProcessorTests
    {
        private CarJobProcessorService _processor;
        private ILogger<BasicWashService> _basicWashLogger;
        private ILogger<AwesomeWashService> _awesomeWashLogger;
        private ILogger<ToTheMaxWashService> _toTheMaxWashLogger;
        private ILogger<TireShineService> _tireShineLogger;
        private ILogger<InteriorCleanService> _interiorCleanLogger;
        private ILogger<HandWaxAndShineService> _handWaxLogger;

        public CarWashProcessorTests()
        {
            // Create mock loggers using NSubstitute
            _basicWashLogger = Substitute.For<ILogger<BasicWashService>>();
            _awesomeWashLogger = Substitute.For<ILogger<AwesomeWashService>>();
            _toTheMaxWashLogger = Substitute.For<ILogger<ToTheMaxWashService>>();
            _tireShineLogger = Substitute.For<ILogger<TireShineService>>();
            _interiorCleanLogger = Substitute.For<ILogger<InteriorCleanService>>();
            _handWaxLogger = Substitute.For<ILogger<HandWaxAndShineService>>();

            var carWashProcessorService = new CarWashProcessorService(
                new BasicWashService(_basicWashLogger),
                new AwesomeWashService(_awesomeWashLogger),
                new ToTheMaxWashService(_toTheMaxWashLogger)
            );

            var addOnProcessorService = new AddOnProcessorService(
                new TireShineService(_tireShineLogger),
                new InteriorCleanService(_interiorCleanLogger),
                new HandWaxAndShineService(_handWaxLogger)
            );
            
            _processor = new CarJobProcessorService(
                carWashProcessorService,
                addOnProcessorService
            );
        }


        [Fact]
        public async Task ProcessCarJobAsync_BasicWash_LogsBasicWashMessage()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 123,
                CarMake: ECarMake.Toyota,
                ServiceWash: EServiceWash.Basic,
                ServiceAddons: ImmutableArray<EServiceAddon>.Empty
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            // TODO/NOTE: I could spend more time here, but want to move on. Good enough. Not the point. 
            _basicWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Basic wash performed for customer 123!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_AwesomeWash_LogsAwesomeWashMessage()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 456,
                CarMake: ECarMake.Ford,
                ServiceWash: EServiceWash.Awesome,
                ServiceAddons: ImmutableArray<EServiceAddon>.Empty
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _awesomeWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Awesome wash performed for customer 456!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_ToTheMaxWash_LogsToTheMaxWashMessage()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 789,
                CarMake: ECarMake.Subaru,
                ServiceWash: EServiceWash.ToTheMax,
                ServiceAddons: ImmutableArray<EServiceAddon>.Empty
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _toTheMaxWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> To The Max wash performed for customer 789!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_BasicWashWithTireShine_LogsBothServices()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 111,
                CarMake: ECarMake.Toyota,
                ServiceWash: EServiceWash.Basic,
                ServiceAddons: ImmutableArray.Create(EServiceAddon.TireShine)
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _basicWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Basic wash performed for customer 111!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _tireShineLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Tires have been shined for customer 111!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_AwesomeWashWithInteriorClean_LogsBothServices()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 222,
                CarMake: ECarMake.Hyundai,
                ServiceWash: EServiceWash.Awesome,
                ServiceAddons: ImmutableArray.Create(EServiceAddon.InteriorClean)
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _awesomeWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Awesome wash performed for customer 222!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _interiorCleanLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Interior has been cleaned for customer 222!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_ToTheMaxWashWithHandWaxAndShine_LogsBothServices()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 333,
                CarMake: ECarMake.Dodge,
                ServiceWash: EServiceWash.ToTheMax,
                ServiceAddons: ImmutableArray.Create(EServiceAddon.HandWaxAndShine)
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _toTheMaxWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> To The Max wash performed for customer 333!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _handWaxLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Hand waxed and shined for customer 333!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_BasicWashWithTwoAddons_LogsAllThreeServices()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 444,
                CarMake: ECarMake.Chevy,
                ServiceWash: EServiceWash.Basic,
                ServiceAddons: ImmutableArray.Create(EServiceAddon.TireShine, EServiceAddon.InteriorClean)
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _basicWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Basic wash performed for customer 444!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _tireShineLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Tires have been shined for customer 444!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _interiorCleanLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Interior has been cleaned for customer 444!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_AwesomeWashWithAllAddons_LogsAllFourServices()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 555,
                CarMake: ECarMake.Ford,
                ServiceWash: EServiceWash.Awesome,
                ServiceAddons: ImmutableArray.Create(
                    EServiceAddon.TireShine, 
                    EServiceAddon.InteriorClean, 
                    EServiceAddon.HandWaxAndShine)
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _awesomeWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Awesome wash performed for customer 555!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _tireShineLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Tires have been shined for customer 555!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _interiorCleanLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Interior has been cleaned for customer 555!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _handWaxLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Hand waxed and shined for customer 555!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_ToTheMaxWashWithAllAddons_LogsAllFourServices()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 666,
                CarMake: ECarMake.Subaru,
                ServiceWash: EServiceWash.ToTheMax,
                ServiceAddons: ImmutableArray.Create(
                    EServiceAddon.TireShine, 
                    EServiceAddon.InteriorClean, 
                    EServiceAddon.HandWaxAndShine)
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert
            _toTheMaxWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> To The Max wash performed for customer 666!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _tireShineLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Tires have been shined for customer 666!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _interiorCleanLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Interior has been cleaned for customer 666!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _handWaxLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Hand waxed and shined for customer 666!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task ProcessCarJobAsync_OnlyTireShineAddon_LogsOnlyTireShine()
        {
            // Arrange
            var carJob = new CarJob(
                CustomerId: 777,
                CarMake: ECarMake.Toyota,
                ServiceWash: EServiceWash.Basic,
                ServiceAddons: ImmutableArray.Create(EServiceAddon.TireShine)
            );

            // Act
            await _processor.ProcessCarJobAsync(carJob);

            // Assert - Verify only the expected services were called
            _basicWashLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Basic wash performed for customer 777!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _tireShineLogger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "--> Tires have been shined for customer 777!"),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            // Verify other services were NOT called
            _interiorCleanLogger.DidNotReceive().Log(
                Arg.Any<LogLevel>(),
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());

            _handWaxLogger.DidNotReceive().Log(
                Arg.Any<LogLevel>(),
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<Exception?>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

    }
}