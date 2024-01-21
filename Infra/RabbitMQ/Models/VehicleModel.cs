using Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.RabbitMQ.Models
{
    public class VehicleModel
    {
        public string Id { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string Version { get; set; } = default!;
        public string ManufacturingYear { get; set; } = default!;
        public string ModelYear { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? SourceId { get; set; }

        // Engine Specifications
        public string? EngineType { get; set; } = default!;
        public string? CylinderType { get; set; } = default!;
        public int? Valves { get; set; } = default!;
        public string? FuelSystem { get; set; } = default!;
        public string? EnginePosition { get; set; } = default!;
        public string? FuelType { get; set; } = default!;
        public int? PowerHP { get; set; } = default!;
        public int? DisplacementCC { get; set; } = default!;
        public double? TorqueKgfM { get; set; } = default!;

        // Other Information
        public string? Steering { get; set; } = default!;
        public string? Traction { get; set; } = default!;
        public string? Transmission { get; set; } = default!;

        // Performance
        public int? MaxSpeedKmh { get; set; } = default!;
        public double? Acceleration0To100Kmh { get; set; } = default!;
        public double? CityConsumptionKmL { get; set; } = default!;
        public double? HighwayConsumptionKmL { get; set; } = default!;

        // Suspension / Brake / Wheel
        public string? FrontSuspension { get; set; } = default!;
        public string? RearSuspension { get; set; } = default!;
        public string? FrontBrake { get; set; } = default!;
        public string? RearBrake { get; set; } = default!;
        public string? Wheel { get; set; } = default!;
        public string? Tire { get; set; } = default!;

        // Dimensions
        public int? LengthMm { get; set; } = default!;
        public int? WheelbaseMm { get; set; } = default!;
        public int? HeightMm { get; set; } = default!;
        public int? WidthMm { get; set; } = default!;
        public int? CurbWeightKg { get; set; } = default!;
        public int? TrunkCapacityLiters { get; set; } = default!;
        public int? FuelTankCapacityLiters { get; set; } = default!;
        public int? DoorCount { get; set; } = default!;
        public int? PassengerCapacity { get; set; } = default!;

        public VehicleSpec ToVehicleSpec()
        {
            return new VehicleSpec
            {
                Id = Id,
                Brand = Brand,
                Model = Model,
                Version = Version,
                ManufacturingYear = ManufacturingYear,
                ModelYear = ModelYear,
                FullName = FullName,
                SourceId = SourceId,
                EngineType = EngineType,
                CylinderType = CylinderType,
                Valves = Valves,
                FuelSystem = FuelSystem,
                EnginePosition = EnginePosition,
                FuelType = FuelType,
                PowerHP = PowerHP,
                DisplacementCC = DisplacementCC,
                TorqueKgfM = TorqueKgfM,
                Steering = Steering,
                Traction = Traction,
                Transmission = Transmission,
                MaxSpeedKmh = MaxSpeedKmh,
                Acceleration0To100Kmh = Acceleration0To100Kmh,
                CityConsumptionKmL = CityConsumptionKmL,
                HighwayConsumptionKmL = HighwayConsumptionKmL,
                FrontSuspension = FrontSuspension,
                RearSuspension = RearSuspension,
                FrontBrake = FrontBrake,
                RearBrake = RearBrake,
                Wheel = Wheel,
                Tire = Tire,
                LengthMm = LengthMm,
                WheelbaseMm = WheelbaseMm,
                HeightMm = HeightMm,
                WidthMm = WidthMm,
                CurbWeightKg = CurbWeightKg,
                TrunkCapacityLiters = TrunkCapacityLiters,
                FuelTankCapacityLiters = FuelTankCapacityLiters,
                DoorCount = DoorCount,
                PassengerCapacity = PassengerCapacity
            };
        }
    }
}
