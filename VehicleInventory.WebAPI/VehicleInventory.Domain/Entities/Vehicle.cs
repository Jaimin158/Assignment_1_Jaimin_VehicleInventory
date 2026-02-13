using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities
{
    public class Vehicle
    {
        
        private Vehicle() { }

        public Vehicle(Guid id, string vehicleCode, Guid locationId, string vehicleType)
        {

            //Basic validation
            if (id == Guid.Empty)
                throw new DomainException("Id cannot be empty.");

            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new DomainException("VehicleCode is required.");

            if (locationId == Guid.Empty)
                throw new DomainException("LocationId cannot be empty.");

            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new DomainException("VehicleType is required.");

            Id = id;
            VehicleCode = vehicleCode.Trim();
            LocationId = locationId;
            VehicleType = vehicleType.Trim();

            // New vehicles are available
            Status = VehicleStatus.Available;
        }

        public Guid Id { get; private set; }
        public string VehicleCode { get; private set; } = string.Empty;
        public Guid LocationId { get; private set; }
        public string VehicleType { get; private set; } = string.Empty;
        public VehicleStatus Status { get; private set; }

        
        public void MarkAvailable()
        {
            
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Reserved vehicle cannot be marked Available without explicit release.");

            Status = VehicleStatus.Available;
        }

        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle cannot be rented because it is already rented.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle cannot be rented because it is reserved.");

            if (Status == VehicleStatus.Serviced)
                throw new DomainException("Vehicle cannot be rented because it is under service.");

            Status = VehicleStatus.Rented;
        }

        public void MarkReserved()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Rented vehicle cannot be reserved.");

            if (Status == VehicleStatus.Serviced)
                throw new DomainException("Serviced vehicle cannot be reserved.");

            Status = VehicleStatus.Reserved;
        }

        public void MarkServiced()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Rented vehicle cannot be put into service.");

            Status = VehicleStatus.Serviced;
        }

        // Explicit command method
        public void ReleaseReservation()
        {
            if (Status != VehicleStatus.Reserved)
                throw new DomainException("Vehicle is not reserved, so reservation cannot be released.");

            Status = VehicleStatus.Available;
        }
    }
}
