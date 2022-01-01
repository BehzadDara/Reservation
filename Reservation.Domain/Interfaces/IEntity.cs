using System;

namespace Reservation.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}