using System;

namespace Reservation.Application.Contracts
{
    public abstract class EntityDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}